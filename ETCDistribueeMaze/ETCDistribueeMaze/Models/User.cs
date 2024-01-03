namespace ETCDistribueeMaze.Models
{
    public class User
    {
        public User(string username, bool isHost, int index)
        {
            Username = username;
            IsHost = isHost;
            Index = index;
        }

        public string Username { get; }
        public ConnectedClient Client { get; private set; }
        public bool IsHost { get;  set; }
        public int Index { get; set; }
        public void Connect(ConnectedClient client)
        {
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public void Disconnect()
        {
            this.Client = null;
        }

        public void SetHostStatus(bool isHost)
        {
            IsHost = isHost;
        }
    }
}

