using System;
using System.Collections.Generic;
using System.Linq;
using MidtermAppDev.Models;
using MidtermAppDev.Services;

namespace MidtermAppDev.Data
{
    public static class UpgradeCatalog
    {
        public static List<UpgradeOption> GetUpgradePool()
        {
            return new List<UpgradeOption>
            {
                new UpgradeOption
                {
                    Title = "Overclock Dampeners",
                    Description = "Calibrates targeting thrusters. Reduces incoming enemy speed by 15%.",
                    Icon = "❄️",
                    Rarity = "Rare",
                    ApplyAction = s => s.EnemySpeedModifier = Math.Max(0.4, s.EnemySpeedModifier * 0.85)
                },
                new UpgradeOption
                {
                    Title = "Kinetic Typo Matrix",
                    Description = "+1 Permanent Typo Forgiveness shield replenishing at the start of every wave.",
                    Icon = "🛡️",
                    Rarity = "Epic",
                    ApplyAction = s => s.TypoForgiveness += 1
                },
                new UpgradeOption
                {
                    Title = "Bounty Protocol",
                    Description = "Optimizes score algorithms. Increases overall score multiplier by +25%.",
                    Icon = "💎",
                    Rarity = "Rare",
                    ApplyAction = s => s.ScoreMultiplier += 0.25
                },
                new UpgradeOption
                {
                    Title = "Nanite Hull Siphon",
                    Description = "Restores 35 Base Health and expands max hull capacity by +20.",
                    Icon = "🔧",
                    Rarity = "Common",
                    ApplyAction = s => {
                        s.MaxHealth += 20;
                        s.RepairHealth(35);
                    }
                },
                new UpgradeOption
                {
                    Title = "Frequency Jammer",
                    Description = "Increases delay between enemy spawns by 15%, giving more breathing room.",
                    Icon = "📡",
                    Rarity = "Common",
                    ApplyAction = s => s.SpawnIntervalMs = (int)(s.SpawnIntervalMs * 1.15)
                },
                new UpgradeOption
                {
                    Title = "Syntax Overdrive",
                    Description = "Awards 500 instant bonus points and increases score multiplier by +15%.",
                    Icon = "⚡",
                    Rarity = "Legendary",
                    ApplyAction = s => {
                        s.Score += 500;
                        s.ScoreMultiplier += 0.15;
                    }
                }
            };
        }

        public static List<UpgradeOption> GenerateChoices(int count = 3)
        {
            var rng = new Random();
            return GetUpgradePool().OrderBy(_ => rng.Next()).Take(count).ToList();
        }
    }
}
