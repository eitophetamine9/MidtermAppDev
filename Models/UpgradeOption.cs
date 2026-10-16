using System;
using MidtermAppDev.Services;

namespace MidtermAppDev.Models
{
    public class UpgradeOption
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = "⚡";
        public string Rarity { get; set; } = "Common"; // Common, Rare, Epic, Legendary
        public Action<GameSessionState>? ApplyAction { get; set; }
    }
}
