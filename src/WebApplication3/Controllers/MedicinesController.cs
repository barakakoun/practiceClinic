using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class MedicinesController : Controller
    {
        private ApplicationDbContext _context;

        public MedicinesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Medicines
        public IActionResult Index()
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");

            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }

            // If not manager - not allowed to see all the user, obiouslyyyyy!!!
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            return View(_context.Medicines.ToList());
        }

        // GET: Medicines/Details/5
        public IActionResult Details(int? id)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");

            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }

            // If not manager - not allowed to see all the user, obiouslyyyyy!!!
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            Medicine Medicine = _context.Medicines.SingleOrDefault(m => m.ID == id);
            // If not exist..
            if (Medicine == null)
            {
                return HttpNotFound();
            }

            return View(Medicine);
        }

        // GET: Medicines/Create
        public IActionResult Create()
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager creates meds
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            return View();
        }

        // POST: Medicines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Medicine Medicine)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager creates meds
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            if (ModelState.IsValid)
            {
                _context.Medicines.Add(Medicine);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Medicine);
        }

        // GET: Medicines/Edit/5
        public IActionResult Edit(int? id)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager edits meds
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            // If no parameter..
            if (id == null)
            {
                return HttpNotFound();
            }

            Medicine Medicine = _context.Medicines.SingleOrDefault(m => m.ID == id);
            // If not exist
            if (Medicine == null)
            {
                return HttpNotFound();
            }
            return View(Medicine);
        }

        // POST: Medicines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Medicine Medicine)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager can do this action
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            // If valid - save
            if (ModelState.IsValid)
            {
                _context.Update(Medicine);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Medicine);
        }

        // GET: Medicines/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager can do this action
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            // If no parameter..
            if (id == null)
            {
                return HttpNotFound();
            }

            Medicine Medicine = _context.Medicines.SingleOrDefault(m => m.ID == id);
            // If not exist
            if (Medicine == null)
            {
                return HttpNotFound();
            }

            return View(Medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager can do this action
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            Medicine Medicine = _context.Medicines.SingleOrDefault(m => m.ID == id);
            // If not exist
            if (Medicine == null)
            {
                return HttpNotFound();
            }
            // Delete and save
            _context.Medicines.Remove(Medicine);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
