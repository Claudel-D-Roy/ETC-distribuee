using Confluent.Kafka;
using ETCDistribueeMaze.Models;
using ETCDistribueeMaze.ViewModels;

namespace ETCDistribueeMaze.Services
{
    public interface IMazeService
    {

        event EventHandler<UserLoginEventArgs> UserLoggedIn;
        event EventHandler<UserLogoutEventArgs> UserLoggedOut;
        event EventHandler<Position> PositionReceived;
        event EventHandler<Message> MessageReceived;
        event EventHandler<MazeGrid> MazeReceived;

        User Login(string username, ConnectedClient client);
        IEnumerable<User> GetAllUsers();
        void Logout(string username);
        Task PostPositionAsync(User user, int x, int y);
        Task PostMessageAsync(User user, string message);
        Task PostMazeAsync(Maze maze);

    }
}
