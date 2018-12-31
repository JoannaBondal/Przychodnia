using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Przychodnia.Models;

namespace Przychodnia.Controllers
{
    [Authorize]
    public class WizytyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wizyty

        public ActionResult Index()
        {
            string userId = PobierzID();

            var d = db.Users.ToList();
           
            if (User.IsInRole("Pacjent"))
                return View(db.Wizyty.Where(s => s.Pacjent.Id == userId).ToList());
            else
                return View(db.Wizyty.Where(s => s.Lekarz.Id == userId).ToList());
        }

        private List<ApplicationUser> ZwrocLekarza()
        {
            return db.Users.Where(s=>s.Roles.Any(ss=>ss.RoleId=="1")).ToList();
        }

        public JsonResult ZwrocGodziny(string iDLekarza, DateTime dzien)
        {
            List<SelectListItem> godziny = new List<SelectListItem>
            {
                new SelectListItem{ Text= "8:00" ,Value="8:00" },
                new SelectListItem{ Text= "9:00", Value="9:00" },
                new SelectListItem{ Text= "10:00",Value="10:00" },
                new SelectListItem{ Text= "11:00",Value="11:00" },
                new SelectListItem{ Text= "12:00",Value="12:00" },
                new SelectListItem{ Text= "13:00",Value="13:00" },
                new SelectListItem{ Text= "14:00",Value="14:00" },
                new SelectListItem{ Text= "15:00",Value="15:00" },
                new SelectListItem{ Text= "16:00",Value="16:00" },
                new SelectListItem{ Text= "17:00",Value="17:00" }
            };
            if (dzien.DayOfWeek == DayOfWeek.Sunday || dzien.DayOfWeek == DayOfWeek.Saturday) return Json(new SelectList("", "Value", "Text"));

            var zajete = db.Wizyty.Where(s => s.Data == dzien && s.Lekarz.Id == iDLekarza);
            foreach (var z in zajete)
                godziny.Remove(godziny.FirstOrDefault(s => s.Value.Contains(z.Czas.Hour.ToString())));
            return Json(new SelectList(godziny, "Value", "Text"));
        }


        private string PobierzID()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.Claims.ToList();
            var userId = claims[0].Value;
            return userId;
        }

        // GET: Wizyty/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var d = db.Users.ToList();
            Wizyta wizyta = db.Wizyty.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            return View(wizyta);
        }


        // GET: Wizyty/Create
        [Authorize(Roles = "Pacjent")]
        public ActionResult Create()
        {
            ViewBag.Lekarze = ZwrocLekarza();
          //  ViewBag.Godziny = ZwrocGodziny();
            return View();
        }

        // POST: Wizyty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Pacjent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Data,Czas,RodzajWizyty,Lekarz")] Wizyta wizyta)
        {
            if (ModelState.IsValid)
            {
                var userId = PobierzID();
                var pacjent = db.Users.FirstOrDefault(s => s.Id == userId);
                wizyta.Pacjent = pacjent;
                wizyta.Lekarz = db.Users.FirstOrDefault(s=>s.Id==wizyta.Lekarz.Id);
                db.Wizyty.Add(wizyta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wizyta);
        }

        // GET: Wizyty/Edit/5
        [Authorize(Roles = "Pacjent")]
        public ActionResult Edit(int? id)
        {
            ViewBag.Lekarze = ZwrocLekarza();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wizyta wizyta = db.Wizyty.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            return View(wizyta);
        }

        // POST: Wizyty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pacjent")]
        public ActionResult Edit([Bind(Include = "ID,Data,Czas,RodzajWizyty,Lekarz")] Wizyta wizyta)
        {
            if (ModelState.IsValid)
            {
                var userId = PobierzID();
                var pacjent = db.Users.FirstOrDefault(s => s.Id == userId);
                wizyta.Pacjent = pacjent;
                wizyta.Lekarz = db.Users.FirstOrDefault(s => s.Id == wizyta.Lekarz.Id);
                db.Entry(wizyta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wizyta);
        }

        // GET: Wizyty/Delete/5
        [Authorize(Roles = "Pacjent")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var d = db.Users.ToList();
            Wizyta wizyta = db.Wizyty.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            return View(wizyta);
        }

        // POST: Wizyty/Delete/5
        [Authorize(Roles = "Pacjent")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wizyta wizyta = db.Wizyty.Find(id);
            db.Wizyty.Remove(wizyta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
