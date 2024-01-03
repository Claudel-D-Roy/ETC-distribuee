using ETCDistribueeMaze.Models;

namespace ETCDistribueeMaze.Services
{
    public class UserLoginEventArgs : EventArgs
    {
        public User User { get; }

        public UserLoginEventArgs(User user)
        {
            User = user;
        }
    }
}
