using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Core.DomainService
{
    public interface IPetTypeRepository
    {
        List<PetType> GetAllPetTypes();
        List<PetType> FindPetTypeById(int id);
        List <PetType> FindPetTypeByName(string petTypeName);
        List<Pet> FindAllPetsByType(PetType theType);
        PetType AddNewPetType(PetType newPetType);
        PetType DeletePetType(PetType petTypeForDeletion);
        PetType UpdatePetType(PetType updatePetType, PetType oldPetType);
    }
}
 