using StandUpTimer.App.Constants;
using StandUpTimer.App.Models;

namespace StandUpTimer.App.Pages;

[QueryProperty(nameof(Participants), "Participants")]
[QueryProperty(nameof(MinutesPerPerson), "MinutesPerPerson")]
public partial class TimerPage : ContentPage
{
    public List<Participant> Participants { get; set; } = new();
    public int MinutesPerPerson { get; set; } = AppConstants.Timer.DefaultMinutes;

    private IDispatcherTimer? _timer;
    private TimeSpan _timeLeft;
    private TimeSpan _totalTime;
    private int _currentIndex = 0;
    private double _containerWidth = 0;

    public TimerPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartCurrentPerson();
        BuildParticipantsStatus();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopTimer();
    }

    private void StartCurrentPerson()
    {
        if (_currentIndex >= Participants.Count) return;

        var current = Participants[_currentIndex];

        _totalTime = TimeSpan.FromMinutes(MinutesPerPerson);
        _timeLeft = _totalTime;

        CurrentPersonLabel.Text = current.Name;
        UpdateUpNext();
        UpdateTimerLabel();
        UpdateProgressBar();

        StartTimer();
    }

    private void StartTimer()
    {
        StopTimer();

        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(AppConstants.Timer.TickIntervalMs);
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    private void StopTimer()
    {
        if (_timer is null) return;
        _timer.Stop();
        _timer.Tick -= OnTimerTick;
        _timer = null;
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));

        UpdateTimerLabel();
        UpdateProgressBar();

        if (_timeLeft <= TimeSpan.Zero)
        {
            MoveToNext();
        }
    }

    private void UpdateTimerLabel()
    {
        TimerLabel.Text = _timeLeft.ToString(AppConstants.Strings.TimerFormat);

        TimerLabel.TextColor = _timeLeft.TotalSeconds <= 30
            ? AppConstants.Colors.AccentRed
            : AppConstants.Colors.TextPrimary;
    }

    private void UpdateProgressBar()
    {
        if (_containerWidth <= 0) return;

        var progress = _timeLeft.TotalSeconds / _totalTime.TotalSeconds;
        ProgressBar.WidthRequest = _containerWidth * progress;

        ProgressBar.Color = _timeLeft.TotalSeconds <= 30
            ? AppConstants.Colors.AccentRed
            : AppConstants.Colors.AccentGreen;
    }

    private void UpdateUpNext()
    {
        var nextIndex = _currentIndex + 1;

        UpNextLabel.Text = nextIndex < Participants.Count
            ? string.Format(AppConstants.Strings.UpNext, Participants[nextIndex].Name)
            : string.Empty;
    }

    private void BuildParticipantsStatus()
    {
        ParticipantsStatus.Clear();

        for (int i = 0; i < Participants.Count; i++)
        {
            var p = Participants[i];
            bool isCurrent = i == _currentIndex;
            bool isDone = i < _currentIndex;

            var label = new Label
            {
                Text = isDone ? $"✓  {p.Name}" :
                            isCurrent ? $"▶  {p.Name}" :
                                        $"○  {p.Name}",
                FontSize = 13,
                TextColor = isDone ? AppConstants.Colors.AccentGreen :
                            isCurrent ? AppConstants.Colors.TextPrimary :
                                        AppConstants.Colors.TextMuted,
                HorizontalOptions = LayoutOptions.Center,
            };

            ParticipantsStatus.Add(label);
        }
    }

    private void MoveToNext()
    {
        StopTimer();

        Participants[_currentIndex].IsDone = true;
        _currentIndex++;

        if (_currentIndex >= Participants.Count)
        {
            ShowAllDone();
            return;
        }

        BuildParticipantsStatus();
        StartCurrentPerson();
    }

    private void ShowAllDone()
    {
        CurrentPersonLabel.Text = AppConstants.Strings.AllDone;
        UpNextLabel.Text = string.Empty;
        TimerLabel.Text = "00:00";
        TimerLabel.TextColor = AppConstants.Colors.AccentGreen;
        ProgressBar.WidthRequest = 0;

        NextButtonLabel.Text = AppConstants.Strings.DoneButton;
        NextButton.BackgroundColor = AppConstants.Colors.AccentPurple;
        NextButton.Stroke = AppConstants.Colors.AccentPurple;

        BuildParticipantsStatus();
    }

    private void OnNextTapped(object sender, TappedEventArgs e)
    {
        if (_currentIndex >= Participants.Count)
        {
            OnBackTapped(sender, e);
            return;
        }

        MoveToNext();
    }

    private async void OnBackTapped(object sender, TappedEventArgs e)
    {
        StopTimer();
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        _containerWidth = width - 48;
    }
}