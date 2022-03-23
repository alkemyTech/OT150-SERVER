using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IContact
    {
        IEnumerable<ContactDto> GetContacts();
        Task<Response<ContactPostDto>> PostContact(ContactPostDto contactPostDto);
    }
}
