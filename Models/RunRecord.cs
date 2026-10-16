using System;

namespace MidtermAppDev.Models
{
    public class RunRecord
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public GameDifficulty Difficulty { get; set; }
        public LexiconMode Lexicon { get; set; }
        public int WaveReached { get; set; }
        public int FinalScore { get; set; }
        public double Accuracy { get; set; }
        public int Wpm { get; set; }
        public int WordsCompleted { get; set; }
    }
}
