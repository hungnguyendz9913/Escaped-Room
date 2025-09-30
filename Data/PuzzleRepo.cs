using System.Collections.Generic;

namespace EscapeRoom.Data
{
    public static class PuzzleRepo
    {
        public static List<Puzzle> Puzzles = new()
        {
            new Puzzle
            {
                Question = "Câu hỏi 1",
                ImagePath = "Assets/room1.jpg",
                Options = new [] { "Đáp án A", "Đáp án B: Chọn", "Đáp án C" },
                CorrectIndex = 1
            },
            new Puzzle
            {
                Question = "Câu hỏi 2",
                ImagePath = "Assets/room2.jpg",
                Options = new [] { "Đáp án A: Chọn", "Đáp án B", "Đáp án C" },
                CorrectIndex = 0
            },
            new Puzzle
            {
                Question = "Câu hỏi 3",
                ImagePath = "Assets/room3.jpg",
                Options = new [] { "Đáp án A", "Đáp án B", "Đáp án C: Chọn" },
                CorrectIndex = 2
            }
        };
    }
}
