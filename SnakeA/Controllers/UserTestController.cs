using Microsoft.AspNetCore.Mvc;
using SnakeA.UserModels.Users;

namespace SnakeA.Controllers
{
	public class UserTestController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult TestUsersTest()
		{
			Console.Clear();
			Console.WriteLine(4);
			return View("~/Views/Home/UsersTest.cshtml");
		}
		public IActionResult GetUserInfoToRegisterUser(string username, string password)
		{
			User user = new User(username, password);
			UserService userService = new UserService(user);
			userService.RegisterUserInDatabase();
            return View("~/Views/Home/UsersTest.cshtml");
        }
	}
}
