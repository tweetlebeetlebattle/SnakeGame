using Microsoft.AspNetCore.Mvc;
using SnakeA.Models.BedRock;
using SnakeA.Models.InGameEntities;

namespace SnakeA.Controllers
{
    public class SnakeController : Controller
	{
		public IActionResult ControllerEnterPoint()
		{
			DoSomething();
			return View("~/Views/Home/Privacy.cshtml");
		}
		private void DoSomething()
		{
			
			Console.Clear();
			Console.Write("ASDASD");
			Map map = new Map("full");
			GameService game = new GameService();
			game.InitializeGame();
			game.RunGame();
		}
	}
}
