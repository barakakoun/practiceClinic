using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;
using System.Collections.Generic;
using System;

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
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // If not manager is trying to watch index which is not his
            if ((pLogged.ID != 1) && (nPatient != pLogged.ID))
            {
                return RedirectToAction("PermissionError", "Home");
            }

            var allProcedures = _context.Procedures.Include(p => p.ProcedureType).Include(p => p.Patient).ToList();

            if (nPatient == null)
            {
                return View(allProcedures);
            }

            ViewData["nCurrPatient"] = nPatient;
            //ViewBag.nCurrPatient = nPatient;

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

            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");

            Procedure procedure = _context.Procedures.Include(p => p.ProcedureType).Include(p => p.Patient).SingleOrDefault(m => m.ID == id);

            if (procedure == null)
            {
                return RedirectToAction("NotExistError", "Home");
            }
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // If regular user is trying to watch a procedure which is not his
            // Or unlogged user is trying to access
            if ((pLogged.ID != 1) && (procedure.PatientID != pLogged.ID))
            {
                return RedirectToAction("PermissionError", "Home");
            }

            return View(procedure);
        }

        // GET: Procedures/Create
        public IActionResult Create(int? PatientID, int? ProcedureTypeID)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            // Build list of patients the user can choose one to schedule for
            List<object> newPatList = new List<object>();
            foreach (var ptCurr in _context.Patients)
                newPatList.Add(new
                {
                    ID = ptCurr.ID,
                    Name = ptCurr.FirstName + " " + ptCurr.LastName + " (" + ptCurr.Identifier + ")"
                });

            // If theres chosen patient, set him as chosen
            if (PatientID != null)
            {
                ViewBag.PatientID = new SelectList(newPatList, "ID", "Name", PatientID);
            }
            else
            {
                ViewBag.PatientID = new SelectList(newPatList, "ID", "Name");
            }

            // If theres chosen proc, set him as chosen
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
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager creates procedures
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }


            if (ModelState.IsValid)
            {
                _context.Procedures.Add(procedure);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            int PatientID = procedure.PatientID, ProcedureTypeID = procedure.ProcedureTypeID;

            // IF NOT VALID
            // Same as the get function, set the values
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
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager edits procedures
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            // No parameter
            if (id == null)
            {
                return HttpNotFound();
            }


            Procedure procedure = _context.Procedures.Include(p => p.ProcedureType).Include(p => p.Patient).SingleOrDefault(m => m.ID == id);
            // If the proc isnt exist
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
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager edits procedures
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            // If valid, save..
            if (ModelState.IsValid)
            {
                _context.Update(procedure);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedure);
        }

        // GET
        [ActionName("Search")]
        public IActionResult Search()
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");

            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }



            //var patients = _context.Patients.Include(p => p.MedicineAllergies);

            //ViewBag.minPrice = "";
            //ViewBag.maxPrice = "";

            //var allergies = _context.Medicines.Select(c => new {
            //    ID = c.ID,
            //    Name = c.Name
            //}).ToList();

            //ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name");


            var allProcedures = _context.Procedures.Include(p => p.ProcedureType).Include(p => p.Patient).ToList();

            if (pLogged.ID == 1)
            {
                return View(allProcedures);
            }

            return View(allProcedures.Where(p => p.PatientID == pLogged.ID).ToList());
        }

        // POST
        [HttpPost, ActionName("Search")]
        [ValidateAntiForgeryToken]
        public IActionResult Search(int minPrice, int maxPrice, string pDate, string unPaid)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            DateTime dt;
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }

            if (minPrice != 0)
            {
                ViewBag.minPrice = minPrice;
            }
            if ((maxPrice != 0) && (maxPrice != 999999))
            {
                ViewBag.maxPrice = maxPrice;
            }

            var allProcs = _context.Procedures.Include(p => p.ProcedureType).Include(p => p.Patient);

            var toReturn = new List<Procedure>();
            // If the searcher is not the manager, you should only show his procs
            if (pLogged.ID != 1)
            {
                toReturn = allProcs.Where(p => (p.Price <= maxPrice) && (p.Price >= minPrice) && (p.PatientID == pLogged.ID)).ToList();
            }
            else
            {
                toReturn = allProcs.Where(p => (p.Price <= maxPrice) && (p.Price >= minPrice)).ToList();
            }
            

            if (pDate != null)
            {
                ViewBag.pDate = pDate;
                dt = Convert.ToDateTime(pDate);
                toReturn = toReturn.Where(p => p.Time.Date.CompareTo(dt) == 0).ToList();
            }

            ViewBag.unPaid = unPaid;
            if (unPaid == "on")
            {
                toReturn = toReturn.Where(p => p.IsPaid == false).ToList();
            }

            return View(toReturn);
        }

        // GET: Procedures/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager edits procedures
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            Procedure procedure = _context.Procedures.SingleOrDefault(m => m.ID == id);
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
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            // If no user logged in
            if (pLogged == null)
            {
                return RedirectToAction("NotLoggedError", "Home");
            }
            // Only manager edits procedures
            if (pLogged.ID != 1)
            {
                return RedirectToAction("PermissionError", "Home");
            }

            // Delete..
            Procedure procedure = _context.Procedures.SingleOrDefault(m => m.ID == id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            _context.Procedures.Remove(procedure);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
