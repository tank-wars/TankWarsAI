using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.GameDomain;
using GameClient.Foundation;
using GameClient.Network.Messages;
using GameClient.Network.Communicator;
using GameClient.Foundation;

namespace GameClient.AI
{
    /// <summary>
    /// A simple console routine to show examples of the A* implementation in use
    /// </summary>
    class AIDriver
    {
        private bool[,] map;
        private SearchParameters searchParameters;
        PathFinder pathFinder;
        List<Point> path;

        /// <summary>
        /// Outputs three examples of path finding to the Console.
        /// </summary>
        /// <remarks>The examples have copied from the unit tests!</remarks>
        /// 
        public AIDriver()
        {
            InitializeMap();
        }

        public void Run()
        {
            if (GameWorld.Instance.State == GameWorld.GameWorldState.Running)
            {
                Point startPoint = new Point(GameWorld.Instance.Players[GameWorld.Instance.MyPlayerNumber].Position.X, GameWorld.Instance.Players[GameWorld.Instance.MyPlayerNumber].Position.Y);
                Point endPoint = new Point(GameWorld.Instance.Players[1].Position.X, GameWorld.Instance.Players[1].Position.Y);
                setBarriers();
                setEndPoints(startPoint, endPoint);
                pathFinder = new PathFinder(searchParameters);
                path = pathFinder.FindPath();

                ClientMessage msg = new PlayerMovementMessage(Direction.West);
             //   Communicator.Instance.SendMessage(msg.GenerateStringMessage());

                ShowRoute("The algorithm should find a direct path without obstacles:", path);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays the map and path as a simple grid to the console
        /// </summary>
        /// <param name="title">A descriptive title</param>
        /// <param name="path">The points that comprise the path</param>
        private void ShowRoute(string title, IEnumerable<Point> path)
        {
            Console.WriteLine("{0}\r\n", title);
            for (int y = 0; y < this.map.GetLength(1); y++) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (this.searchParameters.StartLocation.Equals(new Point(x, y)))
                        // Show the start position
                        Console.Write('S');
                    else if (this.searchParameters.EndLocation.Equals(new Point(x, y)))
                        // Show the end position
                        Console.Write('F');
                    else if (this.map[x, y] == false)
                        // Show any barriers
                        Console.Write('░');
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                        // Show the path in between
                        Console.Write('*');
                    else
                        // Show nodes that aren't part of the path
                        Console.Write('·');
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Creates a clear map with a start and end point and sets up the search parameters
        /// </summary>
        private void InitializeMap()
        {
            this.map = new bool[10, 10];
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                    map[x, y] = true;

        }

        private void setEndPoints(Point start, Point end)
        {
            var startLocation = start;
            var endLocation = end;
            this.searchParameters = new SearchParameters(startLocation, endLocation, map);
        }

        private void setBarriers()
        {
            GameWorld gameWorld = GameWorld.Instance;
            MapDetails mapDetails = gameWorld.Map;
            Coordinate[] bricks = mapDetails.Brick;
            Coordinate[] stones = mapDetails.Stone;
            Coordinate[] water = mapDetails.Water;

            foreach(Coordinate coordinate in bricks)
            {
                map[coordinate.X, coordinate.Y] = false;
            }

            foreach (Coordinate coordinate in stones)
            {
                map[coordinate.X, coordinate.Y] = false;
            }

            foreach (Coordinate coordinate in water)
            {
                map[coordinate.X, coordinate.Y] = false;
            }

        }

        //public Direction decodeDirection(Point source, Point destination)
        //{
        //    if((destination.X - source.X < 0) && (destination.Y = source.Y > 0))
        //    {
        //        return Direction
        //    }
        //    return Direction.West;
        //}
    }
}
