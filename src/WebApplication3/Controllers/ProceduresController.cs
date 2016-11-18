using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;
using System.Collections.Generic;

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
        public IActionResult Index(int? nPatient)
        {
            var allProcedures = _context.Procedures.Include(p => p.ProcedureType).Include(p => p.Patient).ToList();

            if (nPatient == null)
            {
                return View(allProcedures);
            }
            ViewData["nCurrPatient"] = nPatient;
            ViewBag.nCurrPatient = nPatient;

            // Add the object of the client so we can read it's name
            return View(allProcedures.Where(p => p.PatientID == nPatient));
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
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            if ((pLogged == null) ||
                (pLogged.ID != 1))
            {
                return RedirectToAction("PermissionError", "Home");
            }

            List<object> newPatList = new List<object>();
            foreach (var ptCurr in _context.Patients)
                newPatList.Add(new
                {
                    ID = ptCurr.ID,
                    Name = ptCurr.FirstName + " " + ptCurr.LastName + " (" + ptCurr.Identifier + ")"
                });

            if (PatientID != null)
            {
                ViewBag.PatientID = new SelectList(newPatList, "ID", "Name", PatientID);
            }
            else
            {
                ViewBag.PatientID = new SelectList(newPatList, "ID", "Name");
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

            int PatientID = procedure.PatientID, ProcedureTypeID = procedure.ProcedureTypeID;
            List<object> newPatList = new List<object>();
            foreach (var ptCurr in _context.Patients)
                newPatList.Add(new
                {
                    ID = ptCurr.ID,
                    Name = ptCurr.FirstName + " " + ptCurr.LastName + " (" + ptCurr.Identifier + ")"
                });

            if (PatientID != 0)
            {
                ViewBag.PatientID = new SelectList(newPatList, "ID", "Name", PatientID);
            }
            else
            {
                ViewBag.PatientID = new SelectList(newPatList, "ID", "Name");
            }

            if (ProcedureTypeID != 0)
            {
                ViewBag.ProcedureTypeID = new SelectList(_context.ProcedureTypes, "ID", "Name", ProcedureTypeID);
            }
            else
            {
                ViewBag.ProcedureTypeID = new SelectList(_context.ProcedureTypes, "ID", "Name");
            }


            return View(procedure);
        }

        // GET: Procedures/Edit/5
        public IActionResult Edit(int? id)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            if ((pLogged == null) ||
                (pLogged.ID != 1))
            {
                return RedirectToAction("PermissionError", "Home");
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var proceduretry =
                from it in _context.Procedures
                where it.ID.Equals(id)
                select it;
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
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            if ((pLogged == null) ||
                (pLogged.ID != 1))
            {
                return RedirectToAction("PermissionError", "Home");
            }

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
