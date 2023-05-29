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
using MaterialDesignThemes.Wpf;
using System.Text.RegularExpressions;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper;
        BookstoreDBContext bookstoreDBContext;
        bool CredentialsCreate;

        public RegisterWindow()
        {
            InitializeComponent();
            CredentialsCreate = false;
            paletteHelper = new PaletteHelper();
            bookstoreDBContext = new BookstoreDBContext();
        }

        private void CreateCredentials_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                MessageBox.Show("Enter your login");
                return;
            }

            if (LoginTextBox.Text.Length < 4 || LoginTextBox.Text.Length > 20)
            {
                MessageBox.Show("Login should be between 4 and 20 characters long");
                return;
            }

            if (!char.IsUpper(LoginTextBox.Text[0]))
            {
                MessageBox.Show("Login should start with an uppercase letter");
                return;
            }

            if (!LoginTextBox.Text.All(c => char.IsLetterOrDigit(c) || c == '_'))
            {
                MessageBox.Show("Login should only contain letters, digits, and underscores");
                return;
            }

            if (string.IsNullOrEmpty(PasswordBox.Text))
            {
                MessageBox.Show("Enter your password");
                return;
            }

            if (!IsPasswordValid(PasswordBox.Text))
            {
                MessageBox.Show("Password should be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, and one digit.");
                return;
            }

            if (string.IsNullOrEmpty(ConfirmPasswordBox.Text))
            {
                MessageBox.Show("Enter your confirm password");
                return;
            }

            if (PasswordBox.Text != ConfirmPasswordBox.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            var user = bookstoreDBContext.Credentials.FirstOrDefault(x => x.Login == LoginTextBox.Text);
            var user2 = bookstoreDBContext.Administrators.FirstOrDefault(x => x.Login == LoginTextBox.Text);
            if (user != null || user2 != null)
            {
                MessageBox.Show("A user with this login already exists");
                return;
            }


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

        private bool IsPasswordValid(string password)
        {
            var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            return passwordRegex.IsMatch(password);
        }

        private bool IsValidEmail(string email)
        {
            string emailRegexPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(emailRegexPattern);
            return regex.IsMatch(email);
        }

        private bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (name.Length < 4 || name.Length > 20)
                return false;

            if (!char.IsUpper(name[0]))
                return false;

            var allowedCharacters = new Regex(@"^[a-zA-Z0-9\s_-]+$");
            if (!allowedCharacters.IsMatch(name))
                return false;

            return true;
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                MessageBox.Show("Enter your email address");
                return;
            }

            if (!IsValidEmail(EmailTextBox.Text))
            {
                MessageBox.Show("Enter a valid email address");
                return;
            }

            var user = bookstoreDBContext.Clients.FirstOrDefault(x => x.Email == EmailTextBox.Text);
            var user2 = bookstoreDBContext.Administrators.FirstOrDefault(x => x.Email == EmailTextBox.Text);
            if (user != null || user2 != null)
            {
                MessageBox.Show("A user with this login already exists");
                return;
            }

            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Enter your Name");
                return;
            }

            if (!ValidateName(NameTextBox.Text))
            {
                MessageBox.Show("Name should start with an uppercase letter and contain 4 to 20 characters (letters, numbers, spaces, dashes, underscores).");
                return;
            }

            var latestCredentials = bookstoreDBContext.Credentials.OrderByDescending(c => c.Id).FirstOrDefault();
            var newClient = new Client
            {
                CredentialsId = latestCredentials.Id,
                Name = NameTextBox.Text,
                Email = EmailTextBox.Text,
            };

            bookstoreDBContext.Clients.Add(newClient);
            bookstoreDBContext.SaveChanges();
            MessageBox.Show("Account created successfully!");
            LoginTextBox.Clear();
            PasswordBox.Clear();
            ConfirmPasswordBox.Clear();
            EmailTextBox.Clear();
            NameTextBox.Clear();
            Login mainWindow = new Login();
            mainWindow.Show();
            this.Close();

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

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minApp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
