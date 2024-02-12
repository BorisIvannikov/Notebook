using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Domain.Entity;

namespace Notebook.DAL.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person?> GetPersonDeletePhones(int id);
    }
}
