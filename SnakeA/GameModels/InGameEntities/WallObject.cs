using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.GameEntities
{
	public class WallObject : IMapObject
	{
		private char displayChar;
		private (int, int) pointCoordinates;

		public WallObject((int,int) coords) 
        {
			pointCoordinates = coords;
			displayChar = 'X';
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
