using AppPractica1710.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPractica1710.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedItemspage : ContentPage
    {
        CompletedItemsViewModel ViewModel;
        public CompletedItemspage()
        {
            InitializeComponent();
            ViewModel = new CompletedItemsViewModel();
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.RefreshCommand.Execute(null);
        }
    }
}