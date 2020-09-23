using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace PetShopApplication.Core.ApplicationService.Impl
{
    public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepo;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepo = ownerRepository;
        }
        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepo.Create(owner);
        }

        public Owner DeleteOwner(int id)
        {
            return _ownerRepo.DeleteOwner(id);
        }

        public List<Pet> FindAllPetsByOwner(Owner theOwner)
        {
            return _ownerRepo.FindAllPetsByOwner(theOwner);
        }

        public Owner FindOwnerById(int id)
        {
            return _ownerRepo.ReadOwnerById(id);
        }

        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.ReadOwners().ToList();
        }

        public Owner NewOwner(string FirstName, string LastName, string Address, string PhoneNumber, string Email)
        {
            Owner owner = new Owner()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                PhoneNumber = PhoneNumber,
                Email = Email
            };
            _ownerRepo.Create(owner);
            return owner;
        }

        public List<Owner> ReadOwners()
        {
            return _ownerRepo.ReadOwners();
        }

        public List<Owner> SearchByOwner(FilterModel filter)
        {
            string searchValue = filter.SearchValue;
            string searchTerm = filter.SearchTerm.ToLower();
            switch(searchTerm)
            {
                case "name":
                    return _ownerRepo.FindOwnerByName(searchValue).ToList();
                case "address":
                    return _ownerRepo.FindOwnerByAddress(searchValue).ToList();
                case "phonenumber":
                    return _ownerRepo.FindOwnerByPhoneNummer(searchValue).ToList();
                case "email":
                    return _ownerRepo.FindOwnerByEmail(searchValue).ToList();

                case "id":
                    int idForSearch;
                    if(int.TryParse(searchValue, out idForSearch))
                    {
                        return _ownerRepo.FindOwner(idForSearch);
                    }
                    else
                    {
                        throw new InvalidDataException(message: "Input a numer to search for an id, dumbass!");
                    }
                default:
                    throw new InvalidDataException(message: "Can't work with that. Nope, just... Not possible.");
            }
        }

  
        public Owner UpdateOwner(Owner updateOwner)
        {
            return _ownerRepo.Update(updateOwner);
        }
    }
}
