using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;

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

    }
}
