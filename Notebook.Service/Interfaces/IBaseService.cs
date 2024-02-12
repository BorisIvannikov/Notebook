using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Domain.Entity;
using Notebook.Domain.Responce;
using Notebook.Domain.ViewModel;

namespace Notebook.Service.Interfaces
{
    public interface IBaseService
    {        
        public Task<BaseResponse<bool>> Create(PersonViewModel model);
        public Task<BaseResponse<IEnumerable<Person>>> GetAll();
        public Task<BaseResponse<bool>> Update(int id, PersonViewModel model);
        public Task<BaseResponse<bool>> Delete(int id);
    }
}
