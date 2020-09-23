using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Core.ApplicationService
{
    public interface IOwnerService
    {
        List<Owner> ReadOwners();
        List<Owner> GetAllOwners();
        Owner FindOwnerById(int id);
        Owner NewOwner(string FirstName, string LastName, string Address, string PhoneNumber, string Email);
        Owner CreateOwner(Owner owner);
        Owner UpdateOwner(Owner updateOwner);
        Owner DeleteOwner(int id);
        List<Owner> SearchByOwner(FilterModel filter);
        List<Pet> FindAllPetsByOwner(Owner theOwner);


    }
}
