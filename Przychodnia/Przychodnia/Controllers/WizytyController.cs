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
                     
            if (User.IsInRole("Pacjent"))
                return View(db.Wizyty.Where(s => s.Pacjent.Id == userId).ToList());
            else
                return View(db.Wizyty.Where(s => s.Lekarz.Id == userId).ToList());
        }

        private List<ApplicationUser> ZwrocLekarza()
        {
            return db.Users.Where(s=>s.Roles.Any(ss=>ss.RoleId=="1")).ToList();
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
            return View();
        }

        // POST: Wizyty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Pacjent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Data,RodzajWizyty,Lekarz")] Wizyta wizyta)
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
        public ActionResult Edit([Bind(Include = "ID,Data,RodzajWizyty,Lekarz")] Wizyta wizyta)
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
