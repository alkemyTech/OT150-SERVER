using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContact _contact;
       
        public ContactController(IContact contact)
        {
            _contact = contact;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("Contacts")]
        public IActionResult GetContacts()
        {
            return Ok(_contact.GetContacts());
        }

        [HttpPost("Contacts")]
        public async Task<IActionResult> PostContact([FromBody]ContactPostDto contact)
        {
         
            if (ModelState.IsValid)
            {
                
                

                return Ok(await _contact.PostContact(contact));

            }
            else
            {
                return BadRequest(ModelState);
              

            }
        }
    }


    }
