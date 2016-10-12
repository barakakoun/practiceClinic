using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProcedureTypesController : Controller
    {
        private ApplicationDbContext _context;

        public ProcedureTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProcedureTypes
        public IActionResult Index()
        {
            return View(_context.ProcedureTypes.ToList());
        }

        // GET: ProcedureTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ProcedureType procedureType = _context.ProcedureTypes.Single(m => m.ID == id);
            if (procedureType == null)
            {
                return HttpNotFound();
            }

            return View(procedureType);
        }

        // GET: ProcedureTypes/Create
        public IActionResult Create()
        {
            Patient pLogged = Services.SessionExtensions.GetObjectFromJson<Patient>(HttpContext.Session, "patient");
            if ((pLogged == null) ||
                (pLogged.ID != 1))
            {
                return RedirectToAction("PermissionError", "Home");
            }

            return View();
        }

        // POST: ProcedureTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProcedureType procedureType)
        {
            if (ModelState.IsValid)
            {
                _context.ProcedureTypes.Add(procedureType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedureType);
        }

        // GET: ProcedureTypes/Edit/5
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

            ProcedureType procedureType = _context.ProcedureTypes.Single(m => m.ID == id);
            if (procedureType == null)
            {
                return HttpNotFound();
            }
            return View(procedureType);
        }

        // POST: ProcedureTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProcedureType procedureType)
        {
            if (ModelState.IsValid)
            {
                _context.Update(procedureType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procedureType);
        }

        // GET: ProcedureTypes/Delete/5
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

            ProcedureType procedureType = _context.ProcedureTypes.Single(m => m.ID == id);
            if (procedureType == null)
            {
                return HttpNotFound();
            }

            return View(procedureType);
        }

        // POST: ProcedureTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ProcedureType procedureType = _context.ProcedureTypes.Single(m => m.ID == id);
            _context.ProcedureTypes.Remove(procedureType);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ProcedureTypes/PriceByID/5
        [ActionName("PriceByID")]
        public int getPriceByID(int nTypeID)
        {
            return (_context.ProcedureTypes.Single(m => m.ID == nTypeID).Price);
        }
    }
}
