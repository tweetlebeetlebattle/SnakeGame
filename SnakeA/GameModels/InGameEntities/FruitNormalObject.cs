
using SnakeA.Models.InGameEntities.Abstractions;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.GameEntities
{
	public class FruitNormalObject : AFruit, IMapObject
	{
		private char displayChar;
		private (int, int) pointCoordinates;

		public FruitNormalObject((int, int) coords) 
		{
			pointCoordinates = coords;
			displayChar = '@';
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
			base.pointPrize = 1;
		}
	}
}
