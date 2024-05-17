
namespace SnakeA.Models.InGameEntities.Abstractions
{
	public abstract class AFruit 
	{
		protected int pointPrize;
		protected abstract void generatePrizePoints();
		public int GetPointsPrize()
		{
			return this.pointPrize;
		}
	}
}
