using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using App1.Models;
using App1.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using App1.Services;
using App1.Events;

namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string message;
        private ControllerInfoItem _selectedItem;        
        public ICommand OpenWebCommand { get; }
        public Command LoadItemsCommand { get; }       
        public ObservableCollection<ControllerInfoItem> Items { get; }        
        
        public int Id { get; }

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public AboutViewModel()
        {
            Title = "Home";
            Items = new ObservableCollection<ControllerInfoItem>();           
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());           
        }        

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public ControllerInfoItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(ControllerInfoItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
                
        public async void TurnOnRelay(int id)
        {
            var controller = await DataStore.GetItemAsync(id);            
            var request = new ControllerRequests();
            var m = request.SendRequestToController(controller.IPAddress, controller.RelayOneCommand);
            await App.Current.MainPage.DisplayAlert(m.MessageTitle, m.Message, "OK");
        }

        public async void TurnOffRelay(int id)
        {
            var controller = await DataStore.GetItemAsync(id);
            var request = new ControllerRequests();
            var m = request.SendRequestToController(controller.IPAddress, controller.RelayTwoCommand);
            await App.Current.MainPage.DisplayAlert(m.MessageTitle, m.Message, "OK");
        }
    }
}