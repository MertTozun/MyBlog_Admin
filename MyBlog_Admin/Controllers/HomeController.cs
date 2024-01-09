using Microsoft.AspNetCore.Mvc;
using MyBlog_Admin.Context;
using MyBlog_Admin.Models;
using System.Diagnostics;

namespace MyBlog_Admin.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly BlogContext _context;

		public HomeController(ILogger<HomeController> logger, BlogContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Login(string Email, string Password)
		{
			var author = _context.Author.FirstOrDefault(w => w.Email == Email && w.Password == Password);

			if (author != null)
			{
				HttpContext.Session.SetInt32("id", author.Id);
				HttpContext.Session.SetString("name", author.Name);
				return RedirectToAction("Index", "Blog");
			}
				return RedirectToAction(nameof(Index));

		}

		public async Task<IActionResult> AddCategory(Category category)
		{
			if (category.Id == 0)
			{
				await _context.AddAsync(category);
			}
			else
			{
				_context.Update(category);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Category));
		}

		public async Task<IActionResult> AddAuthor(Author author)
		{
			if (author.Id == 0)
			{
				await _context.AddAsync(author);
			}
			else
			{
				_context.Update(author);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Author));
		}

		public async Task<IActionResult> CategoryDetails(int Id)
		{
			var category = await _context.Category.FindAsync(Id);
			return Json(category);
		}

		public async Task<IActionResult> AuthorDetails(int Id)
		{
			var author = await _context.Author.FindAsync(Id);
			return Json(author);
		}

		public async Task<IActionResult> DeleteCategory(int? Id)
		{
			var category = await _context.Category.FindAsync(Id);
			if (category != null)
			{
				_context.Remove(category);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Category));
		}

		public async Task<IActionResult> DeleteAuthor(int? Id)
		{
			var author = await _context.Author.FindAsync(Id);
			if (author != null)
			{
				_context.Remove(author);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Author));
		}

		public IActionResult LogOut()
		{
			HttpContext.Session.Clear();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Index()
		{
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
				return Redirect("/Home/Index");
            }
            return View();
		}

		public IActionResult SignUp()
		{
			if (HttpContext.Session.GetInt32("id").HasValue)
			{
				return Redirect("/Home/Index");
			}
			return View();
		}

		public async Task<IActionResult> Register(Author author)
		{
			await _context.AddAsync(author);
			await _context.SaveChangesAsync();

			return Redirect("Index");
		}
		public IActionResult Category()
		{
			List<Category> list = _context.Category.ToList();
			return View(list);
		}

		public IActionResult Author()
		{
			List<Author> list = _context.Author.ToList();
			return View(list);
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
