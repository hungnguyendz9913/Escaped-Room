namespace EscapeRoom.Models
{
    public class Puzzle
    {
        public required string Question { get; set; }
        public required string ImagePath { get; set; }
        public required string[] Options { get; set; }
        public required string CorrectIndex { get; set; }
    }
}
