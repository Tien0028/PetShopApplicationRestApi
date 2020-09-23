using PetShopApplication.Core.ApplicationService;
using PetShopApplication.Core.ApplicationService.Impl;
using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopApplication.Infrastructure.Static.Data
{
    public class FakeDB
    {
        public static IEnumerable<Pet> allThePets { get; set; }
        public static IEnumerable<Owner> allTheOwners { get; set; }
        public static IEnumerable<PetType> allThePetTypes { get; set; }

        public static int thePetCount { get; set; }
        public static int theOwnerCount { get; set; }
        public static int thePetTypeCount { get; set; }

        private static IOwnerRepository _ownerRepo;
        private static IPetRepository _petRepo;
        private static IPetTypeRepository _petTypeRepo;
        public FakeDB(IOwnerRepository ownerRepository, IPetRepository petRepository, IPetTypeRepository petTypeRepository)
        {

            _ownerRepo = ownerRepository;
            _petRepo = petRepository;
            _petTypeRepo = petTypeRepository;
        }

        public string InitData()
        {
            List<PetType> allPetTypes = new List<PetType>
            {
                new PetType {NameOfPetTypes = "Dog"},
                new PetType {NameOfPetTypes = "Cat"},
                new PetType {NameOfPetTypes = "Snake"},
                new PetType {NameOfPetTypes = "Hawk"},
                new PetType {NameOfPetTypes = "Lion"},
                new PetType {NameOfPetTypes = "Goat"},
                new PetType {NameOfPetTypes = "Lizard"},
                new PetType {NameOfPetTypes = "Rabbit"},
                new PetType {NameOfPetTypes = "Dragon"},
                new PetType {NameOfPetTypes = "Turtle"}

            };
            List<Owner> allOwners = new List<Owner>
            {
                new Owner {FirstName ="Jack", LastName = "Davidson", Address = "Deadwell 3, 6600 Vejen", 
                    PhoneNumber = "3333 44444", Email = "jack@hotmail.com"},
                new Owner {FirstName ="Peter", LastName = "Parker", Address = "Pride 69, 6969 Someplace",
                    PhoneNumber = "6969 6969", Email = "peter@hmail.com"},
                new Owner {FirstName ="Clark", LastName = "Kent", Address = "Shittington 3, Smallville",
                    PhoneNumber = "DC Comics 45", Email = "Superman@justicemail.com"},
                new Owner {FirstName ="Tony", LastName = "Stark", Address = "ExpensivePlace 4, Malibu State",
                    PhoneNumber = "I AM IRONMAN 10", Email = "tonyStark@avengers.mail"},
                new Owner {FirstName ="Steve", LastName = "Rogers", Address = "Brooklyn 4, 1918",
                    PhoneNumber = "America 5504", Email = "capAmerica@avngers.mail"},
                new Owner {FirstName ="Natasha", LastName = "Romanoff", Address = "Red Room, Russia",
                    PhoneNumber = "0000 0000", Email = "RedWidow@SHIELDmail.com"},
                new Owner {FirstName ="Bruce", LastName = "Wayne", Address = "Wayne Manor, Gotham City",
                    PhoneNumber = "1234 5678", Email = "Batman@justicemail.com"},
                new Owner {FirstName ="Arthur", LastName = "Curry", Address = "Coral Reef, Atlantis",
                    PhoneNumber = "9012 3456", Email = "Aquaman@justicemail.com"}
            };

            List<Pet> allPets = new List<Pet>
            {

                new Pet{Name = "Dante", Birthdate = new DateTime(2013, 7, 8), SoldDate = new DateTime(2014, 7, 8),
                    previousOwner = "Uriel Sorensson", Price = 250, PetType = allPetTypes[0], PetOwner = allOwners[0]},
                new Pet{Name = "Vergil", Birthdate = new DateTime(2015, 2, 4), SoldDate = new DateTime(2016, 2, 4),
                    previousOwner = "Bruce Wayne", Price = 750, PetType = allPetTypes[1], PetOwner = allOwners[1]},
                new Pet{Name = "Lady", Birthdate = new DateTime(2014, 5, 2), SoldDate = new DateTime(2016, 2, 4),
                    previousOwner = "Loki Odinson", Price = 360, PetType = allPetTypes[2], PetOwner = allOwners[2]},
                new Pet{Name = "Trish", Birthdate = new DateTime(2012, 6, 4), SoldDate = new DateTime(2012, 7, 3),
                    previousOwner = "Urizen Sparkles", Price = 70, PetType = allPetTypes[3], PetOwner = allOwners[3]},
                new Pet{Name = "Irish", Birthdate = new DateTime(2011, 4, 6), SoldDate = new DateTime(2013, 3, 1),
                    previousOwner = "Simba", Price = 120, PetType = allPetTypes[4], PetOwner = allOwners[4]},
                new Pet{Name = "Barry", Birthdate = new DateTime(1920, 4, 6), SoldDate = new DateTime(2018, 3, 1),
                    previousOwner = "Henry Allen", Price = 30, PetType = allPetTypes[5], PetOwner = allOwners[5]},
                new Pet{Name = "OLiver", Birthdate = new DateTime(1970, 5, 9), SoldDate = new DateTime(2019, 2, 3),
                    previousOwner = "Moira", Price = 306, PetType = allPetTypes[6], PetOwner = allOwners[6]},
            };
            foreach (var pet in allPets)
            {
                _petRepo.Create(pet);
            }
            foreach (var owner in allOwners)
            {
                _ownerRepo.Create(owner);
            }
            foreach (var petType in allPetTypes)
            {
                _petTypeRepo.AddNewPetType(petType);
            }
            return "Data has been made.";
        }
        public static Owner DeleteOwner(Owner ownerForDeletion)
        {
            allTheOwners = allTheOwners.Where(owner => owner != ownerForDeletion);
            allThePets = allThePets.Where(pet => pet.PetOwner != ownerForDeletion);
            return ownerForDeletion;
        }
        public static Owner AddNewOwner (Owner newOwner)
        {
            if(theOwnerCount == 0)
            {
                theOwnerCount++;
            }
            newOwner.Id = theOwnerCount;
            theOwnerCount++;
            List<Owner> theNewOwner = new List<Owner> { newOwner };
            if(allTheOwners == null)
            {
                allTheOwners = theNewOwner;
            }
            else
            {
                allTheOwners = allTheOwners.Concat(theNewOwner);
            }
            return newOwner;
        }
        public static Pet DeletePet(Pet petForDeletion)
        {
            allThePets = allThePets.Where(pet => pet != petForDeletion);
            return petForDeletion;
        }

        public static PetType AddNewPetType(PetType newPetType)
        {
            if (thePetTypeCount == 0)
            {
                thePetTypeCount++;
            }
            newPetType.IdOfPetTypes = thePetTypeCount;
            thePetTypeCount++;
            List<PetType> theNewPetType = new List<PetType> { newPetType };
            if (allThePetTypes == null)
            {
                allThePetTypes = theNewPetType;
            }
            else
            {
                allThePetTypes = allThePetTypes.Concat(theNewPetType);
            }
            return newPetType;
        }
        public static PetType DeletePetType(PetType petTypeForDelection)
        {
            allThePets = allThePets.Where(pet => pet.PetType != petTypeForDelection);
            allThePetTypes = allThePetTypes.Where(petType => petType != petTypeForDelection);
            return petTypeForDelection;
        }
        
        public static Pet AddNewPet(Pet newPet)
        {
            if(thePetCount == 0)
            {
                thePetCount++;
            }
            newPet.Id = thePetCount;
            thePetCount++;
            List<Pet> theNewPet = new List<Pet> { newPet };
            if (allThePets == null)
            {
                allThePets = theNewPet;
            }
            else
            {
                allThePets = allThePets.Concat(theNewPet);
            }
            return newPet;
        }

        public static PetType UpdatePetType(PetType newPetType, PetType oldPetType)
        {
            oldPetType.NameOfPetTypes = newPetType.NameOfPetTypes;
            return oldPetType;
        }

    }
}
