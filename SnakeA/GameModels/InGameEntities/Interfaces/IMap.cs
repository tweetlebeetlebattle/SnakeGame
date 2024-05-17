namespace SnakeA.Models.InGameEntities.Interfaces
{
	public interface IMap
    {
        List<IMapObject> GameMap { get; }
        int MapDimensionWidth { get; }
        int MapDimensionHeight { get; }
    }
}
