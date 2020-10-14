using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using App1.Models;
using Xamarin.Forms;

namespace App1.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        
        private string ipAddress;
        private string unitName;
        private string relayOneName;
        private string relayTwoName;
        private string relayOneCommand;
        private string relayTwoCommand;

        public ItemDetailViewModel()
        {
            Title = "Modify";

            SaveCommand = new Command(OnModify, ValidateSave);
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(ipAddress)
                && !String.IsNullOrWhiteSpace(unitName);
        }

        public int Id { get; set; }       

        public string IPAddress
        {
            get => ipAddress;
            set => SetProperty(ref ipAddress, value);
        }

        public string UnitName
        {
            get => unitName;
            set => SetProperty(ref unitName, value);
        }

        public string RelayOneName
        {
            get => relayOneName;
            set => SetProperty(ref relayOneName, value);
        }

        public string RelayTwoName
        {
            get => relayTwoName;
            set => SetProperty(ref relayTwoName, value);
        }

        public string RelayOneCommand
        {
            get => relayOneCommand;
            set => SetProperty(ref relayOneCommand, value);
        }

        public string RelayTwoCommand
        {
            get => relayTwoCommand;
            set => SetProperty(ref relayTwoCommand, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                IPAddress = item.IPAddress;
                UnitName = item.UnitName;
                RelayOneName = item.RelayOneName;
                RelayTwoName = item.RelayTwoName;
                RelayOneCommand = item.RelayOneCommand;
                RelayTwoCommand = item.RelayTwoCommand;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public Command DeleteCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnModify()
        {
            ControllerInfoItem modifyItem = new ControllerInfoItem()
            {
                Id = Id,
                IPAddress = IPAddress,
                UnitName = UnitName,
                RelayOneName = RelayOneName,
                RelayTwoName = RelayTwoName,
                RelayOneCommand = RelayOneCommand,
                RelayTwoCommand = RelayTwoCommand
            };

            await DataStore.UpdateItemAsync(modifyItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async void OnDelete()
        {            
            await DataStore.DeleteItemAsync(Id);            

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
