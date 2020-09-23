using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShopApplication.Core.ApplicationService;
using PetShopApplication.Core.Entities;

namespace PetShopApplicationSolutionRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;
        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        // GET: api/<PetTypeController>
        [HttpGet]
        public ActionResult<List<PetType>> Get([FromQuery] FilterModel filter)
        {
            if (string.IsNullOrEmpty(filter.SearchTerm) && string.IsNullOrEmpty(filter.SearchValue))
            {
                try
                {
                    return Ok(_petTypeService.ListPetTypes());
                    
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(filter.SearchTerm) || string.IsNullOrEmpty(filter.SearchValue))
                {
                    return BadRequest("You need to enter both a SearchTerm and a SearchValue");
                }
                else
                {
                    try
                    {
                        return Ok(_petTypeService.SearchForPetType(filter));
                            
                            
                    }
                    catch (Exception e)
                    {
                        return NotFound(e.Message);
                    }
                }
            }
        }


        // GET api/<TypePetControllerr>/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            try
            {
                return _petTypeService.FindPetTypeByIdwithPets(id);
                    ////FindPetTypeById(id);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
                
        }

        // POST api/<TypePetControllerr>
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType petType)
        {
            if (string.IsNullOrEmpty(petType.NameOfPetTypes))
            {
               BadRequest("Another Error! Seriously?! Check it!");
            }
            else
            {
                try
                {
                    return _petTypeService.AddNewPetType(petType);
                }
                catch(Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            

            return StatusCode(500, "Noob! You made a new Pet Type.");
        }

        // PUT api/<TypePetControllerr>/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType petType)
        {
            if (id < 0 || petType.IdOfPetTypes != id)
            {
                return BadRequest("ID Error! Please check id, make sure they match!");
            }
            _petTypeService.UpdatePetType(petType);
            return StatusCode(500, "Pet Type"+ petType.NameOfPetTypes + "has been updated.");
        }

        // DELETE api/<PetTypeController>/5
        [HttpDelete("{id}")]
        public ActionResult<PetType> Delete(int id)
        {
            try
            {
                _petTypeService.DeletePetType(id);
                return Accepted("The pet type with the id:" + id + "has been deleted");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
