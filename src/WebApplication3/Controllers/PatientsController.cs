using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;
using WebApplication3.ActionFilters;
using WebApplication3.Services;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System;

namespace WebApplication3.Controllers
{
    [NewActionFilters]
    public class PatientsController : Controller
    {
        private ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Patients
        public IActionResult Index()
        {
            //var query = _context.Patients.GroupBy(p => p.Birthday);
            var patients = _context.Patients.Include(p => p.MedicineAllergies);
            return View(patients.ToList());
        }

        // GET: Patients/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            //Patient patient = _context.Patients.Include(d => d.PatientDrugs).Include(d => d.Prucedures).Single(m => m.ID == id);
            Patient patient = _context.Patients.Include(d => d.Prucedures).Single(m => m.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            //ViewBag.ProcedureTypeID = new SelectList(_context.ProcedureTypes, "ID", "Name", ProcedureTypeID);
            var list = _context.Drugs.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var categories = _context.Drugs.Select(c => new {
                CategoryID = c.ID,
                CategoryName = c.Name
            }).ToList();
            ViewBag.Categories = new MultiSelectList(categories, "CategoryID", "CategoryName");

            var allergies = _context.Medicines.Select(c => new {
                ID = c.ID,
                Name = c.Name
            }).ToList();

            ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name");

            Patient tryhi = new Patient();

            //List<spGetTagsOfDocument_Result> tags = db.spGetTagsOfDocument(documentId).ToList();

            //foreach (spGetTagsOfDocument_Result tag in tags)
            //{
            //    model.selectedTags.Add(tag.TagName);
            //}



            return View(tryhi);
            //return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient, string arrAllergies)
        {
            if (_context.Patients.Any())
            {
                bool isExist;
                //isExist = _context.Patients.Any(m => m.ID == patient.ID);
                //if (tmpPatient != null)
                //{
                //    ModelState.AddModelError(string.Empty, "User with the same ID is already exist");
                //    return View(patient);
                //}

                isExist = _context.Patients.Any(m => m.Email == patient.Email);
                if (isExist)
                {
                    ModelState.AddModelError(string.Empty, "User with the same email is already exist");
                    return View(patient);
                }

                isExist = _context.Patients.Any(m => m.Phone == patient.Phone);
                if (isExist)
                {
                    ModelState.AddModelError(string.Empty, "User with the same phone is already exist");
                    return View(patient);
                }
            }

            if (ModelState.IsValid)
            {
                if (arrAllergies != null)
                {
                    IEnumerable<int> lstMedsIds = arrAllergies.Split(',').Select(curr => Int32.Parse(curr));

                    List<Medicine> med = _context.Medicines.Where(t => lstMedsIds.Contains(t.ID)).ToList();

                    patient.MedicineAllergies = med;
                }
                _context.Patients.Add(patient);
                _context.SaveChanges();
                if (Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient") == null)
                {
                    Services.SessionExtensions.SetObjectAsJson(HttpContext.Session, "patient", patient);
                }
                return RedirectToAction("Index", "Home");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return HttpNotFound();
            //}

            //var PatientsViewModel = new PatientsViewModel
            //{
            //    Patient = _context.Patients.Include(i => i.DrugAllergies).First(i => i.ID == id),
            //    //Patient = _context.Patients.Include(i => i.PatientDrugs).First(i => i.ID == id),
            //};

            //if (PatientsViewModel.Patient == null)
            //    return HttpNotFound();

            //var allDrugAllergiesList = _context.Drugs.ToList();
            //PatientsViewModel.AllDrugAllergies = allDrugAllergiesList.Select(o => new SelectListItem
            //{
            //    Text = o.Name,
            //    Value = o.ID.ToString()
            //});

            ////ViewBag.EmployerID =
            ////        new SelectList(db.Employers, "Id", "FullName", jobpostViewModel.JobPost.EmployerID);

            //return View(PatientsViewModel);



            if (id == null)
            {
                return HttpNotFound();
            }

            Patient patient = _context.Patients.Single(m => m.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Update(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Patient patient = _context.Patients.Single(m => m.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            return View(patient);
        }

        // GET: Medicine/5
        [ActionName("Medicine")]
        public List<Medicine> Medicine(int[] arrayOfValues)
        {
            //if (id == null)
            //{
            //    return HttpNotFound();
            //}

            //Medicine med = _context.Medicines.Single(m => m.ID == id);

            List<Medicine> med = _context.Medicines.Where(t => arrayOfValues.Contains(t.ID)).ToList();
            //if (med == null)
            //{
            //    return HttpNotFound();
            //}

            return med;
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Patient patient = _context.Patients.Single(m => m.ID == id);
            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: Patients/LogIn
        [ActionName("LogIn")]
        public IActionResult LogIn()
        {
            return View();
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("LogIn")]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(int id, string password)
        {
            Patient patient = _context.Patients.Single(m => m.ID == id);
            if (!patient.Password.Equals(password))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                // ???????
                return View();
            }

            Services.SessionExtensions.SetObjectAsJson(HttpContext.Session, "patient", patient);

            return RedirectToAction("Index", "Home");
        }
        

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
