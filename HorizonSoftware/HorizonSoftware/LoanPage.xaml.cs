using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorizonSoftware
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoanPage : ContentPage
    {
        public class mysqlList
        {
            public string AccountNumber { get; set; }
            public string AccountHolder { get; set; }
            public string PrincipleAmount { get; set; }
            public string InterestAmount { get; set; }
            public string FinePrinciple { get; set; }
            public string FineInterest { get; set; }
            public string TotalDue { get; set; }
        }


        SqlConnection sqlConnection;
        public LoanPage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.96";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }




        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                //if (Amount != null && !string.IsNullOrWhiteSpace(Amount.Text))

                if (RecievedAmount.Text == null || RecievedAmount.Text == "")
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Please enter amount ", "Ok");
                    return;
                }
                sqlConnection.Open();
                string Accountnumber = AccountNumber.Text;
                var amount = Convert.ToInt32(RecievedAmount.Text);
                var sql = $"update dbo.LoanTable set RecievedAmount=RecievedAmount+" +amount+ " WHERE AccountNumber='" + Accountnumber + "'";
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











        private void Button_Clicked_1(object sender, EventArgs e)
        {

            Navigation.PushAsync(new HomePage());
        }



        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            //Console.WriteLine(AccountNumber.Text);
            //await App.Current.MainPage.DisplayAlert("Alert",AccountNumber.Text, "Ok");
            try
            {

                List<mysqlList> mysqlLists = new List<mysqlList>();
                sqlConnection.Open();
                string queryString = $"Select AccountNumber,AccountHolder,PrincipleAmount,InterestAmount,FinePrinciple,FineInterest,TotalDue from dbo.LoanTable WHERE AccountNumber='{AccountNumber.Text}'";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mysqlLists.Add(new mysqlList
                    {
                        AccountNumber = reader["AccountNumber"].ToString(),
                        AccountHolder = reader["AccountHolder"].ToString(),
                        PrincipleAmount = reader["PrincipleAmount"].ToString(),
                        InterestAmount = reader["InterestAmount"].ToString(),
                        FinePrinciple = reader["FinePrinciple"].ToString(),
                        FineInterest = reader["FineInterest"].ToString(),
                        TotalDue = reader["TotalDue"].ToString(),
                      
                    }
                    );
                    AccountNumber.Text = reader["AccountNumber"].ToString();
                    AccountHolder.Text = reader["AccountHolder"].ToString();
                    PrincipleAmount.Text = reader["PrincipleAmount"].ToString();
                    InterestAmount.Text = reader["InterestAmount"].ToString();
                    FinePrinciple.Text = reader["FinePrinciple"].ToString();
                    FineInterest.Text = reader["FineInterest"].ToString();
                    TotalDue.Text = reader["TotalDue"].ToString();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "AccountNumber is incorrect.", "Ok");
                }

                reader.Close();
                reader.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }
    }
}