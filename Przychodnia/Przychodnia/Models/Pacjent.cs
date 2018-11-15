using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Przychodnia.Models
{
    public class Pacjent
    {
        [Key]
        public int ID_Pacjenta { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Pesel { get; set; } 
        public string Adres { get; set; }
        public string Plec { get; set; }
        public int Telefon { get; set; }
        public DateTime Data_ur { get; set; }

        public ApplicationUser Osoba { get; set; }
    }
}