namespace SnakeA.Models.GameEntities.Interfaces
{
	public interface ISnakePlayer
	{
		bool IsWinner { get; }
		Dictionary<char, string> KeyToMovementDict { get; }
		bool CollisionCheck();
		(int, int) HeadCoordinates { get; }
		List<(int,int)> BodyCoordinates { get; }

	}
}
