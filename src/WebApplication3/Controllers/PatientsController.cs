using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;
using WebApplication3.Services;
using Microsoft.AspNet.Http;

namespace WebApplication3.Controllers
{
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
            var query = _context.Patients.GroupBy(p => p.Birthday);
            return View(_context.Patients.ToList());
        }

        // GET: Patients/Details/5
        public IActionResult Details(int? id)
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

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
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
                _context.Patients.Add(patient);
                _context.SaveChanges();
                Services.SessionExtensions.SetObjectAsJson(HttpContext.Session, "patient", patient);
                return RedirectToAction("Index", "Home");
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
