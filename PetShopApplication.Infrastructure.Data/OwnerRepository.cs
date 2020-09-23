using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using PetShopApplication.Infrastructure.Static.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopApplication.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        static int id = 1;
        private List<Owner> _owners = new List<Owner>();

        public Owner Create(Owner owner)
        {
            owner.Id = id++;
            _owners.Add(owner);
            return owner;
        }

        public Owner DeleteOwner(int id)
        {
            Owner ownerForDeletion = ReadOwnerById(id);
            if(ownerForDeletion != null)
            {
                _owners.Remove(ownerForDeletion);
                return ownerForDeletion;
            }
            return null;
        }

        public List<Pet> FindAllPetsByOwner(Owner theOwner)
        {
            List<Pet> OwnersandPets = FakeDB.allThePets.Where(pet => pet.PetOwner == theOwner).ToList();
            return OwnersandPets;
        }

        public List<Owner> FindOwner(int ownerId)
        {
            return FakeDB.allTheOwners.Where(owner => owner.Id == ownerId).ToList();
        }

        public IEnumerable<Owner> FindOwnerByAddress(string searchValue)
        {
            IEnumerable<Owner> ownersAddress = FakeDB.allTheOwners.Where(owner => owner.Address.Contains(searchValue));
            return ownersAddress;
        }

        public IEnumerable<Owner> FindOwnerByEmail(string searchValue)
        {
            IEnumerable<Owner> ownersEmail = FakeDB.allTheOwners.Where(owner => owner.Email.Contains(searchValue));
            return ownersEmail;
        }

        public List<Owner> FindOwnerById(int searchForOwner)
        {
            return FakeDB.allTheOwners.Where(owner => owner.Id == searchForOwner).ToList();
        }

        public IEnumerable<Owner> FindOwnerByName(string searchValue)
        {
            IEnumerable<Owner> ownerByName = FakeDB.allTheOwners.Where(owner => owner.FirstName.ToLower().Contains(searchValue.ToLower()) || owner.LastName.ToLower().Contains(searchValue.ToLower()));
            return ownerByName;
        }

        public IEnumerable<Owner> FindOwnerByPhoneNummer(string searchValue)
        {
            IEnumerable<Owner> ownersPhoneNumber = FakeDB.allTheOwners.Where(owner => owner.PhoneNumber.Contains(searchValue));
            return ownersPhoneNumber;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FakeDB.allTheOwners;
        }

        public Owner ReadOwnerById(int id)
        {
            foreach (var owner in _owners)
            {
                if (owner.Id == id)
                {
                    return owner;
                }
            }
            return null;
        }

        public List<Owner> ReadOwners()
        {
            return _owners;
        }

        public Owner Update(Owner owner)
        {
            
            var dbOwner = this.ReadOwnerById(owner.Id);
            if (dbOwner != null)
            {
                dbOwner.FirstName = owner.FirstName;
                dbOwner.LastName = owner.LastName;
                dbOwner.PhoneNumber = owner.PhoneNumber;
                dbOwner.Email = dbOwner.Email;
                dbOwner.Address = dbOwner.Address;
                return dbOwner;
            }
            return null;
        }
    }
}
