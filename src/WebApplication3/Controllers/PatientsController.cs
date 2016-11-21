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

            Patient patient = _context.Patients.Include(d => d.MedicineAllergies).Include(d => d.Prucedures).Single(m => m.ID == id);

            if (patient == null)
            {
                return HttpNotFound();
            }

            var allergies =
                from MP in patient.MedicineAllergies
                where MP.PatientID == patient.ID
                join M in _context.Medicines on MP.MedicineID equals M.ID
                select M.Name;
            ViewBag.Meds = allergies.ToList();

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            var allergies = _context.Medicines.Select(c => new {
                ID = c.ID,
                Name = c.Name
            }).ToList();

            ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name");
            
            return View(new Patient());
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient, string[] Allergies)
        {
            if (_context.Patients.Any())
            {
                bool isExist;


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
            if (id == null)
            {
                return HttpNotFound();
            }

            // Get the patient
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

            // selected in the end
            ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name", selected.Select(t => t.ID).ToArray());

            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient, string[] Allergies)
        {
            if (ModelState.IsValid)
            {
                // Find all the medicine that were unchosen and remove them
                _context.Medicine_Patients.Where(curr => curr.PatientID == patient.ID).
                    Where(curr => !Allergies.Contains(curr.MedicineID.ToString())).ToList().
                    ForEach(curr=> _context.Medicine_Patients.Remove(curr));

                _context.SaveChanges();

                // Add new allergies
                // Run over the user choose
                foreach (string currChose in Allergies)
                {
                    // If the current choose isnt exist yet
                    // then add it!
                    if (!_context.Medicine_Patients.Any(curr=>curr.MedicineID.ToString().Equals(currChose)&&(curr.PatientID == patient.ID)))
                    {
                        Medicine_Patient mpNew = new Medicine_Patient();
                        mpNew.MedicineID = Int32.Parse(currChose);
                        mpNew.PatientID = patient.ID;

                        _context.Medicine_Patients.Add(mpNew);
                    }
                }

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

        [ActionName("Search")]
        public IActionResult Search()
        {
            var patients = _context.Patients.Include(p => p.MedicineAllergies);

            ViewBag.firstName = "";
            ViewBag.lastName = "";
            //ViewBag.nProc = 0;

            var allergies = _context.Medicines.Select(c => new {
                ID = c.ID,
                Name = c.Name
            }).ToList();

            ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name");

            return View(patients.ToList());
        }

        [HttpPost, ActionName("Search")]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string strFirst, string strLast, int nProc, string[] Allergies)
        {
            if (strFirst == null)
            {
                strFirst = "";
            }
            if (strLast == null)
            {
                strLast = "";
            }

            ViewBag.firstName = strFirst;
            ViewBag.lastName = strLast;
            if (nProc != 0)
            {
                ViewBag.nProc = nProc;
            }
            var allergies = _context.Medicines.Select(c => new {
                ID = c.ID,
                Name = c.Name
            }).ToList();
            // Build int array from the input
            IEnumerable<int> lstMedsIds = Allergies.Select(curr => Int32.Parse(curr));

            var selected = allergies.Where(c => lstMedsIds.Contains(c.ID)).ToList();

            // selected in the end
            ViewBag.Allergies = new MultiSelectList(allergies, "ID", "Name", selected.Select(t => t.ID).ToArray());

            // Filter the patients by first and last name
            var patients = _context.Patients.Include(d => d.MedicineAllergies).Include(p => p.Prucedures)
                                .Where(m => ((m.FirstName).Contains(strFirst) && (m.LastName).Contains(strLast)));

            var lstAfterMinProcs = new List<Patient>();

            // Make sure to return only patients who has more procs than nProc
            // If the value is 0, there's no need to check
            if (nProc != 0)
            {
                foreach (Patient currPa in patients)
                {
                    if (currPa.Prucedures.Count >= nProc)
                    {
                        lstAfterMinProcs.Add(currPa);
                    }
                }
            }
            else
            {
                lstAfterMinProcs = patients.ToList();
            }

            var toReturn = new List<Patient>();

            // We filter only if there where allergies chosen
            if (Allergies.Count() > 0)
            {

                var meds = _context.Medicines.Include(d => d.PatientAllergic);

                // Twice join - with condition to get only relevent meds
                var filterMeds = lstAfterMinProcs
                    .Join(_context.Medicine_Patients.Where(mp => lstMedsIds.Contains(mp.MedicineID)), p => p.ID, pc => pc.PatientID, (p, pc) => new { p, pc })
                    .Join(meds, ppc => ppc.pc.MedicineID, c => c.ID, (ppc, c) => new { ppc, c })
                    .Select(m => new {
                        PatientID = m.ppc.p.ID
                    }).Distinct();

                var patientIds = new List<int>();

                // Build array of the filtered patients
                foreach (var curr in filterMeds)
                {
                    patientIds.Add(curr.PatientID);
                }

                // Now we ask for the patients who fulfill all the filters
                toReturn = patients.Where(p => patientIds.Contains(p.ID)).ToList();
            }
            else
            {
                toReturn = lstAfterMinProcs;
            }

            return View(toReturn);
        }


        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
