using Bookstore;
using Bookstore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for ChangeDC.xaml
    /// </summary>
    public partial class ChangeDC : Window
    {
        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();
        Credentials credentialsf;
        Client clientf;
        string emailPattern;
        bool Checked;
        public ChangeDC(int id)
        {
            InitializeComponent();
            var credential = bookstoreDBContext.Credentials.Where(c => c.Id == id).FirstOrDefault();
            var client = bookstoreDBContext.Clients.Where(c => c.CredentialsId == id).FirstOrDefault();
            credentialsf = credential;
            clientf = client;
            LoginBox.Text = credential.Login;
            PaswordBox.Text = credential.Password;
            NameBox.Text = client.Name;
            EmailBox.Text = client.Email;
            Checked = false;
            if (client.Status_admin)
            {
                RadioBTNTrue.IsChecked = true;
            }
            else
            {
                RadioBTNTrue.IsChecked = false;
            }
            emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            RadioBTNTrue.Click += RadioBTNTrue_Click;
            RadioBTNFalse.Click += RadioBTNFalse_Click; ;
            RadioBTNTrue.IsThreeState = true;
        }

        private void RadioBTNFalse_Click(object sender, RoutedEventArgs e)
        {
           Checked = false;
        }

        private void RadioBTNTrue_Click(object sender, RoutedEventArgs e)
        {
            if (Checked != true)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to grant administrator rights to this user?", "Grant administrator rights", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    RadioBTNTrue.IsChecked = true;
                    Checked = true;
                    MessageBox.Show("Administrator rights will be granted when the changes are applied.", "Success");
                }
                else
                {
                    RadioBTNFalse.IsChecked = true;
                    Checked = false;
                }
            }
            
        }

        private void ChangeBtn(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(EmailBox.Text, emailPattern))
            {
                clientf.Email = EmailBox.Text;
                clientf.Name = NameBox.Text;
                clientf.Status_admin = Checked;
                credentialsf.Login = LoginBox.Text;
                credentialsf.Password = PaswordBox.Text;
                MessageBox.Show("Applied");
            }
            else
            {
                MessageBox.Show("The email was entered incorrectly. Please check the correctness of the input.");
            }
           
            bookstoreDBContext.Clients.Update(clientf);
            bookstoreDBContext.Credentials.Update(credentialsf);
            bookstoreDBContext.SaveChanges();
        }

       
    }
}
