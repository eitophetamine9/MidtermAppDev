# CyberType // Defense Grid

> A browser-based, run-based survival typing game built with Blazor WebAssembly (.NET 10), pure C# game loops, and Tailwind CSS v4 styling.

![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor&logoColor=white) ![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp&logoColor=white) ![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-v4.x-06B6D4?logo=tailwindcss&logoColor=white) ![Architecture](https://img.shields.io/badge/Architecture-Pure_C%23_State-4B5563) ![Assets](https://img.shields.io/badge/Assets-Kenney_CC0-22C55E)

---

## Refactored Architecture & Project Structure

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
    ├── images/sprites/           # Kenney CC0 2D space shooter sprites
    │   ├── player_ship.png       # Operator battlecruiser avatar
    │   ├── enemy_ship.png        # Hostile typing target sprite
    │   ├── laser_beam.png        # Attack visual projectile
    │   └── space_bg.png          # High-density starfield backdrop
    └── index.html                # CyberType WebAssembly host page with importmap & preload
```

---

## Game Modes & Features

1. **Vocabulary / Lexicon Modes**:
   - **Mixed Hybrid**: Blends programming syntax keywords and regular English words.
   - **Code Syntax**: Pure C# and programming tokens (`async`, `virtual`, `interface`, `middleware`, `reflection`, etc.).
   - **Regular Words**: Everyday English vocabulary (`star`, `galaxy`, `shadow`, `thunder`, `whisper`, `metamorphosis`, etc.).

2. **Difficulty Tiers**:
   - **Easy**: 3–5 letter words, 0.65x falling speed, starts with +1 Typo Shield.
   - **Medium**: 5–8 letter words, 1.0x balanced speed, 1.4x score multiplier.
   - **Hard**: 8–14 letter words, 1.4x aggressive speed, 2.0x score multiplier.

3. **Precision Mechanics**:
   - Real-time 60 FPS loop with strict laser target-locking.
   - 2D sprite rendering powered by Kenney CC0 Space Shooter assets.
   - Text selection and insertion carets disabled across non-input UI elements for a native arcade feel.
   - Dynamic glowing health bar, typo forgiveness shield buffer, and combo multiplier.

---

## Running Locally

Development server: **`http://localhost:5223`**

```bash
dotnet run --launch-profile http
```
