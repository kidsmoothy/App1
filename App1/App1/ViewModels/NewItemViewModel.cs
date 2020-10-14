using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Input;
using App1.Helpers;
using App1.Models;
using App1.Services;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string ipAddress;
        private string unitName;
        private string relayOneName;
        private string relayTwoName;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(ipAddress)
                && !String.IsNullOrWhiteSpace(unitName);
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            // Check to see how many records have new IPAddress.  If IPAddress is already listed once, this means that it is a 4 channel relay.
            var items = await DataStore.GetItemByIPAsync(IPAddress);

            if (items.Count == 2)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "You cannot use this IP Address more than twice", "OK");
                return;
            }

            ControllerInfoItem newItem = new ControllerInfoItem()
            {
                IPAddress = IPAddress,
                UnitName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(UnitName).Trim(),
                RelayOneName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(RelayOneName).Trim(),
                RelayTwoName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(RelayTwoName).Trim(),
                RelayOneCommand = items.Count == 1 ? WebConstants.RelayThreeOn : WebConstants.RelayOneOn,
                RelayTwoCommand = items.Count == 1 ? WebConstants.RelayFourOn : WebConstants.RelayTwoOn
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }       
    }
}
