using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorizonSoftware
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepositePage : ContentPage
    {
        public class mysqlList
        {
            public string ANumber { get; set; }
            public string Amount { get; set; }
        }

        //ObservableCollection<mysqlList> mysqlLists = new ObservableCollection<mysqlList>();
        //public ObservableCollection<mysqlList> AccountNumberList { get { return accountNumbers; } }
        public ObservableCollection<string> mysqlLists { get; set; }
        ObservableCollection<string> data = new ObservableCollection<string>();

        SqlConnection sqlConnection;

        //public string BindingHolder { get;  set; }

        public DepositePage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.96";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
            //ListOfStore();

        }


        //public async void ListOfStore() //List of Countries  
        //{
        //    try
        //    {
        //        data.Add("Afghanistan");
        //        data.Add("Austria");
        //        data.Add("Australia");
        //        data.Add("Azerbaijan");
        //        data.Add("Bahrain");
        //        data.Add("Bangladesh");
        //        data.Add("Belgium");
        //        data.Add("Botswana");
        //        data.Add("China");
        //        data.Add("Colombia");
        //        data.Add("Denmark");
        //        data.Add("Kmart");
        //        data.Add("Pakistan");
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("", "" + ex, "Ok");
        //    }
        //}











                             //EnergySaverStatus button click

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                //if (Amount != null && !string.IsNullOrWhiteSpace(Amount.Text))
              
                if(Amount.Text==null || Amount.Text=="")
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Please enter amount ", "Ok");
                    return;
                }
                sqlConnection.Open();
                string Accountnumber = AccountNumber.Text;
                var amount = Convert.ToInt32(Amount.Text);
                var sql = $"update dbo.DepositeTable set Amount=Amount+" +amount+" WHERE AccountNumber='"+Accountnumber+"'";
                using (var Command = new SqlCommand(sql, sqlConnection))
                {
                   int reader = Command.ExecuteNonQuery();
                    if (reader != 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Successfull", "Ok");
                        _ = Navigation.PushAsync(new HomePage());
                    }

                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Something Where Wrong, Please Check! ", "Ok");
                    }
                    //Command.Close();
                    Command.Dispose();
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Somthing Went wrong!", "Ok");
                //throw new Exception("something wrong");
            }

        }








                                       //Search button click

        private async void AccountNumber_SearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                
                List<mysqlList> mysqlLists = new List<mysqlList>();
                sqlConnection.Open();
                string queryString = $"select * from dbo.DepositeTable where AccountNumber='{AccountNumber.Text}'";

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    mysqlLists.Add(new mysqlList
                    {
                        ANumber = reader["AccountNumber"].ToString(),
                        //Holder = reader["AccountHolder"].ToString(),
                    }
                    );
                    AccountNumber.Text = reader["AccountNumber"].ToString();
                    AccountHolder.Text = reader["AccountHolder"].ToString();
                    //BindingHolder = reader["AccountHolder"].ToString();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "AccountNumber is incorrect.", "Ok");
                }

                reader.Close();
                reader.Dispose();
                sqlConnection.Close();
            }
            //myListView.ItemsSource = mysqlLists;


            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }




                                  //cancle button click

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }








        //private async void AccountNumber_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //myListView.ItemsSource = myList.Where(s => s.Number.StartsWith(e.NewTextValue));
        //    //myListView.ItemsSource =  mysqlLists.Where(s => s.Number.StartsWith(e.NewTextValue));
        //    try
        //    {

        //        List<mysqlList> mysqlLists = new List<mysqlList>();
        //        sqlConnection.Open();
        //        string queryString = $"select AccountNumber from dbo.DepositeTable Where AccountNumber like '%{e.NewTextValue}%'";

        //        SqlCommand command = new SqlCommand(queryString, sqlConnection);
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            mysqlLists.Add(new mysqlList
        //            {
        //                ANumber = reader["AccountNumber"].ToString(),
        //            }
        //            );
        //        }
        //        reader.Close();
        //        sqlConnection.Close();
        //        myListView.ItemsSource = mysqlLists;
        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
        //        throw;
        //    }
        //}


        //private async void AccountNumber_Focused(object sender, FocusEventArgs e)
        //{
        //    try
        //    {
        //        List<mysqlList> mysqlLists = new List<mysqlList>();
        //        sqlConnection.Open();
        //        string queryString = "select AccountNumber from dbo.DepositeTable";

        //        SqlCommand command = new SqlCommand(queryString, sqlConnection);
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            mysqlLists.Add(new mysqlList
        //            {
        //                ANumber = reader["AccountNumber"].ToString(),
        //            }
        //            );
        //        }
        //        reader.Close();
        //        //IsVisible = false;
        //        sqlConnection.Close();
        //        myListView.ItemsSource = mysqlLists;

        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
        //        throw;
        //    }


        //}






        //private void myListView_ItemTapped_1(object sender, ItemTappedEventArgs e)
        //{


        //    String listsd = e.Item as string;
        //    AccountNumber.Text = listsd;
        //    myListView.IsVisible = false;

        //    ((ListView)sender).SelectedItem = null;
        //}












        //private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    myListView.IsVisible = true;
        //    myListView.BeginRefresh();

        //    try
        //    {
        //        var dataEmpty = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));

        //        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        //            myListView.IsVisible = false;
        //        else if (dataEmpty.Max().Length == 0)
        //            myListView.IsVisible = false;
        //        else
        //            myListView.ItemsSource = data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
        //    }
        //    catch (Exception ex)
        //    {
        //        myListView.IsVisible = false;

        //    }
        //    myListView.EndRefresh();
        //}





        //private void myListView_ItemTapped_1(object sender, ItemTappedEventArgs e)
        //{
        //    //EmployeeListView.IsVisible = false;  

        //    String listsd = e.Item as string;
        //    AccountNumber.Text = listsd;
        //    myListView.IsVisible = false;

        //    ((ListView)sender).SelectedItem = null;
        //}
    }
}