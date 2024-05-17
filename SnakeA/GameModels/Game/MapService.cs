using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.GameEntities;
using System.Diagnostics.Metrics;
using System.ComponentModel.DataAnnotations;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.BedRock
{
    public class MapService
	{
		private IMap map;
		private int numbersOfFruitOnBoardAtAnyTime;
		private List<int> listOfSnakeBodyPositions;
		public MapService(IMap map)
		{
			listOfSnakeBodyPositions = new List<int>();
			numbersOfFruitOnBoardAtAnyTime = 1; // can be controlled via DI for GameMode distinction
			this.map = map;
		}
		public IMap Map
		{
			get
			{
				return this.map;
			}
		}
		public int CoordsToMapIndex((int, int) coords)
		{
			return map.MapDimensionWidth * coords.Item2 + coords.Item1; // always sequence of insert is width, hight into coords
		}
		public (int,int) MapIndexToCoords(int index)
		{
			int width = index % map.MapDimensionWidth;
			int height = index / map.MapDimensionHeight;
			(int, int) coords = (width, height);

			return coords;
		}
		public List<int> ListOfSnakeBodyPositions 
		{
			get
			{
				return listOfSnakeBodyPositions;
			}
			set
			{
				listOfSnakeBodyPositions = value;
			}
		}
		public List<List<(int, int)>> CalculatePlayersStartingCoordinates(int numbOfPlayers)
		{
			List<List<(int, int)>> playersCoords = new List<List<(int, int)>>();
			for (int i = 0; i < numbOfPlayers; i++)   // calculate equally spaced starting positions for a maximum of 4 players using a runtime calculation
			{
				List<(int, int)> playerCoords = new List<(int, int)>();
				int playersCountOffset = numbOfPlayers == 1 ? 0 : 1;
				int widthOffset = (i % 2 == 0) ? 1 : 2;
				int heightOffset = i < 2 ? 1 : 2;
				int coordHeight = ((map.MapDimensionHeight / (2 + playersCountOffset)) * heightOffset);
				for (int t = 0; t < 4; t++)
				{
					int growthOffset = (widthOffset == 1) ? 1 : -1;
					int coordWidth = ((map.MapDimensionWidth / (2 + playersCountOffset)) * widthOffset) + (t * growthOffset);
					(int, int) coords = (coordWidth, coordHeight);
					playerCoords.Add(coords);
				}
				playersCoords.Add(playerCoords);
			}
			return playersCoords;
		}
		public void RenderMap()
		{
			Console.Clear();
			for (int i = 0; i < map.MapDimensionHeight; i++)
			{
				for (int t = 0; t < map.MapDimensionWidth; t++)
				{
					int indexInMap = CoordsToMapIndex((t, i));
					Console.Write(map.GameMap[indexInMap].Display);
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		public void GenerateFruit(List<(int,int)> allSnakesBodyCoords)
		{
			// check fruit count
			int fruitCounter = 0;
			int gameModePreSetFruitCount = numbersOfFruitOnBoardAtAnyTime; // can be set by IGameMode class
			foreach(var items in map.GameMap)
			{
				if(items is FruitNormalObject || items is FruitFreezeObject || items is FruitEpicObject)
				{
					fruitCounter++;
				}
			}
			if (fruitCounter == gameModePreSetFruitCount) 
			{
				return;
			}
			// generate new fruit
			// avoid player/wall coords for generation
			List<int> allSnakesBodyCoordsTranslated = new List<int>();
			foreach(var pair  in allSnakesBodyCoords)
			{
				allSnakesBodyCoordsTranslated.Add(CoordsToMapIndex(pair));
			}
			List<int> CoordsOfValidFruitSpawnObject = new List<int>();
			foreach(var item in map.GameMap)
			{
				int itemCoords1Dindex = CoordsToMapIndex(item.PointCoordinates);
				if (item is not WallObject || !(listOfSnakeBodyPositions.Contains(itemCoords1Dindex)) ) // filter walls and snake bodies, create the everyTurnCollectSNakeBodyCoords function to work with a relevant List in this if- check
				{
					if(!allSnakesBodyCoordsTranslated.Contains(itemCoords1Dindex))
					{
						CoordsOfValidFruitSpawnObject.Add(itemCoords1Dindex);
					}
				}
			}
			while (fruitCounter < gameModePreSetFruitCount)
			{
				Random rnd = new Random();
				int indexSpawnPoint = rnd.Next(0, CoordsOfValidFruitSpawnObject.Count - 1);
				int randomFruitTypePicker = rnd.Next(0, 6);
				if (randomFruitTypePicker == 0)
				{
					this.map.GameMap[indexSpawnPoint] = new FruitFreezeObject(MapIndexToCoords(indexSpawnPoint));
				}
				else if (randomFruitTypePicker == 1)
				{
					this.map.GameMap[indexSpawnPoint] = new FruitEpicObject(MapIndexToCoords(indexSpawnPoint));
				} else
				{
					this.map.GameMap[indexSpawnPoint] = new FruitNormalObject(MapIndexToCoords(indexSpawnPoint));
				}
				CoordsOfValidFruitSpawnObject.Remove(indexSpawnPoint);
				fruitCounter++;
			}
		}
	}
}
