using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Notebook.DAL.Interfaces;
using Notebook.Domain.Entity;
using Notebook.Domain.Enums;
using Notebook.Domain.Responce;
using Notebook.Domain.ViewModel;
using Notebook.Service.Interfaces;

namespace Notebook.Service.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository repository;

        public PersonService(IPersonRepository repository)
        {
            this.repository = repository;
        }

        public async Task<BaseResponse<bool>> Create(PersonViewModel model)
        {
            BaseResponse<bool> response = new();
            try
            {
                Person person = new Person()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
                foreach (var phoneNumber in model.PhoneNumbers)
                {
                    phoneNumbers.Add(new PhoneNumber() { PhoneNumberValue = phoneNumber,
                                                         Person = person });
                }

                person.PhoneNumbers = phoneNumbers;
                
                response.Data = await repository.Create(person);
                response.StatusCode = StatusCode.OK;
                return response;

            }
            catch (Exception ex)
            {
                response.Description = $"[GetAll]" + ex.Message;
                response.StatusCode = StatusCode.Exception;
                response.Data = false;
                return response;
            }
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            BaseResponse<bool> response = new();
            try
            {
                Person person = await repository.GetPersonDeletePhones(id);
                if (person == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Data = false;
                    response.Description = $"[Delete] NotFound";
                    return response;
                }
                person.PhoneNumbers.Clear();

                response.Data = await repository.Delete(person);
                response.StatusCode = StatusCode.OK;
                return response;

            } 
            catch(Exception ex)
            {
                response.Description = $"[Delete]" + ex.Message;
                response.StatusCode = StatusCode.Exception;
                response.Data = false;
                return response;
            }
        }

        public async Task<BaseResponse<IEnumerable<Person>>> GetAll()
        {
            BaseResponse<IEnumerable<Person>> response = new();
            try
            {
                IEnumerable<Person> persons = await repository.GetAll();

                response.Data = persons;
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[GetAll]" + ex.Message;
                response.StatusCode = StatusCode.Exception;
                return response;
            }
        }

        public async Task<BaseResponse<bool>> Update(int id, PersonViewModel model)
        {
            BaseResponse<bool> response = new();
            try
            {
                Person person = await repository.GetPersonDeletePhones(id);

                if (person == null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Data = false;
                    response.Description = $"[GetAll] NotFound";
                    return response;
                }

                person.FirstName = model.FirstName;
                person.LastName = model.LastName;

                List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();
                foreach (var phoneNumber in model.PhoneNumbers)
                {
                    phoneNumbers.Add(new PhoneNumber()
                    {
                        PhoneNumberValue = phoneNumber,
                        Person = person
                    });
                }
                person.PhoneNumbers = phoneNumbers;

                response.Data = await repository.Update(id, person);
                response.StatusCode = StatusCode.OK;
                return response;
            }
            catch(Exception ex)
            {
                response.Description = $"[Update]" + ex.Message;
                response.StatusCode = StatusCode.Exception;
                response.Data = false;
                return response;
            }

        }
    }
}
