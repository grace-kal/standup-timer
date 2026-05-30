using StandUpTimer.App.Pages;
using StandUpTimer.App.Pages;

namespace StandUpTimer.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(TimerPage), typeof(TimerPage));
    }
}