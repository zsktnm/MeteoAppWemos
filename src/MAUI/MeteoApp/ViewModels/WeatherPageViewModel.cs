using MeteoApp.Models;
using MeteoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp.ViewModels
{
    public class WeatherPageViewModel : BaseViewModel
    {
        private double temperature = 0;
        public double Temperature
        {
            get => temperature;
            set => SetProperty(ref temperature, value);
        }

        private double humidity = 0;
        public double Humidity
        {
            get => humidity;
            set => SetProperty(ref humidity, value);
        }

        private double pressure = 0;
        public double Pressure
        {
            get => pressure;
            set => SetProperty(ref pressure, value);
        }

        private double light = 0;
        public double Light
        {
            get => light;
            set => SetProperty(ref light, value);
        }

        private readonly IWeatherGetter weather;
        private readonly IAlertService alertService;

        private bool isLoading = false;

        private async Task LoadWeather()
        {
            isLoading = true;
            UpdateCommand.ChangeCanExecute();
            var data = await weather.GetDataAsync();
            isLoading = false;
            UpdateCommand.ChangeCanExecute();
            if (data is not null)
            {
                Temperature = data.Temperature;
                Humidity = data.Humidity;
                Pressure = data.PressureMercury;
                Light = data.Light;
                await alertService.ShowSuccessAsync("Данные о погоде обновлены");
            }
            else
            {
                await alertService.ShowErrorAsync("Не удалось загрузить данные. Проверьте исправность платы и настройки IP-адреса");
            }
        }

        public Command UpdateCommand { get ; set; }

        public WeatherPageViewModel(IWeatherGetter weather, IAlertService alertService)
        {
            this.weather = weather;
            this.alertService = alertService;
            UpdateCommand = new Command(async () => await LoadWeather(), () => !isLoading);
        }
    }
}
