using CommunityToolkit.Maui;
using MeteoApp.Services;
using MeteoApp.ViewModels;
using MeteoApp.Views;
using Microsoft.Extensions.Logging;

namespace MeteoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // SERVICES
            builder.Services.AddSingleton<ISettingsService, PreferencesSettingsService>();
            builder.Services.AddSingleton<IWeatherGetter, WeatherGetter>();
            HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
            builder.Services.AddSingleton(client);
            builder.Services.AddSingleton<IAlertService, SnackbarAlertService>();
            // PAGES
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<WeatherPage>();
            // VIEW MODELS
            builder.Services.AddSingleton<SettingsPageViewModel>();
            builder.Services.AddSingleton<WeatherPageViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
