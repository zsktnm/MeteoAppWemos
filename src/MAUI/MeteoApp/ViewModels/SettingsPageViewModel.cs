using MeteoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MeteoApp.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public string IpAddress { get; set; }   
        public Command UpdateIpAddressCommand { get; set; }
        public Command InputIpAddressCommand { get; set; }


        private string error = string.Empty;

        public string Error
        {
            get => error; 
            set => SetProperty(ref error, value);
        }

        private static bool IsValidIp(string ipAddress) => 
            IPAddress.TryParse(ipAddress, out IPAddress? _);

        public SettingsPageViewModel(ISettingsService settings, IAlertService alertService) 
        {
            IpAddress = settings.LoadIpAddress();

            UpdateIpAddressCommand = new Command(
                () => 
                {
                    settings.SaveIpAddress(IpAddress);
                    alertService.ShowSuccessAsync("Адрес успешно изменен");
                }, 
                () => string.IsNullOrEmpty(Error)
            );

            InputIpAddressCommand = new Command(() => {
                if (IsValidIp(IpAddress))
                {
                    Error = string.Empty;
                }
                else
                {
                    Error = "Укажите валидный Ip-адресс";
                }
                UpdateIpAddressCommand?.ChangeCanExecute();
            });
        }
    }
}
