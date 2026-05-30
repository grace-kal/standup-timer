namespace StandUpTimer.App.Constants;

public static class AppConstants
{
    public static class Colors
    {
        public static readonly Color Background = Color.FromArgb("#0F172A");
        public static readonly Color Surface = Color.FromArgb("#1A2F4A");
        public static readonly Color SurfaceLight = Color.FromArgb("#162640");
        public static readonly Color Border = Color.FromArgb("#243552");
        public static readonly Color TextPrimary = Color.FromArgb("#FFFFFF");
        public static readonly Color TextSecondary = Color.FromArgb("#A5C8E8");
        public static readonly Color TextMuted = Color.FromArgb("#7CA8C8");
        public static readonly Color AccentGreen = Color.FromArgb("#10B981");
        public static readonly Color AccentRed = Color.FromArgb("#EF4444");
        public static readonly Color AccentOrange = Color.FromArgb("#F59E0B");
        public static readonly Color AccentPurple = Color.FromArgb("#7C3AED");
    }

    public static class Timer
    {
        public const int DefaultMinutes = 2;
        public const int MinMinutes = 1;
        public const int MaxMinutes = 5;
        public const int TickIntervalMs = 1000;
    }

    public static class Animation
    {
        public const uint FadeOutDuration = 150;
        public const uint FadeInDuration = 250;
        public const double FadeOutValue = 0;
        public const double FadeInValue = 1;
    }

    public static class Strings
    {
        public const string AppTitle = "Standup Timer";
        public const string AddParticipant = "Add";
        public const string StartButton = "START";
        public const string NextButton = "NEXT  →";
        public const string DoneButton = "DONE ✓";
        public const string TimerFormat = @"mm\:ss";
        public const string PlaceholderName = "Enter name...";
        public const string MinutesFormat = "{0} min / person";
        public const string SpeakingNow = "Speaking now";
        public const string UpNext = "Up next: {0}";
        public const string AllDone = "🎉  All done!";
    }
}