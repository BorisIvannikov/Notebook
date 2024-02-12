using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Delete(T entity);
        Task<IEnumerable<T>> GetAll();
    }
}
