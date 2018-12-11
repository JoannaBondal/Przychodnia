using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Przychodnia.Models
{
    public class Wizyta
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Data wizyty")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        
        public string RodzajWizyty { get; set; }
        

        public ApplicationUser Pacjent { get; set; }
        public ApplicationUser Lekarz { get; set; }
    }
}