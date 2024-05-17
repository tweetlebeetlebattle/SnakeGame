using SnakeA.Models.BedRock.Exceptions.cs;
using SnakeA.Models.BedRock.Interfaces;
using SnakeA.Models.GameEntities;
using SnakeA.Models.GameEntities.Interfaces;
using SnakeA.Models.InGameEntities.Interfaces;

namespace SnakeA.Models.InGameEntities
{
    public class Map : IMap
    {
        private int gameDimensionWidth;
        private int gameDimensionHeight;
        private List<IMapObject> gameMap;
        public Map(string size)
        {
            gameMap = new List<IMapObject>();

            GameMapInitializer(size);
            LoadMapWithObjects();
        }
        private void GameMapInitializer(string size)
        {
            switch (size)
            {
                case "small":
                    gameDimensionWidth = 50;
                    gameDimensionHeight = 15;
                    break;
                case "medium":
                    gameDimensionWidth = 75;
                    gameDimensionHeight = 22;
                    break;
                case "full":
                    gameDimensionWidth = 110;
                    gameDimensionHeight = 30;
                    break;
                default:
                    throw new GameInitializingException("Ivalid map dimension~");
            }
        }
        public int MapDimensionWidth { get { return gameDimensionWidth; } }
        public int MapDimensionHeight { get { return gameDimensionHeight; } }

        public List<IMapObject> GameMap
        {
            get
            {
                return gameMap;
            }
            set
            {
                gameMap = value;
            }
        }


        private void LoadMapWithObjects()  // could be abstract if more than one map exists
        {
            for (int i = 0; i < MapDimensionHeight; i++)
            {
                for (int t = 0; t < MapDimensionWidth; t++)
                {
                    IMapObject obj = null;
                    (int, int) objCoords = (t, i);
                    if (i == 0 || i == MapDimensionHeight - 1
                               ||
                        t == 0 || t == MapDimensionWidth - 1)
                    {
                        obj = new WallObject(objCoords);
                    }
                    else
                    {
                        obj = new EmptyObject(objCoords);
                    }
                    gameMap.Add(obj);
                }
            }
        }
    }
}
