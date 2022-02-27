using OngProject.Core.Models.DTOs;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface IContact
    {
        IEnumerable<ContactDto> GetContacts();
    }
}
