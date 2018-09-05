
namespace MlNetCore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public Movie Movie { get; set; }

        public string Text { get; set; }
    }
}
