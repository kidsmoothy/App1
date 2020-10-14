using System.ComponentModel;
using System;
using Xamarin.Forms;
using App1.ViewModels;
using System.Dynamic;
using App1.Models;
using App1.Services;
using System.Net;

namespace App1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailViewModel _viewModel;

        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemDetailViewModel();
        }


        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Alert", "Are you sure you want to dete this control?", "Yes", "No");

            if (answer)
            {
                _viewModel.OnDelete();
            }
        }
    }
}