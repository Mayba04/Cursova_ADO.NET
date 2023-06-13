using Bookstore;
using Bookstore.Entities;
using Bookstore.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        IRepository<Order> repositoryO = new Repository<Order>(new BookstoreDBContext());
        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();

        public Payment()
        {
            InitializeComponent();
        }

        public async void StartPayButton(string url, string data, string signature)
        {
            var form = new StringBuilder();
            form.Append($"<form method=\"post\" action=\"{url}\" accept-charset=\"utf-8\">\n");
            form.Append($"<input type=\"hidden\" name=\"data\" value=\"{data}\" />\n");
            form.Append($"<input type=\"hidden\" name=\"signature\" value=\"{signature}\" />\n");
            form.Append($"<button type=\"submit\">Pay</button>\n");
            form.Append("</form>\n");

            await WebView.EnsureCoreWebView2Async();

            WebView.CoreWebView2.NavigateToString(form.ToString());

            WebView.SourceChanged += UrlChange;
        }

        public async void StartWebResourceRequest(string url, string data, string signature)
        {
            string param = "data=" + data + "&signature=" + signature;
            byte[] postData = Encoding.UTF8.GetBytes(param); 
            MemoryStream postDataStream = new MemoryStream(postData.Length); 
            postDataStream.Write(postData, 0, postData.Length); 
            postDataStream.Seek(0, SeekOrigin.Begin); 

            StringBuilder headers = new StringBuilder();
            headers.AppendLine("Content-Type: application/x-www-form-urlencoded");

            await WebView.EnsureCoreWebView2Async();

            var request = WebView.CoreWebView2.Environment.CreateWebResourceRequest(url, "POST", postDataStream, headers.ToString());

            WebView.CoreWebView2.NavigateWithWebResourceRequest(request);

            WebView.SourceChanged += UrlChange;
        }

        void UrlChange(object sender, EventArgs e)
        {
            var obj = (Microsoft.Web.WebView2.Wpf.WebView2)sender;
            string url = obj.Source.ToString();
            if(url.Contains("checkout/success"))
            {
                Task task = new Task(GetStatus);
                task.Start();
                task.Wait();
            }
        }


        public void GetStatus()
        {
            var orders = bookstoreDBContext.Orders.OrderByDescending(c => c.Id).FirstOrDefault();
            var dataJson = new
            {
                version = "3",
                action = "status",
                public_key = Config.LiqyPublicKey,
                order_id = orders.Id.ToString()
            };
            
            string jsonDataString = System.Text.Json.JsonSerializer.Serialize(dataJson);

            string data = LiqyPayHelper.CreateData(jsonDataString);
            string signature = LiqyPayHelper.CreateSign(data, Config.LiqypayPrivateKey);

            string url = Config.LiqyPayRequestURL;
            
            var webC = new WebClient();
            webC.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            
            string responseStr = webC.UploadString(new Uri(url), "POST", "data=" + data + "&signature=" + signature);
            
            var response = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(responseStr);
            var status = response.First(x => x.Key == "status").Value.ToString();
            
            var dbEntry = repositoryO.GetById(orders.Id);
            
            if (status == "success")
            {
                dbEntry.Payment_status = true;
            }
            
            repositoryO.Update(dbEntry);
            repositoryO.Save();

            bookstoreDBContext.SaveChanges();
        }

    }
}
