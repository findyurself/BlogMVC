using BlogMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogMVC.Controllers
{
    public class BlogController : Controller
    {
        BlogDbContext db;
        public BlogController(BlogDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ViewPosts(int category)
        {
            IQueryable<Post> posts = db.Posts.Include(p => p.Category).Where(d => !d.IsDraft);
            if (category != null && category != 0)
            {
                posts = posts.Where(p => p.CategoryId == category);
            }

            List<Category> categories = db.Categories.ToList();
      
            categories.Insert(0, new Category { Title = "Все", Id = 0 });

            PostsFiltrModel viewModel = new PostsFiltrModel
            {
                Posts = posts.ToList(),
                Categories = new SelectList(categories, "Id", "Title", category)
            };
            return View(viewModel);
        }

        public async Task<IActionResult> ViewPost(int id)
        {
            Post post;
            try
            {
                post = await db.Posts.Include(c => c.Category).SingleAsync(i => i.Id == id);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return View(post);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}