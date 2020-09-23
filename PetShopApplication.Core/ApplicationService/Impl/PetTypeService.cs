using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopApplication.Core.ApplicationService.Impl
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepo;

        public PetTypeService(IPetTypeRepository petTypeRepo)
        {
            _petTypeRepo = petTypeRepo;
        }

        public PetType AddNewPetType(PetType newPetType)
        {
            return _petTypeRepo.AddNewPetType(newPetType);
        }



        public PetType DeletePetType(int id)
        {
            PetType petTypeForDeletion = FindPetTypeById(id);
            return _petTypeRepo.DeletePetType(petTypeForDeletion);
        }

        public PetType FindPetTypeById(int id)
        {
            List<PetType> allPetsWithID = _petTypeRepo.FindPetTypeById(id);
            if(allPetsWithID.Count != 1)
            {
                throw new Exception("Cannot find pet with that id, source of Error: HUmans!");
            }else
            {
                
                return allPetsWithID[0];
            }
        }

        public PetType FindPetTypeByIdwithPets(int id)
        {
            List<PetType> selectedPetType = _petTypeRepo.FindPetTypeById(id);
            if(selectedPetType.Count != 1)
            {
                return null;
            }
            else
            {
                PetType desiredPetType = selectedPetType.Select(thePet => new PetType()
                {
                    IdOfPetTypes = thePet.IdOfPetTypes,
                    NameOfPetTypes = thePet.NameOfPetTypes,
                    PetTypes = FindAllPetsByPetType(thePet)
                }).FirstOrDefault(thePet => thePet.IdOfPetTypes == id);
                return desiredPetType;
            }
        }

        private List<Pet> FindAllPetsByPetType(PetType desiredPetType)
        {
            return _petTypeRepo.FindAllPetsByType(desiredPetType);
        }

        public List<PetType> ListPetTypes()
        {
            return _petTypeRepo.GetAllPetTypes();
        }

        public PetType UpdatePetType(PetType updatePetType)
        {
            PetType oldPetType = FindPetTypeById(updatePetType.IdOfPetTypes);
            return _petTypeRepo.UpdatePetType(updatePetType, oldPetType);
            
        }

        public List<PetType> SearchForPetType(FilterModel filter)
        {
            string searthTerm = filter.SearchTerm.ToLower();
            string searchValue = filter.SearchValue;
            switch (searthTerm)
            {
                case "id":
                    List<PetType> selectedPetType;
                    int searchId;
                    if(int.TryParse(searchValue, out searchId) || searchId == 0)
                    {
                        selectedPetType = new List<PetType> { FindPetTypeById(searchId) };
                        return selectedPetType;
                    }
                    else
                    {
                        throw new Exception(message: "Enter a valid id, please!");
                    }
                case "name":
                    return FindPetTypeByName(searchValue);
                default:
                    throw new Exception(message: "That search does not help, try something else");
            }
        }

        public List<PetType> FindPetTypeByName(string name)
        {
            List<PetType> petTypes = _petTypeRepo.FindPetTypeByName(name);
            if(petTypes.Count < 1)
            {
                throw new Exception(message: "Could not find any pet types matching that name, try again!");
            }
            else
            {
                return petTypes;
            }
        }
    }
}
