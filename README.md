# CyberType // Defense Grid - Survival Typing Game

A fast-paced, browser-based run-based survival typing game built with **Blazor WebAssembly (.NET 10)** and **Tailwind CSS**.

---

## 🏗️ Refactored Architecture & Project Structure

The project has been cleaned of unused template files and decoupled into modular domains:

```text
├── Models/
│   ├── GameEnums.cs              # GameDifficulty & LexiconMode enums
│   ├── EnemyWord.cs              # Enemy entity with coordinate & substring properties
│   ├── UpgradeOption.cs          # Upgrade definition model
│   ├── RunRecord.cs              # Run telemetry record model
│   └── FloatingCombatText.cs     # Real-time floating damage & score text model
├── Data/
│   ├── WordLexicon.cs            # Word pools (Syntax & English) and difficulty filters
│   └── UpgradeCatalog.cs         # Upgrade card definitions & randomizer
├── Services/
│   └── GameSessionState.cs       # Scoped session manager (vitals, combo, stats, timers)
├── Components/
│   ├── WaveCompleteModal.razor   # Sector secured victory modal with high-contrast actions
│   └── GameOverModal.razor       # Base destroyed defeat modal with retry/abort actions
├── Pages/
│   ├── Home.razor                # Briefing Room dashboard with Lexicon & Difficulty selectors
│   ├── DefenseGrid.razor         # 60 FPS typing combat engine & target-locking state machine
│   ├── UpgradeHub.razor          # Centered upgrade cards & run progression
│   ├── Profile.razor             # Operator Dossier (Accuracy, WPM, Wave records, run log)
│   └── NotFound.razor            # 404 fallback page
├── Styles/
│   └── app.css                   # Tailwind CSS v4 source with caret-blocker & cyber styling
└── wwwroot/
    ├── css/app.css               # Compiled & minified Tailwind CSS bundle
    └── index.html                # CyberType WebAssembly host page
```

---

## 🎮 Game Modes & Features

1. **Vocabulary / Lexicon Modes**:
   - ⚡ **Mixed Hybrid**: Blends programming syntax keywords and regular English words.
   - 💻 **Code Syntax**: Pure C# and programming tokens (`async`, `virtual`, `interface`, `middleware`, `reflection`, etc.).
   - 📖 **Regular Words**: Everyday English vocabulary (`star`, `galaxy`, `shadow`, `thunder`, `whisper`, `metamorphosis`, etc.).

2. **Difficulty Tiers**:
   - **Easy**: 3–5 letter words, 0.65x falling speed, starts with +1 Typo Shield.
   - **Medium**: 5–8 letter words, 1.0x balanced speed, 1.4x score multiplier.
   - **Hard**: 8–14 letter words, 1.4x aggressive speed, 2.0x score multiplier.

3. **Precision Mechanics**:
   - Real-time 60 FPS loop with strict laser target-locking.
   - Text selection and insertion carets disabled across non-input UI elements for a native arcade feel.
   - Dynamic glowing health bar, typo forgiveness shield buffer, and combo multiplier.

---

## 💻 Running Locally

Development server: **`http://localhost:5223`**

```bash
dotnet watch
```
