using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Przychodnia.Models;

namespace Przychodnia.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RejestracjaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rejestracja
        public ActionResult Index()
        {
            return View(db.Users.Where(s=>s.Roles.Any(ss=>ss.RoleId=="2")).ToList());
        }

        // GET: Rejestracja/Details/5
        public ActionResult ZmienNaLekarz(string id)
        {
            if (id == null)
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            using (var db = new ApplicationDbContext())
            {
                var user =db.Users.FirstOrDefault(s => s.Id == id);

                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                userManager.AddToRole(user.Id, "Lekarz");
                userManager.RemoveFromRole(user.Id, "Pacjent");

            }
            return RedirectToAction("Index");
        }

     
    }
}
