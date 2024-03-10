using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp.Services
{
    public class PreferencesSettingsService : ISettingsService
    {
        private readonly string ip;

        public PreferencesSettingsService(string defaultIp = "192.168.0.11") 
        {
            ip = defaultIp;
        }

        public string LoadIpAddress()
        {
            return Preferences.Get("IpAddress", ip);
        }

        public void SaveIpAddress(string ipAddress)
        {
            Preferences.Set("IpAddress", ipAddress);
        }
    }
}
