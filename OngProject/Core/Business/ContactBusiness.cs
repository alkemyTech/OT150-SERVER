using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class ContactBusiness : IContact
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;
        public ContactBusiness(IUnitOfWork UnitOfWork, EntityMapper EntityMapper)
        {
            _unitOfWork = UnitOfWork;
            _entityMapper = EntityMapper;
        }
        public IEnumerable<ContactDto> GetContacts()
        {
            var contacts = _unitOfWork.ContactsModelRepository.GetAll();
            var contactsDto = new List<ContactDto>();

            foreach (var contact in contacts)
            {
                contactsDto.Add(_entityMapper.ConctactListDtoContactModel(contact));
            }

            return contactsDto;
        }
    }
}
