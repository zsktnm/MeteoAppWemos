using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp.Services
{
    public interface IAlertService
    {
        Task ShowSuccessAsync(string message);
        Task ShowErrorAsync(string message);
    }
}
