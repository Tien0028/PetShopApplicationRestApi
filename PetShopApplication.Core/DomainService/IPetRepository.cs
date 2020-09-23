using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Core.DomainService
{
    public interface IPetRepository
    {
        List<Pet> ReadPets();
        Pet Create(Pet createPet);
        Pet UpdateDB(Pet pet);
        Pet ReadByPetId(int id);
        void Delete(Pet pet);
        IEnumerable<Pet> FindPetByName(string petName);
        IEnumerable<Pet> GetAllPets();
        IEnumerable<Pet> SearchPetsBytBirthday(DateTime dateValue);
        IEnumerable<Pet> FindPetsBySoldDate(DateTime soldValue);
        IEnumerable<Pet> FindPetsByFormerOwner(string searchValue);
        IEnumerable<Pet> FindPetsByPrice(long priceValue);
        List<Pet> FindPetById(int searchPetId);
    }
}
