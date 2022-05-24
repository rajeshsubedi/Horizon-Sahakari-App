using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorizonSoftware
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageDetail : ContentPage
    {
        public HomePageDetail()
        {
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.Black;
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DepositePage());

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoanPage());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TodayPage());
        }

   


        private async void Close_Clicked_1(object sender, EventArgs e)
        {
            var result = await this.DisplayAlert("Alert!", "Do you want to Logout?", "yes", "No");
            if (result == true)
            {
                _ = Navigation.PushAsync(new LoginPage());
            }
            else
            {
                //_ = Navigation.PushAsync(new HomePageDetail());
            }
        }




        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");

                if (result == true)
                {
                    /*    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();*/ // Or anything else
                    _ = Navigation.PushAsync(new LoginPage());
                }
            });
            return true;
        }
    }
}