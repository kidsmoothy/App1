using App1.Helpers;
using App1.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace App1.Events
{
    public class ControllerRequests
    {        
        public MessageModel SendRequestToController(string ipAddress, string command)
        {
            var message = new MessageModel();

            var connectivity = new PhoneConnectivity();
            if (connectivity.CheckWifiConnectivity())
            {
                var url = new Url();

                try
                {
                    WebRequest request = WebRequest.Create(url.CreateUrl(ipAddress, command));
                    var response = new ControllerResponse();
                                       
                    request.Timeout = WebConstants.RequestTimeOut;
                    message.Message = response.ParseResponse((HttpWebResponse)request.GetResponse());
                }
                catch (Exception e)
                {                    
                    message.Message = e.Message.ToString();
                    message.MessageTitle = WebConstants.MessageError;
                }
            }
            else
            {                
                message.Message = "Please Connect to WiFi :)";
                message.MessageTitle = WebConstants.MessageAlert;
            }

            return message;
        }
    }
}
