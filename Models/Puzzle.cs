namespace EscapeRoom.Data
{
    public class Puzzle
    {
        public string Question { get; set; }
        public string ImagePath { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }
    }
}
