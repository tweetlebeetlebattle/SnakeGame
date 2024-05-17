using SnakeA.Models.InGameEntities.Abstractions;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.GameEntities
{
	public class FruitEpicObject : AFruit, IMapObject
	{
		private char displayChar;
		private (int, int) pointCoordinates;

		public FruitEpicObject((int, int) coords) 
		{
			pointCoordinates = coords;
			displayChar = '$';
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

		protected override void generatePrizePoints()
		{
			Random rn = new Random();
			base.pointPrize = rn.Next(1,3);
		}
	}
}
