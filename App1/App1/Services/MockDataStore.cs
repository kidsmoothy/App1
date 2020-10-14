using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App1.Models;

namespace App1.Services
{
    public class MockDataStore //: IDataStore<ControllerInfoItem>
    {
        readonly List<ControllerInfoItem> items;

        public MockDataStore()
        {
            items = new List<ControllerInfoItem>()
            {
                new ControllerInfoItem { Id = 1, IPAddress = "192.168.1.17", UnitName="Pistols", RelayOneName = "OPEN", RelayTwoName = "CLOSE", RelayOneCommand = "r1on", RelayTwoCommand = "r2on" },
                new ControllerInfoItem { Id = 2, IPAddress = "192.167.1.17", UnitName="Big Guns", RelayOneName = "OPEN", RelayTwoName = "CLOSE", RelayOneCommand = "r1on", RelayTwoCommand = "r2on"},
                new ControllerInfoItem { Id = 3, IPAddress = "192.166.1.17", UnitName="Drawer", RelayOneName = "OPEN", RelayTwoName = "CLOSE", RelayOneCommand = "r1on", RelayTwoCommand = "r2on"},
                new ControllerInfoItem { Id = 4, IPAddress = "192.165.1.17", UnitName="Cabinet 1", RelayOneName = "UP", RelayTwoName = "DOWN",RelayOneCommand = "r1on", RelayTwoCommand = "r2on" },
                new ControllerInfoItem { Id = 5, IPAddress = "192.164.1.17", UnitName="Cabinet 2", RelayOneName = "OPEN", RelayTwoName = "SHUT",RelayOneCommand = "r1on", RelayTwoCommand = "r2on" },
                new ControllerInfoItem { Id = 6, IPAddress = "192.163.1.17", UnitName="Cabinet 3", RelayOneName = "UP", RelayTwoName = "DOWN",RelayOneCommand = "r1on", RelayTwoCommand = "r2on" }
            };
        }

        public async Task<bool> AddItemAsync(ControllerInfoItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ControllerInfoItem item)
        {
            var oldItem = items.Where((ControllerInfoItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((ControllerInfoItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ControllerInfoItem> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ControllerInfoItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<ControllerInfoItem> GetItemByUnitNameAsync(string unitName)
        {
            return await Task.FromResult(items.FirstOrDefault(u => u.UnitName == unitName));
        }
    }
}