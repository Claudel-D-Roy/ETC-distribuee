using System.ComponentModel.DataAnnotations;

namespace ETCDistribueeMaze.ViewModels
{
    public class Login
    {

        [Required, MinLength(1)]
        public string Username { get; set; }
    }
}
