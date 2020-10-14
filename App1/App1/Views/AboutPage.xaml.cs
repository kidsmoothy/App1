using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.ViewModels;
using App1.Models;
using System.Linq;
using System.Net;

namespace App1.Views
{
    public partial class AboutPage : ContentPage
    {        
        AboutViewModel _viewModel;

        public AboutPage()
        {
            InitializeComponent();                        
            BindingContext = _viewModel = new AboutViewModel();                      
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        void OnRelayOn(object sender, EventArgs e)
        {
            var button = sender as Button;
            _viewModel.TurnOnRelay(Int32.Parse(button.ClassId));           
        }

        private void OnRelayoff(object sender, EventArgs e)
        {
            var button = sender as Button;
            _viewModel.TurnOffRelay(Int32.Parse(button.ClassId));            
        }
    }
}