using System;
using System.Collections.Generic;
using MidtermAppDev.Data;
using MidtermAppDev.Models;

namespace MidtermAppDev.Services
{
    public class GameSessionState
    {
        // Selected Configurations
        public GameDifficulty SelectedDifficulty { get; set; } = GameDifficulty.Medium;
        public LexiconMode SelectedLexiconMode { get; set; } = LexiconMode.Mixed;

        // Run Status
        public bool IsRunActive { get; set; } = false;
        public bool IsGameOver { get; set; } = false;
        public int WaveNumber { get; set; } = 1;
        public double RunActiveTimeSeconds { get; set; } = 0;

        // Player Vitals & Combat Stats
        public int MaxHealth { get; set; } = 100;
        public int CurrentHealth { get; set; } = 100;
        public int Score { get; set; } = 0;
        public int Currency { get; set; } = 0;
        public double ScoreMultiplier { get; set; } = 1.0;
        public int TypoForgiveness { get; set; } = 0;
        public int CurrentTypoShields { get; set; } = 0;
        public int ComboCount { get; set; } = 0;
        public int HighestCombo { get; set; } = 0;

        // Run Stats Tracking
        public int TotalKeystrokes { get; set; } = 0;
        public int CorrectKeystrokes { get; set; } = 0;
        public int TyposCount { get; set; } = 0;
        public int WordsCompletedCount { get; set; } = 0;

        public double AccuracyPercentage => TotalKeystrokes > 0 
            ? Math.Round((double)CorrectKeystrokes / TotalKeystrokes * 100.0, 1) 
            : 100.0;

        public int CurrentWpm
        {
            get
            {
                if (RunActiveTimeSeconds < 3 || CorrectKeystrokes == 0) return 0;
                double minutes = RunActiveTimeSeconds / 60.0;
                return (int)Math.Round((CorrectKeystrokes / 5.0) / minutes);
            }
        }

        // Lifetime Profile Records
        public double BestAccuracy { get; set; } = 0.0;
        public int BestWpm { get; set; } = 0;
        public int LongestWaveRun { get; set; } = 0;
        public int AllTimeHighScore { get; set; } = 0;
        public int TotalRunsAttempted { get; set; } = 0;
        public int TotalWordsDestroyedAllTime { get; set; } = 0;
        public List<RunRecord> PastRuns { get; } = new();

        // Difficulty Modifiers
        public double EnemySpeedModifier { get; set; } = 1.0;
        public int SpawnIntervalMs { get; set; } = 2200;
        public int WaveDurationSeconds { get; set; } = 30;
        public int EnemiesPerWaveQuota { get; set; } = 14;

        // Applied Upgrades History
        public List<UpgradeOption> AppliedUpgrades { get; } = new();

        public event Action? OnStateChanged;
        public void NotifyStateChanged() => OnStateChanged?.Invoke();

        public List<string> GetActiveDictionary()
        {
            return WordLexicon.GetWords(SelectedLexiconMode, SelectedDifficulty);
        }

        public void SetDifficulty(GameDifficulty difficulty)
        {
            SelectedDifficulty = difficulty;
            NotifyStateChanged();
        }

        public void SetLexiconMode(LexiconMode mode)
        {
            SelectedLexiconMode = mode;
            NotifyStateChanged();
        }

        public void ResetRun()
        {
            IsRunActive = true;
            IsGameOver = false;
            WaveNumber = 1;
            MaxHealth = 100;
            CurrentHealth = 100;
            Score = 0;
            Currency = 0;
            TypoForgiveness = 0;
            CurrentTypoShields = 0;
            ComboCount = 0;
            HighestCombo = 0;
            TotalKeystrokes = 0;
            CorrectKeystrokes = 0;
            TyposCount = 0;
            WordsCompletedCount = 0;
            RunActiveTimeSeconds = 0;
            AppliedUpgrades.Clear();

            switch (SelectedDifficulty)
            {
                case GameDifficulty.Easy:
                    EnemySpeedModifier = 0.65;
                    SpawnIntervalMs = 2800;
                    WaveDurationSeconds = 25;
                    EnemiesPerWaveQuota = 10;
                    ScoreMultiplier = 1.0;
                    TypoForgiveness = 1;
                    break;
                case GameDifficulty.Hard:
                    EnemySpeedModifier = 1.40;
                    SpawnIntervalMs = 1500;
                    WaveDurationSeconds = 35;
                    EnemiesPerWaveQuota = 18;
                    ScoreMultiplier = 2.0;
                    TypoForgiveness = 0;
                    break;
                case GameDifficulty.Medium:
                default:
                    EnemySpeedModifier = 1.0;
                    SpawnIntervalMs = 2200;
                    WaveDurationSeconds = 30;
                    EnemiesPerWaveQuota = 14;
                    ScoreMultiplier = 1.4;
                    TypoForgiveness = 0;
                    break;
            }

            CurrentTypoShields = TypoForgiveness;
            TotalRunsAttempted++;
            NotifyStateChanged();
        }

