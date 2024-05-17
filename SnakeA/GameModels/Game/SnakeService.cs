using SnakeA.Models.GameEntities;
using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.InGameEntities;
using SnakeA.Models.InGameEntities.Abstractions;
using SnakeA.Models.InGameEntities.Interfaces;
using System.Text;

namespace SnakeA.Models.BedRock
{
	public class SnakeService
	{
		private List<SnakePlayer> snakePlayers;
        private int mSecsFrequiencyGameUpdate = 100;
        public SnakeService()
        {
            snakePlayers = new List<SnakePlayer>();
        }
        public void CreatePlayers( List<List<(int, int)>> publicSnakeCoords)
        {
			int setSnakePlayerLastDirection = 4;

			for (int i = 0; i < publicSnakeCoords.Count; i++)
            {
				SnakePlayer newPlayer = new SnakePlayer(PrivateKeyBindings(), publicSnakeCoords[i], setSnakePlayerLastDirection, char.Parse(i.ToString()));
                snakePlayers.Add(newPlayer);
                if(setSnakePlayerLastDirection == 6)
                {
                    setSnakePlayerLastDirection = 4;
                } else
                {
                    setSnakePlayerLastDirection = 6;
                }
            }
        }
		public List<(int,int)> GetAllSnakesHeads2DCoords()
		{
			List<(int, int)> snakesHeads = new List<(int, int)>();
			foreach(var snake in snakePlayers)
			{
				snakesHeads.Add(snake.HeadCoordinates);
			}
			return snakesHeads;
		}
		public List<List<(int,int)>> GetAllSnakesBodiesCoords2D()
		{
			List<List<(int, int)>> values = new List<List<(int, int)>>();
			foreach(var snake in snakePlayers)
			{
				List<(int, int)> coords = new List<(int, int)>();
				foreach(var pair in snake.BodyCoordinates)
				{
					coords.Add(pair);
				}
				values.Add(coords);
			}
			return values;
		}
        private Dictionary<char, string> PrivateKeyBindings()
        {
            Dictionary<char, string> dict = new Dictionary<char, string>();
            List<string> direction = new List<string>() { "Up", "Down", "Left", "Right" };
            foreach(string directionName in direction)
            {
				Console.Write($"{directionName}:");
				char keyBind = char.Parse(Console.ReadLine());
				dict.Add(keyBind, directionName);
            }
            return dict;
        }
        public List<(int,int)> GetAllSnakesBodyCoords()
        {
            List<(int, int)> allSnakesBodyCoords = new List<(int, int)>();
            foreach( var snake in snakePlayers)
            {
                foreach(var pair in snake.BodyCoordinates)
                {
                    allSnakesBodyCoords.Add(pair);
                }
            }
            return allSnakesBodyCoords;
        }
        public void SnakesMove()
        {
            string roundInput = ReadAllInput();
            UpdateSnakesCoords(roundInput);
        }
        private void UpdateSnakesCoords(string roundInput)
        {
			List<char> roundInputSeparateChars = roundInput.ToList();
			foreach (var snake in snakePlayers)
			{
				List<char> allCommandsRelevantToOneSnake = new List<char>();
				foreach (char command in roundInputSeparateChars)
				{
					if (snake.KeyToMovementDict.ContainsKey(command))
					{
						allCommandsRelevantToOneSnake.Add(command);
					}
				}
				string commandDirection = "";

				if (allCommandsRelevantToOneSnake.Count == 0)
				{
					commandDirection = "previous";
				}
				else
				{
					commandDirection = snake.KeyToMovementDict[allCommandsRelevantToOneSnake[0]];
				}
				snake.UpdateCoordsControler(commandDirection);
			}
		}
        private string ReadAllInput() // not sure how i will read input in browser environment??
        {
			StringBuilder userInputBuilder = new StringBuilder();

			// Set the cursor position so user input is not in the middle of the board
			Console.SetCursorPosition(111, 22);
			Console.Write("Input");
			Console.SetCursorPosition(111, 23);
			Console.Write(":");

			DateTime startTime = DateTime.Now;
			TimeSpan duration = TimeSpan.FromMilliseconds(mSecsFrequiencyGameUpdate);

			while ((DateTime.Now - startTime) < duration)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
					char keyChar = keyInfo.KeyChar;

					if (keyChar == '\r') // Enter key
					{
						break;
					}

					userInputBuilder.Append(keyChar);
				}
				Thread.Sleep(10);
			}

			string inputString = userInputBuilder.ToString();
			return inputString;
        }
		public void RenderSnakes()
		{
			foreach(var snake in snakePlayers)
			{
				snake.RenderSnake();
			}
		}
		public void SnakesEat(List<IMapObject> mapObjects)
		{
			for(int i = 0; i < mapObjects.Count; i++)
			{
				if (mapObjects[i] is FruitFreezeObject)
				{
					for (int k = 0; k < snakePlayers.Count; k++)
					{
						if (k == i)
						{
							continue;
						}
						snakePlayers[k].IsFrozen = true;
					}
				}
				if (mapObjects[i] is AFruit)
				{
					snakePlayers[i].SnakeEat((AFruit)mapObjects[i]);
				}
			}
		}
	}
}
