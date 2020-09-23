using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetShopApplication.Core.ApplicationService;
using PetShopApplication.Core.ApplicationService.Impl;
using PetShopApplication.Core.Entities;

namespace PetShopApplicationSolution.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IOwnerService _ownerService;
        public PetController(IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] FilterModel filter)
        {
            if(string.IsNullOrEmpty(filter.SearchTerm) && string.IsNullOrEmpty(filter.SearchValue))
            {
                try
                {
                    return _petService.ListAllPets();
                }
                catch(Exception e)
                {
                    return NotFound(e.Message);
                }

            }
            else
            {
                if(string.IsNullOrEmpty(filter.SearchTerm) || string.IsNullOrEmpty(filter.SearchValue))
                {
                    return StatusCode(500, "Try enterign both a SearchTerm and a SearchValue, then try again");
                }
                else
                {
                    try
                    {
                        List<Pet> allPets = _petService.SearchForPet(filter);
                        if(allPets.Count < 1)
                        {
                            return NotFound("No pets with those parameters, moron");
                        }
                        else
                        {
                            return allPets;
                        }
                    }
                    catch(Exception e)
                    {
                        return NotFound(e.Message);
                    }
                }
            }
           
        }

        [HttpGet("{petId}")]
        public ActionResult<Pet> Get(int petId)
        {
            try
            {
                return Ok(_petService.FindPetById(petId));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            if (string.IsNullOrEmpty(pet.Name))
            {
                return BadRequest("Error, Error, Error! Check Name. Error!");
            }

            if (pet.Price <= 0 || pet.Price.Equals(null))
            {
                return BadRequest("Error, Error, Error! Check the Price, Moron. Error!");
            }
            _petService.Create(pet);
            return StatusCode(500, $"Yay Pet"  +  pet.Name  +  $"has been bred");
        }

        [HttpPut("{id}")] 
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            try
            {
                if(pet.Id !=id)
                {
                    return BadRequest("Ids of both parameter and pet must match!");
                }
                return Accepted(_petService.Update(pet));
            }
            catch(System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        // DELETE api/<PetController>/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            try
            {
                Pet PetForDeletion = _petService.FindPetById(id);
                _petService.Delete(PetForDeletion);
                return Ok(PetForDeletion.Name + " pet has been deleted.");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        

        
    }
}


 
