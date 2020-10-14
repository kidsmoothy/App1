using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace App1.Events
{
    public class ControllerResponse
    {
        public  string ParseResponse(HttpWebResponse response)
        {
            var data = string.Empty;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var stream = response.GetResponseStream();
                var encode = Encoding.GetEncoding("utf-8");
                var readStream = new StreamReader(stream, encode);
                var characters = 100;
                var read = new Char[characters];

                int count = readStream.Read(read, 0, characters);

                data = new string(read, 0, count);

                response.Close();
                stream.Close();
                readStream.Close();
            }
            else
            {
                data = "ERROR   ERROR";
                //ResponseLabel.TextColor = Color.Red;
                //ResponseLabel.FontSize = 20;
            }

            return data.Trim().Replace("\r", "").Replace("\n", "");
        }
    }
}
