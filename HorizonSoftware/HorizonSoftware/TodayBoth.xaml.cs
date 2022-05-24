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
    public partial class TodayBoth : ContentPage
    {
        public class mysqlList
        {
            public string ANumber { get; set; }
            public string AHolder { get; set; }
            public int DAmount { get; set; }
            public int LAmount { get; set; }

        }
        public ObservableCollection<string> mysqlLists { get; set; }
        ObservableCollection<string> data = new ObservableCollection<string>();

        SqlConnection sqlConnection;

        public TodayBoth()
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
            string queryString = "Select Lo.[AccountNumber],Lo.[AccountHolder],[RecievedAmount],[Amount] from dbo.LoanTable lo inner join dbo.DepositeTable de on lo.AccountNumber = de.AccountNumber Union all select AcNo='',AcName='Total',LAmount=sum(lo.[RecievedAmount]),Amount=sum(de.[Amount]) from DepositeTable de inner join dbo.LoanTable lo on de.AccountNumber = lo.AccountNumber";
            //string DTotal = "120000";
            //string LTotal = "1200250";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                mysqlLists.Add(new mysqlList
                {
                    ANumber = reader["AccountNumber"].ToString(),
                    AHolder = reader["AccountHolder"].ToString(),
                    DAmount = Convert.ToInt32(reader["Amount"]),
                    LAmount = Convert.ToInt32(reader["RecievedAmount"]),
                }
                );
                //txtDepositTotal.Text = DTotal.ToString();
                //txtLoanTotal.Text = LTotal.ToString();
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

        private void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
       
            Navigation.PushAsync(new TodayLoan());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

   
        
    }
}