using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProceduresController : Controller
    {
        private ApplicationDbContext _context;

        public ProceduresController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Procedures
        public IActionResult Index()
        {
            var query = _context.Procedures.GroupBy(p => p.PatientID);
            return View(_context.Procedures.ToList());
        }

        // GET: Procedures/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedures.Single(m => m.ID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }

            return View(procedure);
        }

        // GET: Procedures/Create
        public IActionResult Create(int? PatientID, int? ProcedureTypeID)
        {
            if (PatientID != null)
            {
                ViewBag.PatientID = new SelectList(_context.Patients, "ID", "FirstName", PatientID);
            }
            else
            {
                ViewBag.PatientID = new SelectList(_context.Patients, "ID", "FirstName");
            }

            if (ProcedureTypeID != null)
            {
                ViewBag.ProcedureTypeID = new SelectList(_context.ProcedureTypes, "ID", "Name", ProcedureTypeID);
            }
            else
            {
                ViewBag.ProcedureTypeID = new SelectList(_context.ProcedureTypes, "ID", "Name");
            }

            return View();
        }

        // POST: Procedures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                _context.Procedures.Add(procedure);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedure);
        }

        // GET: Procedures/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedures.Single(m => m.ID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        // POST: Procedures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                _context.Update(procedure);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedure);
        }

        // GET: Procedures/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedures.Single(m => m.ID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }

            return View(procedure);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Procedure procedure = _context.Procedures.Single(m => m.ID == id);
            _context.Procedures.Remove(procedure);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
