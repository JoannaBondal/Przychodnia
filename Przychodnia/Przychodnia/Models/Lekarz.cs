using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Przychodnia.Models
{
    public class Lekarz
    {
        [Key]
        public int ID_Lekarza { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set;}
        public int Telefon { get; set; }

        public ApplicationUser Osoba { get; set; }

    }
}