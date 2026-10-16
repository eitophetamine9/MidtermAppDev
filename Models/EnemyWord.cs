using System;

namespace MidtermAppDev.Models
{
    public class EnemyWord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Word { get; set; } = string.Empty;
        public int TypedIndex { get; set; } = 0;
        public double X { get; set; } // Horizontal percentage (0 to 100%)
        public double Y { get; set; } // Vertical pixels from top
        public double Speed { get; set; } = 1.0; // Movement pixels per tick
        public bool IsLocked { get; set; } = false;
        public bool IsDestroyed { get; set; } = false;
        public string Category { get; set; } = "Keyword";
        public string SpriteUrl { get; set; } = "images/sprites/enemy_ship.png";

        public string TypedPart => Word.Substring(0, Math.Min(TypedIndex, Word.Length));
        public string NextChar => TypedIndex < Word.Length ? Word[TypedIndex].ToString() : string.Empty;
        public string UntypedPart => TypedIndex < Word.Length ? Word.Substring(TypedIndex) : string.Empty;
        public bool IsCompleted => TypedIndex >= Word.Length;
    }
}
