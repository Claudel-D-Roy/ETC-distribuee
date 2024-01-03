using System.ComponentModel.DataAnnotations;

namespace ETCDistribueeMaze.ViewModels
{
    public class PostMessage
    {
        [Required, MinLength(1)]
        public string Text { get; set; }
    }
}
