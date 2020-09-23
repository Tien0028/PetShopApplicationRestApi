using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Core.DomainService
{
    public interface IOwnerRepository
    {

        Owner Create(Owner owner);
        List<Owner> ReadOwners();
        Owner Update(Owner owner);
        Owner DeleteOwner(int id);
        Owner ReadOwnerById(int id);
        IEnumerable<Owner> FindOwnerByName(string searchValue);
        IEnumerable<Owner> FindOwnerByPhoneNummer(string searchValue);
        IEnumerable<Owner> FindOwnerByAddress(string searchValue);
        IEnumerable<Owner> FindOwnerByEmail(string searchValue);
        List<Pet> FindAllPetsByOwner(Owner theOwner);
        List<Owner> FindOwner(int ownerId);
        IEnumerable <Owner> GetAllOwners();
        List<Owner> FindOwnerById(int searchForOwner);
    }
}
