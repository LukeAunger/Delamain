using delamain_maui.Views;

namespace delamain_maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        //Registering the routes of the page in appshell to share across the application
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(RequestEntryPage), typeof(RequestEntryPage));
        Routing.RegisterRoute(nameof(StatusPage), typeof(StatusPage));
        Routing.RegisterRoute(nameof(UserPreferences), typeof(UserPreferences));
    }
}

