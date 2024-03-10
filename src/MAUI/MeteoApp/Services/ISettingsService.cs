using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp.Services
{
    public interface ISettingsService
    {
        string LoadIpAddress();
        void SaveIpAddress(string ipAddress);
    }
}
