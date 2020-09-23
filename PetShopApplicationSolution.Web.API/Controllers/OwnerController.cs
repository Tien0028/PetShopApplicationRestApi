using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShopApplication.Core.ApplicationService;
using PetShopApplication.Core.Entities;

namespace PetShopApplicationSolution.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IPetService _petService;


        public OwnerController(IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
        }

        // GET: api/<OwnerController>
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] FilterModel filter)
        {
            if(string.IsNullOrEmpty(filter.SearchTerm) && string.IsNullOrEmpty(filter.SearchValue))
            {
                try
                {
                    return _ownerService.GetAllOwners();
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
            else
            {
                if(string.IsNullOrEmpty(filter.SearchTerm) || string.IsNullOrEmpty(filter.SearchValue))
                {
                    return StatusCode(500, "This again?! For real? Try putting a SearchTerm and a Searchvalue, for once!");
                }
                else
                {
                    try
                    {
                        return _ownerService.SearchByOwner(filter);
                    }
                    catch(Exception e)
                    {
                        return NotFound(e.Message);
                    }
                }
            }
        }

        // GET: api/<OwnerController>
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            return _ownerService.FindOwnerById(id);
        }


        // POST api/<OwnerController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            if (string.IsNullOrEmpty(owner.FirstName))
            {
                return BadRequest("Error: Check First name");
            }
            if (string.IsNullOrEmpty(owner.LastName))
            {
                return BadRequest("Error: Check last name");
            }
            if (string.IsNullOrEmpty(owner.Address))
            {
                return BadRequest("Error: Check Address");
            }
            if (string.IsNullOrEmpty(owner.PhoneNumber)) 
            {
                return BadRequest("Error: Check Phone Number");
            }
            _ownerService.CreateOwner(owner);
            return StatusCode(500, "Yay, a new owner");
        }

        // PUT api/<OwnerController>/
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            if(id != owner.Id || id == 0)
            {
                return StatusCode(500, "Your ids have to be the same, otherwise, this is going south, real quick!");
            }
            if(string.IsNullOrEmpty(owner.FirstName) || string.IsNullOrEmpty(owner.LastName) || string.IsNullOrEmpty(owner.Address) || string.IsNullOrEmpty(owner.PhoneNumber) || string.IsNullOrEmpty(owner.Email))
            {
                return StatusCode(500, "You're missing data, try entering all that's needed.");
            }
            try
            {
                return _ownerService.UpdateOwner(owner);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
             _ownerService.DeleteOwner(id);
            return Ok($"Owmer with the id:" + id + "deleted.");   

        }
    }
}
