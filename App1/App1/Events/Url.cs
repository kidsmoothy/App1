using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms.Xaml;

namespace App1.Events
{
    public class Url
    {
        public string CreateUrl(string ipAddress, string command)
        {
            return $"http://{ipAddress}/{command}";            
        }
    }
}
