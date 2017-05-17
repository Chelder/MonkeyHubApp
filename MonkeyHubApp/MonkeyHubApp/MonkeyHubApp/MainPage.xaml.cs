using MonkeyHubApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        //Não é a forma MVVM
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation?.PushAsync(new MainPage());
        }

        private void ButtonModal_Clicked(object sender, EventArgs e)
        {
            Navigation?.PushModalAsync(new NavigationPage(new MainPage()));
        }

        private void ButtonVoltarModal_Clicked(object sender, EventArgs e)
        {            
            Navigation?.PopModalAsync();
        }
    }
}
