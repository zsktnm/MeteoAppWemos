using MeteoApp.ViewModels;

namespace MeteoApp.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsPageViewModel settingsViewModel)
	{
		InitializeComponent();
        BindingContext = settingsViewModel;
    }
}