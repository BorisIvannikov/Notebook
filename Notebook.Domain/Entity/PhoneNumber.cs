using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Domain.Entity
{
    public class PhoneNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneNumberId { get; set; }
        public string PhoneNumberValue { get; set;} = string.Empty;        
        public Person Person { get; set; }

    }
}
