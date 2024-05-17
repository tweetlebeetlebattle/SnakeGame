using SnakeA.Models.Game.Exceptions.cs;
using SnakeA.Models.InGameEntities;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.Game.MapFactory
{
	public class MapFactory
	{
		public MapFactory() { }
		public IMap CreateMap()
		{
			IMap map = null;
			Console.WriteLine("Enter map size: small, medium, full: ");
			string inputSize = Console.ReadLine();
			bool runCheck = true;
			while (runCheck)
			{
				switch (inputSize)
				{
					case "small":
					case "medium":
					case "full":
						runCheck = false;
						break;
					default:
						Console.WriteLine($"Invalid input {inputSize}.");
						inputSize = Console.ReadLine();
						break;
				}
			}
			map = new Map(inputSize);
			return map;
		}
	}
}
