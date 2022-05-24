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
    public partial class PopupView
    {

        public class mysqlList
        {
            public string ANumber { get; set; }
            //public string Holder { get; set; }

        }


        public ObservableCollection<string> Items { get; set; }

        public PopupView()
        {

            //SqlConnection sqlConnection;
            InitializeComponent();
            //string srvrdbname = "mydb";
            //string srvrname = "192.168.1.96";
            //string srvrusername = "Rajesh";
            //string srvrpassword = "12345";
            //string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            //sqlConnection = new SqlConnection(sqlconn);
            //sqlConnection.Open();
            //List<mysqlList> mysqlLists = new List<mysqlList>();
            //string queryString = "select AccountNumber from dbo.DepositTable";

            //SqlCommand command = new SqlCommand(queryString, sqlConnection);
            //SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    mysqlLists.Add(new mysqlList
            //    {
            //        ANumber = reader["AccountNumber"].ToString(),
            //    }
            //    );
            //}
            //reader.Close();
            //sqlConnection.Close();
            //myListView.ItemsSource = mysqlLists;
        }





        private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}