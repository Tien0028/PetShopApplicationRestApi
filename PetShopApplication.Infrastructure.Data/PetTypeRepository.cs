using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using PetShopApplication.Infrastructure.Static.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopApplication.Infrastructure.Data
{
    public class PetTypeRepository : IPetTypeRepository
    {
  
        public PetType AddNewPetType(PetType newPetType)
        {
            return FakeDB.AddNewPetType(newPetType);
               
        }

        public PetType DeletePetType(PetType petTypeForDeletion)
        {
            return FakeDB.DeletePetType(petTypeForDeletion);
        }



        public List<Pet> FindAllPetsByType(PetType theType)
        {
            return FakeDB.allThePets.Where(pet => pet.PetType == theType).ToList();
        }

        public List<PetType> FindPetTypeById(int id)
        {
            return FakeDB.allThePetTypes.Where(petType => petType.IdOfPetTypes == id).ToList();
        }

        public List<PetType> FindPetTypeByName(string petTypeName)
        {
            return FakeDB.allThePetTypes.Where(petType => petType.NameOfPetTypes.ToLower().Contains(petTypeName.ToLower())).ToList();
 
        }

        public List<PetType> GetAllPetTypes()
        {
            return FakeDB.allThePetTypes.ToList(); 
        }

        public PetType UpdatePetType(PetType updatePetType, PetType oldPetType)
        {
               return FakeDB.UpdatePetType(updatePetType, oldPetType);
        }
    }
}
