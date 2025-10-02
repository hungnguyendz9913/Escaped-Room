namespace EscapeRoom.Models
{
    public class Puzzle
    {
        public string Question { get; set; }
        public string ImagePath { get; set; }
        public string[] Options { get; set; }
        public string CorrectIndex { get; set; }
    }
}