        public void PrepareForWave()
        {
            CurrentTypoShields = TypoForgiveness;
            NotifyStateChanged();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Math.Max(0, CurrentHealth - damage);
            ComboCount = 0;
            if (CurrentHealth <= 0)
            {
                IsGameOver = true;
                IsRunActive = false;
                RecordRunCompletion();
            }
            NotifyStateChanged();
        }

        public void RepairHealth(int amount)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
            NotifyStateChanged();
        }

        public bool RegisterKeystroke(bool isCorrect)
        {
            TotalKeystrokes++;
            if (isCorrect)
            {
                CorrectKeystrokes++;
                ComboCount++;
                if (ComboCount > HighestCombo)
                {
                    HighestCombo = ComboCount;
                }
                NotifyStateChanged();
                return true;
            }
            else
            {
                TyposCount++;
                if (CurrentTypoShields > 0)
                {
                    CurrentTypoShields--;
                    NotifyStateChanged();
                    return false;
                }
                else
                {
                    ComboCount = 0;
                    NotifyStateChanged();
                    return false;
                }
            }
        }

        public int RegisterWordDestroyed(string word)
        {
            WordsCompletedCount++;
            TotalWordsDestroyedAllTime++;
            int basePoints = word.Length * 25;
            double comboBonus = 1.0 + (ComboCount * 0.05);
            int earnedScore = (int)(basePoints * ScoreMultiplier * comboBonus);
            
            Score += earnedScore;
            Currency += Math.Max(1, word.Length / 2);
            NotifyStateChanged();
            return earnedScore;
        }

        public void RecordRunCompletion()
        {
            int runWpm = CurrentWpm;
            double runAccuracy = AccuracyPercentage;

            if (runAccuracy > BestAccuracy && TotalKeystrokes >= 20)
            {
                BestAccuracy = runAccuracy;
            }
            if (runWpm > BestWpm)
            {
                BestWpm = runWpm;
            }
            if (WaveNumber > LongestWaveRun)
            {
                LongestWaveRun = WaveNumber;
            }
            if (Score > AllTimeHighScore)
            {
                AllTimeHighScore = Score;
            }

            PastRuns.Insert(0, new RunRecord
            {
                Difficulty = SelectedDifficulty,
                Lexicon = SelectedLexiconMode,
                WaveReached = WaveNumber,
                FinalScore = Score,
                Accuracy = runAccuracy,
                Wpm = runWpm,
                WordsCompleted = WordsCompletedCount
            });

            if (PastRuns.Count > 10)
            {
                PastRuns.RemoveAt(PastRuns.Count - 1);
            }

            NotifyStateChanged();
        }

        public void ApplyUpgrade(UpgradeOption upgrade)
        {
            AppliedUpgrades.Add(upgrade);
            upgrade.ApplyAction?.Invoke(this);

            WaveNumber++;
            EnemySpeedModifier *= 1.10;
            SpawnIntervalMs = Math.Max(800, (int)(SpawnIntervalMs * 0.92));
            WaveDurationSeconds = Math.Min(60, WaveDurationSeconds + 3);
            EnemiesPerWaveQuota = EnemiesPerWaveQuota + 3;

            NotifyStateChanged();
        }

        public List<UpgradeOption> GenerateUpgradeChoices(int count = 3)
        {
            return UpgradeCatalog.GenerateChoices(count);
        }
    }
}
