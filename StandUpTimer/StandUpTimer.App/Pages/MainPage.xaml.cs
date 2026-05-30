using StandUpTimer.App.Constants;
using StandUpTimer.App.Models;
using StandUpTimer.App.Pages;

namespace StandUpTimer.App.Pages;

public partial class MainPage : ContentPage
{
    private readonly List<Participant> _participants = new();
    private int _minutesPerPerson = AppConstants.Timer.DefaultMinutes;

    public MainPage()
    {
        InitializeComponent();
        UpdateMinutesLabel();
    }

    private void OnMinusTapped(object sender, TappedEventArgs e)
    {
        if (_minutesPerPerson <= AppConstants.Timer.MinMinutes) return;
        _minutesPerPerson--;
        UpdateMinutesLabel();
    }

    private void OnPlusTapped(object sender, TappedEventArgs e)
    {
        if (_minutesPerPerson >= AppConstants.Timer.MaxMinutes) return;
        _minutesPerPerson++;
        UpdateMinutesLabel();
    }

    private void UpdateMinutesLabel()
    {
        MinutesLabel.Text = string.Format(
            AppConstants.Strings.MinutesFormat, _minutesPerPerson);
    }

    private void OnAddParticipantTapped(object sender, TappedEventArgs e)
    {
        var name = NameEntry.Text?.Trim();

        if (string.IsNullOrEmpty(name)) return;

        var participant = new Participant { Name = name };
        _participants.Add(participant);

        AddParticipantRow(participant);

        NameEntry.Text = string.Empty;

        UpdateStartButton();
    }

    private void AddParticipantRow(Participant participant)
    {
        var row = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = new GridLength(36) }
            },
            ColumnSpacing = 8,
        };

        row.Add(new Label
        {
            Text = $"👤  {participant.Name}",
            FontSize = 14,
            TextColor = AppConstants.Colors.TextPrimary,
            VerticalOptions = LayoutOptions.Center,
        });

        var deleteLabel = new Label
        {
            Text = "✕",
            FontSize = 16,
            TextColor = AppConstants.Colors.AccentRed,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };

        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) => RemoveParticipant(participant, row);
        deleteLabel.GestureRecognizers.Add(tap);

        Grid.SetColumn(deleteLabel, 1);
        row.Add(deleteLabel);

        ParticipantsList.Add(row);
    }

    private void RemoveParticipant(Participant participant, Grid row)
    {
        _participants.Remove(participant);
        ParticipantsList.Remove(row);
        UpdateStartButton();
    }

    private void UpdateStartButton()
    {
        bool hasParticipants = _participants.Count > 0;
        StartButton.IsEnabled = hasParticipants;
        StartButton.Opacity = hasParticipants ? 1.0 : 0.4;
    }

    private async void OnStartTapped(object sender, TappedEventArgs e)
    {
        if (_participants.Count == 0) return;

        await Shell.Current.GoToAsync(nameof(TimerPage),
            new Dictionary<string, object>
            {
                ["Participants"] = _participants,
                ["MinutesPerPerson"] = _minutesPerPerson,
            });
    }

    private void OnNameEntryCompleted(object sender, EventArgs e)
    {
        OnAddParticipantTapped(sender, new TappedEventArgs(null));
    }
}