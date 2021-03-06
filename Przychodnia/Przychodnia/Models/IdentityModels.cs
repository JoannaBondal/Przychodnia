﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Przychodnia.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
        public int Test { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Wizyta> Wizyty { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set; }
        public DbSet<Lekarz> Lekarze { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

     //   public System.Data.Entity.DbSet<Przychodnia.Models.ApplicationUser> ApplicationUsers { get; set; }

        //  public System.Data.Entity.DbSet<Przychodnia.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}