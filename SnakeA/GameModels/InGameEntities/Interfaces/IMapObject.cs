namespace SnakeA.Models.InGameEntities.Interfaces
{
	public interface IMapObject
	{
		public (int, int) PointCoordinates { get; }
		public char Display { get; }
	}
}
