using MeteoApp.ViewModels;

namespace MeteoApp.Views;

public partial class WeatherPage : ContentPage
{
    public WeatherPage(WeatherPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}