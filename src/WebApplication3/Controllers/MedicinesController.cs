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
            return View(_context.Medicines.ToList());
        }

        // GET: Medicines/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Medicine Medicine = _context.Medicines.Single(m => m.ID == id);
            if (Medicine == null)
            {
                return HttpNotFound();
            }

            return View(Medicine);
        }

        // GET: Medicines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Medicine Medicine)
        {
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
            if (id == null)
            {
                return HttpNotFound();
            }

            Medicine Medicine = _context.Medicines.Single(m => m.ID == id);
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
            if (id == null)
            {
                return HttpNotFound();
            }

            Medicine Medicine = _context.Medicines.Single(m => m.ID == id);
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
            Medicine Medicine = _context.Medicines.Single(m => m.ID == id);
            _context.Medicines.Remove(Medicine);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
