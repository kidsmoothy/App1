using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace App1.Models
{
    [Table("ControllerInfoItem")]
    public class ControllerInfoItem
    {
        public ControllerInfoItem()
        {           
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string IPAddress { get; set; }

        public string UnitName { get; set; }

        public string RelayOneName { get; set; }

        public string RelayTwoName { get; set; }

        public string RelayOneCommand { get; set; }

        public string RelayTwoCommand { get; set; }
    }
}
