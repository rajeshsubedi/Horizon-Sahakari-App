using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorizonSoftware
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodayLoan : ContentPage
    {
        public class mysqlList
        {
            public string ANumber { get; set; }
            public string AHolder { get; set; }
            public int Amount { get; set; }

        }

        public ObservableCollection<string> mysqlLists { get; set; }
        ObservableCollection<string> data = new ObservableCollection<string>();


        SqlConnection sqlConnection;

        public TodayLoan()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.96";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
            List<mysqlList> mysqlLists = new List<mysqlList>();
            sqlConnection.Open();
            string queryString = "select AccountNumber,AccountHolder,RecievedAmount from dbo.LoanTable Union all select AcNo='',AcName='Total',Amount=sum(RecievedAmount) from LoanTable ";
            //string Total = "120000";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                mysqlLists.Add(new mysqlList
                {
                    ANumber = reader["AccountNumber"].ToString(),
                    AHolder = reader["AccountHolder"].ToString(),
                    Amount = Convert.ToInt32(reader["RecievedAmount"]),
                }
                );
                //txtTotal.Text = Total.ToString();
            }
            reader.Close();
            //IsVisible = false;
            sqlConnection.Close();
            MyListView.ItemsSource = mysqlLists;
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
      
            Navigation.PushAsync(new TodayPage());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

      

        private void RadioButton_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
           
            Navigation.PushAsync(new TodayBoth());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}