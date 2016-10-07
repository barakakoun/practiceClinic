using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

        public void MyMethod(Microsoft.AspNet.Http.HttpContext context)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host}";

            // Other code
        }

        // GET: BlogManage
        public IActionResult Index()
        {
            var posts = _context.Posts.Include(d => d.Comments);
            MyMethod(HttpContext);
            return View(posts.ToList());
        }

        public IActionResult IndexComments(int? id)
        {

            var comments =
                from it in _context.Comments
                where it.PostID.Equals(id)
                select it;

            return View(comments.ToList());
        }

        public IActionResult IndexManage()
        {
            var posts = _context.Posts.Include(d => d.Comments);
            return View(posts.ToList());
        }

        // GET: BlogManage/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Post post = _context.Posts.Include(d => d.Comments).Single(m => m.ID == id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // GET: BlogManage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogManage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: BlogManage/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Post post = _context.Posts.Single(m => m.ID == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: BlogManage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Update(post);
                _context.SaveChanges();
                return RedirectToAction("IndexManage");
            }
            return View(post);
        }

        // GET: BlogManage/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Post post = _context.Posts.Single(m => m.ID == id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // GET: BlogManage/DeleteComment/5
        [ActionName("DeleteComment")]
        public IActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Comment comment = _context.Comments.Single(m => m.ID == id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(comment);
        }

        [HttpPost, ActionName("DeleteComment")]
        public IActionResult DeleteCommentConfirmed(int? id)
        {
            Comment comment = _context.Comments.Single(m => m.ID == id);
            string currPostID = comment.PostID.ToString();
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("IndexManage");
        }

        // POST: BlogManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Post post = _context.Posts.Single(m => m.ID == id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("IndexManage");
        }
    }
}
