using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ContactBusiness : IContact
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEmailBusiness _emailBusiness;
      
        private readonly EntityMapper _entityMapper = new EntityMapper();
        public ContactBusiness(IUnitOfWork unitOfWork, IEmailBusiness emailBusiness)
        {
            _unitOfWork = unitOfWork;
            _emailBusiness = emailBusiness;

   
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
        public async Task<Response<ContactPostDto>> PostContact(ContactPostDto contactPostDto)
        {


            var response = new Response<ContactPostDto>();


            var contact = _entityMapper.ContactPostDtoToContactsModel(contactPostDto);





            
                _unitOfWork.ContactsModelRepository.Add(contact);
                _unitOfWork.SaveChanges();
           
            

              
                await _emailBusiness.SendEmailWithTemplateAsync(contactPostDto.Email, $"El contacto fue exitoso", $"¡Muchas gracias {contact.Name}!", "Ong Somos Más");

            response.Data = contactPostDto;
            response.Succeeded = true;
            response.Message = "The contact was added successfully";
            return response;
            


            
           
         
        }
       

    }
}
