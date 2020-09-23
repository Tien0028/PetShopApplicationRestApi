using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PetShopApplication.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IPetTypeRepository _petTypeRepo;

        public PetService(IPetRepository petRepository, IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository)
        {
            _petRepo = petRepository;
            _ownerRepo = ownerRepository;
            _petTypeRepo = petTypeRepository;
        }
        public Pet Create(Pet pet)
        {
            return _petRepo.Create(pet);
        }

        public void Delete(Pet pet)
        {
            _petRepo.Delete(pet);
        }
                

        public Pet FindPetById(int id)
        {
            return _petRepo.ReadByPetId(id);
        }

        public List<Pet> FindPetByName(string searchName)
        {
            return _petRepo.FindPetByName(searchName).ToList();
        }

        public List<Pet> Get5CheapestPets()
        {
            return _petRepo.ReadPets().OrderBy(pet => pet.Price).Take(5).ToList();
        }

        public List<Pet> ListAllPets()
        {
            List<Pet> listAllPets = _petRepo.GetAllPets().ToList();
            if(listAllPets != null)
            {
                return listAllPets;
            }
            else
            {
                throw new Exception(message: "Missing data, nothing could be found");
            }
        }

        public Pet NewPet(string name, string type, DateTime birthday, DateTime soldDate, string previousOwner, double price)
        {
            throw new NotImplementedException();
        }

        public List<Pet> ReadPets()
        {
            return _petRepo.ReadPets();
        }

        public List<Pet> SearchForPet(FilterModel filter)
        {
            string searchValue = filter.SearchValue;
            string searchString = filter.SearchTerm.ToLower();
            switch (searchString)
            {
                case "name":
                    return _petRepo.FindPetByName(searchValue).ToList();
                case "type":
                    int theSearch;
                    List<PetType> petTypes = null;
                    if(int.TryParse(searchValue, out theSearch) && theSearch != 0)
                    {
                        petTypes = _petTypeRepo.FindPetTypeById(theSearch);
                        if(petTypes.Count < 1)
                        {
                            throw new Exception(message: "We couldn't find the id of the Pet Type");
                        }
                        else
                        {
                            return _petTypeRepo.FindAllPetsByType(petTypes[0]);
                        }
                    }
                    else
                    {
                        petTypes = _petTypeRepo.FindPetTypeByName(searchValue);
                        if(petTypes.Count < 1)
                        {
                            throw new Exception(message: "We couldn't find name of that Pet Type");
                        }
                        else
                        {
                            List<Pet> allPetsByType = null;
                            foreach (var thePetType in petTypes)
                            {
                                if(allPetsByType == null)
                                {
                                    allPetsByType = _petTypeRepo.FindAllPetsByType(thePetType);
                                }
                                else
                                {
                                    allPetsByType = allPetsByType.Concat(_petTypeRepo.FindAllPetsByType(thePetType)).ToList();
                                }
                                
                            }
                            return allPetsByType;
                        }
                    }
                case "birthday":
                    DateTime dateValue = DateTime.Now;
                    if(DateTime.TryParse(searchValue, out dateValue))
                    {
                        return _petRepo.SearchPetsBytBirthday(dateValue).ToList();
                    }
                    else
                    {
                        throw new Exception(message: "Sure you put the correct date? Cause the program's not!");
                    }
                case "solddate":
                    DateTime soldValue = DateTime.Now;
                    if(DateTime.TryParse(searchValue, out soldValue))
                    {
                        return _petRepo.FindPetsBySoldDate(soldValue).ToList();
                    }
                    else
                    {
                        throw new Exception(message: "Congrats, you made another error! You win a prize: NOT");
                    }
                case "previousOwner":
                    return _petRepo.FindPetsByFormerOwner(searchValue).ToList();

                case "owner":
                    int searchForOwner;
                    List<Owner> petOwner = null;
                    if(int.TryParse(searchValue, out searchForOwner) && searchForOwner != 0)
                    {
                        petOwner = _ownerRepo.FindOwnerById(searchForOwner);
                        if(petOwner.Count < 1)
                        {
                            throw new Exception(message: "Try writing even more like a toddler, the program can't read baby. You made a mistake, no owner with that id!");
                        }
                        else
                        {
                            return _ownerRepo.FindAllPetsByOwner(petOwner[0]);
                        }
                    }
                    else
                    {
                        petOwner = _ownerRepo.FindOwnerByName(searchValue).ToList();
                        if(petOwner.Count < 1)
                        {
                            throw new Exception(message: "Try writing even more like a toddler, the program can't read baby. You made a mistake, no owner with that name!");
                        }
                        else
                        {
                            List<Pet> allPetsByOwners = null;
                            foreach (var owner in petOwner)
                            {
                                if(allPetsByOwners == null)
                                {
                                    allPetsByOwners = _ownerRepo.FindAllPetsByOwner(owner);
                                }
                                else
                                {
                                    allPetsByOwners.Concat(_ownerRepo.FindAllPetsByOwner(owner));
                                }
                            }
                            return allPetsByOwners;
                        }
                    }
                case "price":
                    long priceValue = 0;
                    if(long.TryParse(searchValue, out priceValue))
                    {
                        return _petRepo.FindPetsByPrice(priceValue).ToList();
                    }
                    else
                    {
                        throw new Exception(message: "Forgetting something? Maybe actually putting in a price!");
                    }
                case "id":
                    int searchPetId;
                    if(int.TryParse(searchValue, out searchPetId) && searchPetId != 0)
                    {
                        return _petRepo.FindPetById(searchPetId);
                    }
                    else
                    {
                        throw new Exception(message: "Nope, that Id does not exist");
                    }
                default:
                    throw new Exception(message: "No property for this");
            }

        }

        public List<Pet> sortedPetsByPrice()
        {
            return _petRepo.ReadPets().OrderBy(pet => pet.Price).ToList();

        }

        public Pet Update(Pet pet)
        {
            Pet pets = FindPetById(pet.Id);
            pet.Name = pet.Name;
            pet.PetType = pet.PetType;
            pet.Birthdate = pet.Birthdate;
            pet.SoldDate = pet.SoldDate;
            pet.previousOwner = pet.previousOwner;
            pet.Price = pet.Price;
            return _petRepo.UpdateDB(pet);
        }
    }
}
