using System;

namespace MidtermAppDev.Models
{
    public class FloatingCombatText
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Text { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
        public string ColorClass { get; set; } = "text-cyan-400";
        public double Opacity { get; set; } = 1.0;
    }
}
