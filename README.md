# ⏱️ StandUp Timer

A .NET MAUI app for running daily standup meetings. Add your team members, set the time per person, and let the timer automatically move through each participant — with a progress bar, color warnings, and a satisfying "All done" screen at the end.

Built as a live coding demo for a .NET MAUI masterclass.

---

## Features

- Add and remove participants before the standup
- Configurable time per person (1–5 minutes)
- Large countdown timer with `MM:SS` format
- Progress bar that shrinks in real time
- Timer and progress bar turn red in the last 30 seconds
- Automatic progression to the next person when time runs out
- Manual NEXT button to skip ahead
- Participant status list — shows who spoke, who's up, who's waiting
- "All done" screen when everyone has finished
- Dark theme throughout

---

## Tech Stack

| | |
|---|---|
| Framework | .NET MAUI (.NET 10) |
| Language | C# 13 |
| UI | XAML + Code-Behind |
| Architecture | Code-Behind (no MVVM) |
| IDE | Visual Studio 2026 |

---

## Project Structure

```
StandUpTimer/
├── Constants/
│   └── AppConstants.cs        # Colors, timer config, animation durations, strings
├── Models/
│   └── Participant.cs         # Participant model — Name, IsDone
├── Pages/
│   ├── MainPage.xaml          # Setup screen — participants, minutes, START
│   ├── MainPage.xaml.cs       # Add/remove participants, navigate to TimerPage
│   ├── TimerPage.xaml         # Timer screen — countdown, progress bar, NEXT
│   └── TimerPage.xaml.cs      # IDispatcherTimer, TimeSpan, QueryProperty
├── AppShell.xaml              # Navigation — Routing.RegisterRoute for TimerPage
└── Resources/
    └── Styles/
        ├── Colors.xaml        # All color tokens
        └── Styles.xaml        # Global styles
```

---

## Prerequisites

- [Visual Studio 2026](https://visualstudio.microsoft.com/) with the **.NET Multi-platform App UI** workload
- .NET 10 SDK
- For Android: Android SDK API 35+ (via Tools → Android → Android SDK Manager)
- For iOS/macOS: a Mac with Xcode installed

---

## How to Run

### Windows (quickest)

1. Clone the repository
   ```bash
   git clone https://github.com/grace-kal/standup-timer.git
   ```
2. Open `StandUpTimer.sln` in Visual Studio 2026
3. Select **Windows Machine** from the target dropdown
4. Press **F5** or click the green arrow

### Android

1. Open Tools → Android → Android Device Manager
2. Create or start an emulator (API 35+)
3. Select the emulator from the target dropdown
4. Press **F5**

### iOS / macOS

Requires a paired Mac. Select the target device from the dropdown and press **F5**.

---

## How to Use

1. **Set the time** — tap `+` and `−` to set minutes per person (default: 2 min)
2. **Add participants** — type a name and tap **Add** (or press Enter)
3. **Remove participants** — tap the `✕` next to any name
4. **Start** — tap the green **START** button (only active when at least one participant is added)
5. **During the standup** — tap **NEXT →** to move to the next person early, or wait for the timer to run out
6. **Finish** — when everyone is done, tap **DONE ✓** to return to the setup screen

---

## Key Concepts Demonstrated

This project was built to demonstrate the following C# and .NET MAUI concepts:

| Concept | Where |
|---|---|
| `IDispatcherTimer` | `TimerPage.xaml.cs` — ticks on the UI thread |
| `TimeSpan` formatting | `UpdateTimerLabel()` — `mm\:ss` format string |
| `[QueryProperty]` | Passing data between pages via Shell navigation |
| `Shell.Current.GoToAsync` | Navigation with parameters dictionary |
| `Routing.RegisterRoute` | `AppShell.cs` — registering TimerPage route |
| `async` / `await` | Navigation and back button |
| `OnAppearing` / `OnDisappearing` | Start and stop the timer with page lifecycle |
| `OnSizeAllocated` | Getting container width for the progress bar |
| Dynamic UI generation | `BuildParticipantsStatus()` |
| Code-Behind vs MVVM | Entire app — intentionally no ViewModel |

---

## Navigation Flow

```
AppShell
  └── MainPage (setup)
        └── Shell.GoToAsync("TimerPage") with { Participants, MinutesPerPerson }
              └── TimerPage
                    └── Shell.GoToAsync("..") → back to MainPage
```

Data is passed between pages using Shell's `QueryProperty` attribute — no shared state, no static variables.

---

## Extending the App

- **Sound alert** — play a sound when the timer hits zero using `MediaElement` from CommunityToolkit.Maui
- **Shuffle participants** — add a shuffle button on the setup screen before starting
- **History** — save each standup session to SQLite with participant names and timestamps
- **Migrate to MVVM** — move the timer logic into a `StandupViewModel` with `ObservableProperty` for the countdown and progress
