using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.InGameEntities.Abstractions;

namespace SnakeA.Models.InGameEntities
{
	public class SnakePlayer : ISnakePlayer
	{
		private char snakeHead = 'O';
		private char snakeBody;
		private Dictionary<char, string> keyToMovement;
		private (int, int) headCoordinates;
		private List<(int, int)> wholeCoordinates;
		private int lastDirection;
		private bool isFrozen;
		private int frozenCounter;
		private bool isWinner;
		public SnakePlayer(Dictionary<char, string> keyToMovement, List<(int, int)> wholeCoordinates, int intLastDirection, char bodyChar)
		{
			isWinner = true;
			isFrozen = false;
			frozenCounter = 0;
			lastDirection = intLastDirection;
			this.keyToMovement = keyToMovement;
			this.wholeCoordinates = wholeCoordinates;
			this.headCoordinates = wholeCoordinates[0];
			snakeBody = bodyChar;
		}
		public void RenderSnake()
		{
			for(int i = 0; i < wholeCoordinates.Count; i++)
			{
				Console.SetCursorPosition(wholeCoordinates[i].Item1, wholeCoordinates[i].Item2);
				if(i == 0)
				{
					Console.Write(snakeHead);
				}
				else
				{
					Console.Write(snakeBody);
				}
			}
		}
		public void SnakeEat(AFruit fruit)
		{
			for(int i = 0; i < fruit.GetPointsPrize(); i++)
			{
				wholeCoordinates.Add(wholeCoordinates[wholeCoordinates.Count() - 1]); // grow the snake at the back
			}
		}
		public bool IsWinner
		{
			get
			{
				return isWinner;
			}
		}
		public bool IsFrozen
		{
			get
			{
				return isFrozen;
			}
			set
			{
				isFrozen = value;
			}
		}
		public Dictionary<char, string> KeyToMovementDict
		{
			get
			{
				return this.keyToMovement;
			}
			set
			{
				this.keyToMovement = value;
			}
		}

		public (int, int) HeadCoordinates
		{
			get
			{
				return wholeCoordinates[0];
			}
			set
			{
				this.headCoordinates = value;
			}
		}

		public List<(int, int)> BodyCoordinates
		{
			get
			{
				return wholeCoordinates;
			} 
			set
			{
				this.wholeCoordinates = value;
			}
		}

		public bool CollisionCheck()
		{
			throw new NotImplementedException();
		}
		public void UpdateCoordsControler(string direction)
		{

			if (isFrozen)   // freeze player 
			{
				if (frozenCounter == 4)
				{
					frozenCounter = 0;
					isFrozen = false;
					return;
				}
				frozenCounter++;
				return;
			}
			int directionCommanded = 0;
			switch (direction)
			{
				case "Up":
					directionCommanded = 8;
					break;
				case "Down":
					directionCommanded = 2;
					break;
				case "Left":
					directionCommanded = 4;
					break;
				case "Right":
					directionCommanded = 6;
					break;
				default:
					directionCommanded = 5;
					break;
			}
			MoveSnakeHead(directionCommanded);
		}
		private void MoveSnakeHead(int direction)
		{
			int directionModifier = 0;
			int newWidth = 0;
			int newHeight = 0;
			if (direction == 5)
			{
				directionModifier = lastDirection;
			}
			else
			{
				directionModifier = direction;
			}
			if ((direction == 4 && lastDirection == 6) ||
				(direction == 6 && lastDirection == 4) ||
				(direction == 2 && lastDirection == 8) ||
				(direction == 8 && lastDirection == 2))
			{
				directionModifier = lastDirection;
			}
			switch (directionModifier)
			{
				case 2:
					newHeight = wholeCoordinates.First().Item2 + 1;
					newWidth = wholeCoordinates.First().Item1;
					lastDirection = 2;
					break;
				case 4:
					newHeight = wholeCoordinates.First().Item2;
					newWidth = wholeCoordinates.First().Item1 - 1;
					lastDirection = 4;
					break;
				case 8:
					newHeight = wholeCoordinates.First().Item2 - 1;
					newWidth = wholeCoordinates.First().Item1;
					lastDirection = 8;
					break;
				case 6:
					newHeight = wholeCoordinates.First().Item2;
					newWidth = wholeCoordinates.First().Item1 + 1;
					lastDirection = 6;
					break;
			}
			MoveSnakeBody();
			wholeCoordinates[0] = (newWidth, newHeight);
		}
		private void MoveSnakeBody()
		{
			for (int i = wholeCoordinates.Count - 1; i >= 0; i--)
			{
				if (!(i == 0))
				{
					wholeCoordinates[i] = wholeCoordinates[i - 1];
				}
			}
		}
	}
}
