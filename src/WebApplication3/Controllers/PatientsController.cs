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
            var patients = _context.Patients.Include(p => p.MedicineAllergies);
            //var patients = _context.Patients;
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
            Patient patient = _context.Patients.Include(d => d.MedicineAllergies).Include(d => d.Prucedures).Single(m => m.ID == id);

            //List<Medicine_Patient> mpNew = _context.Medicine_Patients.Where(d => patient.MedicineAllergies.Contains(d.ID));

            List<int> mpNew = patient.MedicineAllergies.Select(d => d.MedicineID).ToList();

            ViewBag.Meds = _context.Medicines.Include(d => d.PatientAllergic).Where(m => mpNew.Contains(m.ID)).ToList();

            //var medList = mpNew.Select()
            //foreach (Medicine_Patient curr in patient.MedicineAllergies)
            //{
            //    lstNew.Add(_context.Medicines.Include(d => d.PatientAllergic))
            //}

            //List<Medicine> lstNew = _context.Medicines.Include(d => d.PatientAllergic).joi

            if (patient == null)
            {
                return HttpNotFound();
            }

            return View(patient);
        }

        // GET: Patients/ByID/5
        //public List<Patient> ByID(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }


        //    //List<Medicine> med = _context.Medicines.Where(t => arrayOfValues.Contains(t.ID)).ToList();
        //    //Patient patient = _context.Patients.Include(d => d.PatientDrugs).Include(d => d.Prucedures).Single(m => m.ID == id);
        //    List<Patient> patient = _context.Patients.Include(d => d.MedicineAllergies).Include(d => d.Prucedures).Where(m => m.ID == id).ToList();
            
        //    return patient;
        //}

        // GET: Patients/Create
        public IActionResult Create()
        {
            //ViewBag.ProcedureTypeID = new SelectList(_context.ProcedureTypes, "ID", "Name", ProcedureTypeID);
            //var list = _context.Drugs.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            //ViewBag.Roles = list;

            //var categories = _context.Drugs.Select(c => new {
            //    CategoryID = c.ID,
            //    CategoryName = c.Name
            //}).ToList();
            //ViewBag.Categories = new MultiSelectList(categories, "CategoryID", "CategoryName");

            var allergies = _context.Medicines.Select(c => new {
                ID = c.ID,
                Name = c.Name
            }).ToList();

            ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name");

            //Patient tryhi = new Patient();

            //List<spGetTagsOfDocument_Result> tags = db.spGetTagsOfDocument(documentId).ToList();

            //foreach (spGetTagsOfDocument_Result tag in tags)
            //{
            //    model.selectedTags.Add(tag.TagName);
            //}



            return View(new Patient());
            //return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient, string[] Allergies)
        {
            if (_context.Patients.Any())
            {
                bool isExist;

                //isExist = _context.Patients.Any(m => m.Email == patient.Email);
                //if (isExist)
                //{
                //    ModelState.AddModelError(string.Empty, "User with the same email is already exist");
                //    return View(patient);
                //}

                //isExist = _context.Patients.Any(m => m.Phone == patient.Phone);
                //if (isExist)
                //{
                //    ModelState.AddModelError(string.Empty, "User with the same phone is already exist");
                //    return View(patient);
                //}

                isExist = _context.Patients.Any(m => m.Identifier.Equals(patient.Identifier));
                if (isExist)
                {
                    ModelState.AddModelError(string.Empty, "User with the same Identifier is already exist");
                    return View(patient);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                if (Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient") == null)
                {
                    Services.SessionExtensions.SetObjectAsJson(HttpContext.Session, "patient", patient);
                }

                if (Allergies != null)
                {
                    int nCurrPatientID = patient.ID;
                    IEnumerable<int> lstMedsIds = Allergies.Select(curr => Int32.Parse(curr));
                    foreach (int nCurr in lstMedsIds)
                    {
                        Medicine_Patient mpNew = new Medicine_Patient();
                        mpNew.MedicineID = nCurr;
                        mpNew.PatientID = nCurrPatientID;

                        _context.Medicine_Patients.Add(mpNew);

                        // TODO : האם אפשר להוציא מהלולאה את השורה הזאת? 
                    }
                    _context.SaveChanges();

                    // צריך לעדכן ידנית את הרשימה עבור המשתמש הספציפי?

                    //List<Medicine> med = _context.Medicines.Where(t => lstMedsIds.Contains(t.ID)).ToList();
                    //Medicine_Patient
                    //patient.MedicineAllergies = med;
                }
                return RedirectToAction("Details", new { id = patient.ID });
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

            Patient patient = _context.Patients.Include(p => p.MedicineAllergies).Include(p => p.Prucedures).Single(m => m.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            var allergies = _context.Medicines.Select(c => new {
                ID = c.ID,
                Name = c.Name
            }).ToList();

            var selected = allergies.Where(c => (patient.MedicineAllergies.Select(d => d.MedicineID)).Contains(c.ID)).ToList();

            ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name", selected);

            //string[] mpNew = patient.MedicineAllergies.Select(d => d.MedicineID.ToString()).ToArray<string>();
            //ViewBag.CurrAllergies = patient.MedicineAllergies.Select(d => d.MedicineID.ToString()).ToArray<string>();
            //allergies.Where(c => (patient.MedicineAllergies.Select(d => d.MedicineID)).Contains(c.ID))
            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient, string[] Allergies)
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
        //[ActionName("Medicine")]
        //public List<Medicine> Medicine(int[] arrayOfValues)
        //{
        //    //if (id == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}

        //    //Medicine med = _context.Medicines.Single(m => m.ID == id);

        //    List<Medicine> med = _context.Medicines.Where(t => arrayOfValues.Contains(t.ID)).ToList();
        //    //if (med == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}

        //    return med;
        //}

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
        public IActionResult LogIn(string identifier, string password)
        {
            Patient patient = _context.Patients.Single(m => (m.Identifier).Equals(identifier));
            if (!patient.Password.Equals(password))
            {
                ModelState.AddModelError(string.Empty, "Invalid ID or password");
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
