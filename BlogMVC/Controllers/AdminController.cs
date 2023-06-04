using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogMVC.Controllers
{
    public class AdminController : Controller
    {
        BlogDbContext db;
        public AdminController(BlogDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = db.Categories;
            var posts = await db.Posts.Include(c => c.Category).ToListAsync();
            return View(posts);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditCategory(int? id)
        {
            if(id != null) 
            {
                Category? category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category != null) return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            db.Categories.Update(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult CreatePost()
        {
            ViewBag.Categories = db.Categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id != null)
            {
                Post? post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (post != null)
                {
                    db.Posts.Remove(post);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> EditPost(int? id)
        {
            ViewBag.Categories = db.Categories;
            if (id != null)
            {
                Post? post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (post != null) return View(post);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(Post post)
        {
            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}