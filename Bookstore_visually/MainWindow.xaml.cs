using Bookstore;
using Bookstore.Entities;
using Bookstore.Repository;
using Bookstore_visually;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository<Credentials> repository = new Repository<Credentials>(new BookstoreDBContext());
         

        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            bool isValidUser = CheckUserCredentials(login, password);

            if (isValidUser)
            {
                ///MessageBox.Show("Welcome to the bookstore");
                
                var Credentials = bookstoreDBContext.Credentials.FirstOrDefault(x => x.Login == login);
                var user = bookstoreDBContext.Clients.FirstOrDefault(u => u.Credentials.Login == login);
                if (user.Status_admin == true)
                {
                    AdminPage adminPage = new AdminPage();
                    this.Close();
                    adminPage.Show();
                }
                else
                {
                    BookstoreView bookstore = new BookstoreView(Credentials);
                    this.Close();
                    bookstore.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid login details");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            this.Close();
            registerWindow.ShowDialog();  
        }

        private bool CheckUserCredentials(string login, string password)
        {
            var user = bookstoreDBContext.Credentials.FirstOrDefault(x => x.Login == login);

            if (user != null && user.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}
