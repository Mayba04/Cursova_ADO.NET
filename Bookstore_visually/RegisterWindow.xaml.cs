using Bookstore.Entities;
using Bookstore.Repository;
using Bookstore;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        IRepository<Credentials> repository = new Repository<Credentials>(new BookstoreDBContext());


        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();
        bool CredentialsCreate;

        public RegisterWindow()
        {
            InitializeComponent();
            CredentialsCreate = false;
        }

        private void CreateCredentials_Click(object sender, RoutedEventArgs e)
        {
            
            if(LoginTextBox.Text.Length > 0)
            {
                if (PasswordBox.Text.Length > 0)
                {
                    if (ConfirmPasswordBox.Text.Length > 0)
                    {
                        var user = bookstoreDBContext.Credentials.FirstOrDefault(x => x.Login == LoginTextBox.Text);
                        if (user == null)
                        {
                            if (PasswordBox.Text == ConfirmPasswordBox.Text)
                            {
                                var newCredentials = new Credentials
                                {
                                    Login = LoginTextBox.Text,
                                    Password = PasswordBox.Text
                                };

                                bookstoreDBContext.Credentials.Add(newCredentials);
                                bookstoreDBContext.SaveChanges();
                                CredentialsCreate = true;
                                CreateCredentials.IsEnabled = false;
                                LoginTextBox.IsEnabled = false;
                                PasswordBox.IsEnabled = false;
                                ConfirmPasswordBox.IsEnabled = false;
                                CreateAccount.IsEnabled = true;
                                NameTextBox.IsEnabled = true;
                                EmailTextBox.IsEnabled = true;
                            }
                            else
                            {
                                MessageBox.Show("Passwords do not match");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Such a user already exists with such a name");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter your confirm passwords");
                    }

                }
                else
                {
                    MessageBox.Show("Enter your password");
                }               
            }
            else
            {
                MessageBox.Show("Enter your login");
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text.Length>0)
            {
                if (NameTextBox.Text.Length>0)
                {
                    var client = bookstoreDBContext.Clients.FirstOrDefault(x => x.Email == EmailTextBox.Text);
                    if (client == null)
                    {
                        if (CredentialsCreate == true)
                        {
                            var latestCredentials = bookstoreDBContext.Credentials.OrderByDescending(c => c.Id).FirstOrDefault();
                            var newClient = new Client
                            {
                                CredentialsId = latestCredentials.Id,
                                Name = NameTextBox.Text,
                                Email = EmailTextBox.Text,
                                Status_admin = false,
                            };

                            bookstoreDBContext.Clients.Add(newClient);
                            bookstoreDBContext.SaveChanges();
                            LoginTextBox.Clear();
                            PasswordBox.Clear();
                            ConfirmPasswordBox.Clear();
                            EmailTextBox.Clear();
                            NameTextBox.Clear();
                            Login mainWindow = new Login();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("credentials not created");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A user with this email address already exists");
                    }
                }
                else
                {
                    MessageBox.Show("Enter your name");
                }
            }
            else
            {
                MessageBox.Show("Enter your email");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (CredentialsCreate == true)
            {
                var latestCredentials = bookstoreDBContext.Credentials.OrderByDescending(c => c.Id).FirstOrDefault();
                bookstoreDBContext.Credentials.Remove(latestCredentials);
                bookstoreDBContext.SaveChanges();
                CredentialsCreate = false;
            }
            LoginTextBox.Clear();
            PasswordBox.Clear();
            ConfirmPasswordBox.Clear();
            EmailTextBox.Clear();
            NameTextBox.Clear();
            Login mainWindow = new Login();
            mainWindow.Show();
            this.Close();
        }
    }
}
