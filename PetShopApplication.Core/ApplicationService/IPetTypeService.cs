using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Core.ApplicationService
{
    public interface IPetTypeService
    {
        List<PetType> ListPetTypes();
        PetType FindPetTypeByIdwithPets(int id);
        PetType FindPetTypeById(int id);
        List<PetType> FindPetTypeByName(string name);
        PetType AddNewPetType(PetType newPetType);
        PetType DeletePetType(int id);
        PetType UpdatePetType(PetType updatePetType);
        List<PetType> SearchForPetType(FilterModel filter);
    }
}
