using MeteoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp.Services
{
    public interface IWeatherGetter
    {
        Task<Data?> GetDataAsync();
    }
}
