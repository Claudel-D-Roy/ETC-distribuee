namespace ETCDistribueeMaze.Services
{
    public class UserLogoutEventArgs : EventArgs
    {
        public string Username { get; }

        public UserLogoutEventArgs(string username)
        {
            Username = username;
        }
    }

    public class PositionEventArgs : EventArgs
    {
        public string Username { get; }
        public int posX { get; }
        public int posY { get; }
    }
    public class MessageEventArgs : EventArgs
    {
        public string Username { get; }
        public string Message { get; }
    }

    public class MazeEventArgs : EventArgs
    {
      public bool EndGame { get; }
        public bool[,] gridmaze { get; }
    }
}
