using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notebook.Domain.Entity;

namespace Notebook.Domain.ViewModel
{
    public class PersonViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public List<string> PhoneNumbers { get; set; }
    }
}
