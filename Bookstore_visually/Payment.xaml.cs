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
            //Build form and pay button
            var form = new StringBuilder();
            form.Append($"<form method=\"post\" action=\"{url}\" accept-charset=\"utf-8\">\n");
            form.Append($"<input type=\"hidden\" name=\"data\" value=\"{data}\" />\n");
            form.Append($"<input type=\"hidden\" name=\"signature\" value=\"{signature}\" />\n");
            form.Append($"<button type=\"submit\">Pay</button>\n");
            form.Append("</form>\n");

            //CoreWebView2 Innit
            await WebView.EnsureCoreWebView2Async();

            //Render form and pay button in WebView
            WebView.CoreWebView2.NavigateToString(form.ToString());

            //Delegate SourceChanged event
            WebView.SourceChanged += UrlChange;
        }

        public async void StartWebResourceRequest(string url, string data, string signature)
        {
            //Generate POST Request MemStream
            string param = "data=" + data + "&signature=" + signature; // create request string
            byte[] postData = Encoding.UTF8.GetBytes(param); //request string to byte array
            MemoryStream postDataStream = new MemoryStream(postData.Length); // new memory stream by lenght
            postDataStream.Write(postData, 0, postData.Length); //write params byte array to stream
            postDataStream.Seek(0, SeekOrigin.Begin); //set start position to stream

            //Generate HTTP Request Header
            StringBuilder headers = new StringBuilder();
            headers.AppendLine("Content-Type: application/x-www-form-urlencoded");

            
            //CoreWebView2 Innit
            await WebView.EnsureCoreWebView2Async();

            // Generate WebResourceRequest
            var request = WebView.CoreWebView2.Environment.CreateWebResourceRequest(url, "POST", postDataStream, headers.ToString());

            //Send request - Render response
            WebView.CoreWebView2.NavigateWithWebResourceRequest(request);

            //Delegate SourceChanged event
            WebView.SourceChanged += UrlChange;
        }


        //source change event delegate
        void UrlChange(object sender, EventArgs e)
        {
            var obj = (Microsoft.Web.WebView2.Wpf.WebView2)sender;
            //Close WebView Window
            string url = obj.Source.ToString();
            if(url.Contains("checkout/success"))
            {
                Task task = new Task(GetStatus);
                task.Start();
                task.Wait();
                //this.Close();
            }
        }


        public void GetStatus()
        {
            //enumerate orders where no pay status and update
           // var orders = repositoryO.GetAll().Where(x => !x.Payment_status);// bookstoreDBContext.Orders.Where(x => !x.Payment_status).ToList();
            var orders = bookstoreDBContext.Orders.OrderByDescending(c => c.Id).FirstOrDefault();
            //foreach (var item in orders)
            {

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

                //Create web client and set content type in heder
                var webC = new WebClient();
                webC.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                //Send POST request and get response string
                string responseStr = webC.UploadString(new Uri(url), "POST", "data=" + data + "&signature=" + signature);

                //deserialize respons to dictionary
                var response = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(responseStr);

                var status = response.First(x => x.Key == "status").Value.ToString();
                //update order
                var dbEntry = repositoryO.GetById(orders.Id);

                //dbEntry.Payment_status = status == "success" ? true : false;
                if (status == "success")
                {
                    dbEntry.Payment_status = true;
                }
                repositoryO.Update(dbEntry);
                repositoryO.Save();
                //bookstoreDBContext.Orders.Update(dbEntry);
                bookstoreDBContext.SaveChanges();
               
                //end enumarate
            }

        }

    }
}
