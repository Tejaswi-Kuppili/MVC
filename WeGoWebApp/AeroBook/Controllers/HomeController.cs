using AeroBook.Data;
using AeroBook.Models;
using AeroBook.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AeroBook.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AeroBookDbContext _context;
		
		public HomeController(AeroBookDbContext context)
		{
			_context = context;
		}

		public IActionResult Homepage()
		{
			return View();
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

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(User user)
		{
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            if (ModelState.IsValid)
			{
				if (user.Password != user.ConfirmPassword)
				{
					return View();
				}
				var userObj = new User()
				{
					Name = user.Name,
					Email = user.Email,
					Password = hashedPassword,
				};
				await _context.Users.AddAsync(userObj);
				await _context.SaveChangesAsync();
				return RedirectToAction("Login");
			}
			return View();
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(User user)
		{
			User userObj = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

			if (userObj != null)
			{
                bool isPasswordMatched = BCrypt.Net.BCrypt.Verify(user.Password, userObj.Password);
                if (isPasswordMatched)
                {
                    return RedirectToAction("SignUp");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Email or Password";
                    return View();
                }
            }
			ViewBag.ErrorMessage = "User not found";
			return View("Login","Home");
		}
	}
}