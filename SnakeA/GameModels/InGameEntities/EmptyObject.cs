using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.InGameEntities.Abstractions;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.GameEntities
{
    public class EmptyObject : IMapObject
	{
		private char displayChar;
		private (int, int) pointCoordinates;
		public EmptyObject((int, int) coords)
		{
			pointCoordinates = coords;
			displayChar = ' ';
		}

		public (int, int) PointCoordinates
		{
			get
			{
				return pointCoordinates;
			}
		}

		public char Display
		{
			get { return displayChar; }
		}
	}
}
