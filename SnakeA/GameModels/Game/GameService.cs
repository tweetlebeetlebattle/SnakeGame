using SnakeA.Models.Game.MapFactory;
using SnakeA.Models.GameEntities;
using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.BedRock
{
    public class GameService 
	{
		private MapService mapService;
		private SnakeService snakeService;
		public GameService() 
		{
			
		}
		public void InitializeGame()
		{
			MapFactory mapFactory = new MapFactory();
			IMap gameMap = mapFactory.CreateMap();
			mapService = new MapService(gameMap);
			Console.WriteLine("Enter Players number:");
			int numbOfPlayers = int.Parse(Console.ReadLine());	// needs constraints or doesnt since it is not exposed to users
			List<List<(int, int)>> playersStartCoords = mapService.CalculatePlayersStartingCoordinates(numbOfPlayers);
			snakeService = new SnakeService();
			snakeService.CreatePlayers(playersStartCoords);
		}
		public void RunGame()
		{

			while (true)
			{
				List<(int, int)> allSnakeBodies = snakeService.GetAllSnakesBodyCoords();
				mapService.GenerateFruit(allSnakeBodies);
				mapService.RenderMap();
				snakeService.RenderSnakes();
				if (CollisionCheck())
				{
					break;
				}
				snakeService.SnakesMove();
			}
		}

		private bool CollisionCheck()
		{
			List<List<(int, int)>> allSnakesBodies2D = snakeService.GetAllSnakesBodiesCoords2D();
			List<(int, int)> allSnakesHeads2DCoords = snakeService.GetAllSnakesHeads2DCoords();
			List<int> allSnakeHeads1DCoords = new List<int>();
			foreach ((int,int) pair in allSnakesHeads2DCoords)
			{
				allSnakeHeads1DCoords.Add(mapService.CoordsToMapIndex(pair));
			}

			if (SnakesToWallCollisionCheck(allSnakeHeads1DCoords) || SnakesToSnakesCollisionCheck(allSnakesBodies2D))
			{
				return true;
			}
			SnakesEat(allSnakeHeads1DCoords);
			return false;
		}
		private bool SnakesToWallCollisionCheck(List<int> allSnakesHeadsCoords)
		{
			foreach(int index in allSnakesHeadsCoords)
			{
				if (mapService.Map.GameMap[index] is WallObject)
				{
					return true;
				}
			}
			return false;
		}
		private bool SnakesToSnakesCollisionCheck(List<List<(int,int)>> allSnakesBodiesCoords) 
		{
			int similarCoordsCounter = 0;
			for(int i = 0; i < allSnakesBodiesCoords.Count; i++)
			{
				foreach((int,int) singleSnakeCoords in allSnakesBodiesCoords[i])
				{
					if (allSnakesBodiesCoords[i][0] == singleSnakeCoords)
					{
						similarCoordsCounter++;
					}
				}
			}
			if(similarCoordsCounter > 1)
			{
				return true;
			}
			return false;
		}
		private void SnakesEat(List<int> allSnakesHeadsCoord)
		{
			List<IMapObject> underSnakesHeads = new List<IMapObject>();
			foreach(int index in allSnakesHeadsCoord)
			{
				underSnakesHeads.Add(mapService.Map.GameMap[index]);
			}
			snakeService.SnakesEat(underSnakesHeads);
		}
	}
}
