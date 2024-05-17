using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.BedRock.Interfaces
{
    public interface IGameService
	{
		IMap GameMap { get; }
		List<ISnakePlayer> PlayersList { get; }

	}
}
