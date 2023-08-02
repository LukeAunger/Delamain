global using delamain_maui.Models;
global using delamain_maui.Data;
global using delamain_maui.Views;
global using Maui.GoogleMaps.Hosting;
global using Maui.GoogleMaps;
global using SQLite;
global using Microsoft.AspNetCore.SignalR.Client;
global using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace delamain_maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
#if ANDROID
            .UseGoogleMaps()
#elif IOS
			.UseGoogleMaps("AIzaSyAGSBRWDtgi8TK93YWu2daoAxUhQv-6iag")
#endif
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                //font awesome fonts and after the comma the alias to reference them by
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FAS");
                fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FAR");
                fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "BAR");
            });

        //creating a singleton and transient for fetching details and ability to create enrty
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<RequestEntryPage>();
        builder.Services.AddTransient<StatusPage>();
        builder.Services.AddScoped<UserPreferences>();

        builder.Services.AddSingleton<detailsDatabase>();
        builder.Services.AddSingleton<ServerConnection>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

