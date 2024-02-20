using Azure;
using Microsoft.AspNetCore.Mvc;
using Notebook.DAL;
using Notebook.Domain.Entity;
using Notebook.Domain.Responce;
using Notebook.Domain.ViewModel;
using Notebook.Service.Interfaces;

namespace Notebook.Controllers
{
    
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [Route("api/persons")]
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPersons()
        {
            var response = await personService.GetAll();
            if (response.StatusCode != Domain.Enums.StatusCode.OK)
            {
                throw new Exception(response.Description);
                //return new[] { new Person() }; /*можно возвращать пустой массив персон*/
            }
            var result = response.Data.ToList();
            
            return response.Data.ToList();
        }

        [Route("api/persons")]
        [HttpPost]
        public async Task<bool> Create(PersonViewModel model)
        {
            var response = await personService.Create(model);
            if (response.StatusCode != Domain.Enums.StatusCode.OK)
            {
                throw new Exception(response.Description);
                //return false; /* можно возвращать фолс на фронт*/
            }
            var result = response.Data;

            return result;
        }

        [Route("api/persons/{id}")]
        [HttpPut]
        public async Task<bool> Update(int id, PersonViewModel model)
        {
            var response = await personService.Update(id, model);
            if(response.StatusCode != Domain.Enums.StatusCode.OK)
            {
                throw new Exception(response.Description);
                //return false; /* можно возвращать фолс на фронт*/
            }
            var result = response.Data;

            return result;
        }

        [Route("api/persons/{id}")]
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            var response = await personService.Delete(id);
            if (response.StatusCode != Domain.Enums.StatusCode.OK)
            {
                throw new Exception(response.Description);
                //return false; /* можно возвращать фолс на фронт*/
            }
            var result = response.Data;

            return result;
        }
    }
}
