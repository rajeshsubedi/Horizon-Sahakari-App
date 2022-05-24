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
    public partial class TodayPage : ContentPage
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

        public TodayPage()
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
            string queryString = "select AccountNumber,AccountHolder,Amount from dbo.DepositeTable Union all select AcNo='',AcName='Total',Amount=sum(Amount) from DepositeTable ";
            //string query = "SELECT SUM(Amount) as SUM from dbo.DepositeTable";
            //SqlCommand comm = new SqlCommand(query, sqlConnection);
            //SqlDataReader read = comm.ExecuteReader();

            //string total = "0";
            //while (read.Read())
            //    total = read["SUM"].ToString();
            //read.Close();
            //string total = "120000";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                mysqlLists.Add(new mysqlList
                {
                    ANumber = reader["AccountNumber"].ToString(),
                    AHolder = reader["AccountHolder"].ToString(),
                    Amount = Convert.ToInt32(reader["Amount"]),
                }
                );
                //txtTotal.Text = reader["Total"].ToString();
                //txtTotal.Text = total.ToString();
            }

            reader.Close();
            //reader.Dispose();
            sqlConnection.Close();
            MyListView.ItemsSource = mysqlLists;


        }
     

  


        

            private  void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {

            Navigation.PushAsync(new TodayLoan());
            //Navigation.RemovePage(new DepositePage());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private void RadioButton_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
        
            Navigation.PushAsync(new TodayBoth());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}