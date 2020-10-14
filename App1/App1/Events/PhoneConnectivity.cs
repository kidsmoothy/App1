using System;
using System.Linq;
using Xamarin.Essentials;

namespace App1.Events
{
    public class PhoneConnectivity
    {
        public bool CheckWifiConnectivity()
        {
            var connectivity = Connectivity.ConnectionProfiles;

            if (!connectivity.Contains(ConnectionProfile.WiFi))
                return false;

            return true;
        }
    }
}
