using Confluent.Kafka;
using ETCDistribueeMaze.Models;
using ETCDistribueeMaze.Providers;
using ETCDistribueeMaze.ViewModels;
using System.Runtime.CompilerServices;

namespace ETCDistribueeMaze.Services
{
    public class MazeService : IMazeService
    {
        private readonly IUserStateProvider _usersProvider;
       
        private readonly IPositionPublisher _publisherpos;
        private readonly IPositionConsumer _consumerpos;

        
        private readonly IMessagesConsumer _messagesConsumer;
        private readonly IMessagesPublisher _messagesPublisher;

        private readonly IMazeConsumer _mazeConsumer;
        private readonly IMazePublisher _mazePublisher;
        

        public MazeService(IUserStateProvider usersProvider, IPositionPublisher publisher, IPositionConsumer consumer, IMessagesConsumer messagesConsumer, IMessagesPublisher messagesPublisher, IMazeConsumer mazeConsumer, IMazePublisher mazePublisher)
        {
            _usersProvider = usersProvider;

            _consumerpos = consumer;
            _publisherpos = publisher;
            _consumerpos.PositionReceived += OnPosition;

            _messagesConsumer = messagesConsumer;
            _messagesPublisher = messagesPublisher;
            _messagesConsumer.MessageReceived += OnMessage;

            _mazeConsumer = mazeConsumer;
            _mazePublisher = mazePublisher;
            _mazeConsumer.MazeReceived += OnMaze;

        }

        private void OnPosition(object sender, Position position)
        {
            this.PositionReceived?.Invoke(this, position);
        }

        private void OnMessage(object sender, Message message)
        {
            this.MessageReceived?.Invoke(this, message);
        }

        private void OnMaze(object sender, MazeGrid maze)
        {
            this.MazeReceived?.Invoke(this, maze);
        }

        public event EventHandler<Position> PositionReceived;
        public event EventHandler<Message> MessageReceived;
        public event EventHandler<MazeGrid> MazeReceived;

        public User Login(string username, ConnectedClient client)
        {
            var user = new User(username, false, 0);
            user.Connect(client);
            _usersProvider.AddOrUpdate(user);
            this.UserLoggedIn?.Invoke(this, new UserLoginEventArgs(user));
            return user;
        }

        public IEnumerable<User> GetAllUsers() => _usersProvider.GetAll();
        public event EventHandler<UserLoginEventArgs> UserLoggedIn;

        public void Logout(string username)
        {
            var user = _usersProvider.GetByUsername(username);
            if (null != user)
            {
                user.Disconnect();
                _usersProvider.AddOrUpdate(user);
            }

            this.UserLoggedOut?.Invoke(this, new UserLogoutEventArgs(username));
        }
        public event EventHandler<UserLogoutEventArgs> UserLoggedOut;
        public async Task PostPositionAsync(User user, int posX, int posY)
        {
            await _publisherpos.PublishAsync(new Position(user.Client.Id, posX, posY));
        }
        public async Task PostMessageAsync(User user, string message)
        {
            await _messagesPublisher.PublishAsync(new Message(user.Username, message, DateTime.UtcNow));
        }
        public async Task PostMazeAsync(Maze maze)
        {
            await _mazePublisher.PublishAsync(new MazeGrid
            {
                grid = maze.GetMaze(),
                width = maze.widthMaze,
                height = maze.heightMaze,
                PlayerPositions = maze.PlayerPositions,
                doorx = maze.doorx,
                doory = maze.doory,
                exitx = maze.exitx,
                exity = maze.exity,
                endgame = maze.endgame

            });
        }
    }
}
