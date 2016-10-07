using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class FanClubController : Controller
    {
        private ApplicationDbContext _context;

        public FanClubController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FanClub
        public IActionResult Index()
        {
            return View(_context.Fan.ToList());
        }

        // GET: FanClub/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fan fan = _context.Fan.Single(m => m.ID == id);
            if (fan == null)
            {
                return HttpNotFound();
            }

            return View(fan);
        }

        // GET: FanClub/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FanClub/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fan fan)
        {
            if (ModelState.IsValid)
            {
                _context.Fan.Add(fan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: FanClub/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fan fan = _context.Fan.Single(m => m.ID == id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: FanClub/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Fan fan)
        {
            if (ModelState.IsValid)
            {
                _context.Update(fan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: FanClub/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fan fan = _context.Fan.Single(m => m.ID == id);
            if (fan == null)
            {
                return HttpNotFound();
            }

            return View(fan);
        }

        // POST: FanClub/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Fan fan = _context.Fan.Single(m => m.ID == id);
            _context.Fan.Remove(fan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
