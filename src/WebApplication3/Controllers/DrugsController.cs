using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DrugsController : Controller
    {
        private ApplicationDbContext _context;

        public DrugsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Drugs
        public IActionResult Index()
        {
            return View(_context.Drugs.ToList());
        }

        // GET: Drugs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Drug drug = _context.Drugs.Single(m => m.ID == id);
            if (drug == null)
            {
                return HttpNotFound();
            }

            return View(drug);
        }

        // GET: Drugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drugs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Drug drug)
        {
            if (ModelState.IsValid)
            {
                _context.Drugs.Add(drug);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drug);
        }

        // GET: Drugs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Drug drug = _context.Drugs.Single(m => m.ID == id);
            if (drug == null)
            {
                return HttpNotFound();
            }
            return View(drug);
        }

        // POST: Drugs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Drug drug)
        {
            if (ModelState.IsValid)
            {
                _context.Update(drug);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drug);
        }

        // GET: Drugs/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Drug drug = _context.Drugs.Single(m => m.ID == id);
            if (drug == null)
            {
                return HttpNotFound();
            }

            return View(drug);
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Drug drug = _context.Drugs.Single(m => m.ID == id);
            _context.Drugs.Remove(drug);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
