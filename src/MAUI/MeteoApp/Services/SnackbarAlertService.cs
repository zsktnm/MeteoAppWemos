using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeteoApp.Services
{
    public class SnackbarAlertService : IAlertService
    {
        public async Task ShowErrorAsync(string message)
        {
            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Black,
                ActionButtonTextColor = Colors.Black,
                CornerRadius = new CornerRadius(10)
            };
            var snackbar = Snackbar.Make(message, visualOptions: snackbarOptions);

            await snackbar.Show();
        }

        public async Task ShowSuccessAsync(string message)
        {
            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.Black,
                ActionButtonTextColor = Colors.Black,
                CornerRadius = new CornerRadius(10)
            };
            var snackbar = Snackbar.Make(message, visualOptions: snackbarOptions);

            await snackbar.Show();
        }
    }
}
