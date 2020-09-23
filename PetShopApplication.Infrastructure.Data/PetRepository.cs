using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopApplication.Infrastructure.Static.Data
{
    public class PetRepository : IPetRepository
    {
        
        
        public Pet Create(Pet createPet)
        {

            return FakeDB.AddNewPet(createPet);
        }

        public void Delete(Pet pet)
        {
            FakeDB.DeletePet(pet);
        }

        public List<Pet> FindPetById(int searchPetId)
        {
            return FakeDB.allThePets.Where(pet => pet.Id == searchPetId).ToList();
        }

        public IEnumerable<Pet> FindPetByName(string petName)
        {
            IEnumerable<Pet> petsandNames = FakeDB.allThePets.Where(pet => pet.Name.ToLower().Contains(petName.ToLower()));
            return petsandNames;
        }

        public IEnumerable<Pet> FindPetsByFormerOwner(string searchValue)
        {
            IEnumerable<Pet> petsByFormerOwners = FakeDB.allThePets.Where(pet => pet.previousOwner.Contains(searchValue));
            return petsByFormerOwners;
        }

        public IEnumerable<Pet> FindPetsByPrice(long priceValue)
        {
            IEnumerable<Pet> pricePets = FakeDB.allThePets.Where(pet => pet.Price <= priceValue - 10 && pet.Price <= priceValue + 10);
            return pricePets;
        }

        public IEnumerable<Pet> FindPetsBySoldDate(DateTime soldValue)
        {
            IEnumerable<Pet> soldDatePets = FakeDB.allThePets.Where(pet => pet.SoldDate.Year == soldValue.Year);
            return soldDatePets;
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return FakeDB.allThePets;
        }

        public Pet ReadByPetId(int id)
        {
            List<Pet> _pets = FakeDB.allThePets.ToList();
            foreach (var pet in _pets)
            {
                if (pet.Id == id)
                {
                    return pet;
                }
            }
            return null;
        }

        public List<Pet> ReadPets()
        {
            return FakeDB.allThePets.ToList();
        }

        public IEnumerable<Pet> SearchPetsBytBirthday(DateTime dateValue)
        {
            IEnumerable<Pet> birthYearPets = FakeDB.allThePets.Where(pet => pet.Birthdate.Year == dateValue.Year);
            return birthYearPets;
        }

        public Pet UpdateDB(Pet pet)
        {
            var dbPet = this.ReadByPetId(pet.Id);
            if (dbPet != null)
            {
                dbPet.Name = pet.Name;
                dbPet.Birthdate = pet.Birthdate;
                dbPet.SoldDate = pet.SoldDate;
                dbPet.PetOwner = pet.PetOwner;
                dbPet.Price = pet.Price;
                dbPet.PetType = pet.PetType;
                    
                return dbPet;
            }
            return null;
        }
    }
}
