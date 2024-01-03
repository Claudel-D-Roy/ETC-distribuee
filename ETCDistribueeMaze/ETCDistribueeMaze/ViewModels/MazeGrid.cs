using ETCDistribueeMaze.Models;

namespace ETCDistribueeMaze.ViewModels
{
    public class MazeGrid
    {
       public bool[,] grid { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Dictionary<string, Position> PlayerPositions { get; set; }
        public int doorx { get; set; }
        public int doory { get; set; }
        public int exitx { get; set; }
        public int exity { get; set; }
        public bool endgame { get; set; }

    }
}
