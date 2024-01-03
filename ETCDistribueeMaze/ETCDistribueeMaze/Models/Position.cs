namespace ETCDistribueeMaze.Models
{
    public class Position
    {
        public Position(string playerId, int posX, int posY)
        {
            PlayerID = playerId;
            PosX = posX;
            PosY = posY;
        }

        public string PlayerID { get; }
        public int PosX { get; }
        public int PosY { get; }
    }
}