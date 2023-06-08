using Bookstore;
using Bookstore.Entities;
using Microsoft.VisualBasic.ApplicationServices;
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
        bool Checked;
        public ChangeDC(int id)
        {
            InitializeComponent();
            var credential = bookstoreDBContext.Credentials.Where(c => c.Id == id).FirstOrDefault();
            var client = bookstoreDBContext.Clients.Where(c => c.CredentialsId == id).FirstOrDefault();
            credentialsf = credential;
            clientf = client;
            LoginBox.Text = credential.Login;
            PasswordBox.Text = credential.Password;
            NameBox.Text = client.Name;
            EmailBox.Text = client.Email;
            PhoneTextBox.Text = clientf.PhoneNumber;
            Checked = false;
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

            if (Checked!=true)
            {
                if (Audit())
                {
                    clientf.Email = EmailBox.Text;
                    clientf.Name = NameBox.Text;
                    credentialsf.Login = LoginBox.Text;
                    credentialsf.Password = PasswordBox.Text;
                    clientf.PhoneNumber = PhoneTextBox.Text;

                    bookstoreDBContext.Clients.Update(clientf);
                    bookstoreDBContext.Credentials.Update(credentialsf);
                    bookstoreDBContext.SaveChanges();
                    MessageBox.Show("Saved!");
                }
            }
            else
            {
                if (Audit())
                {
                    Admin admin = new Admin();
                    admin.Login = LoginBox.Text;
                    admin.Password = PasswordBox.Text;
                    admin.Email = EmailBox.Text;
                    admin.Name = NameBox.Text;
                    admin.PhoneNumber = PhoneTextBox.Text;
                    bookstoreDBContext.Administrators.Update(admin);
                    bookstoreDBContext.Credentials.Remove(bookstoreDBContext.Credentials.Where(b => b.Id == credentialsf.Id).FirstOrDefault());
                    bookstoreDBContext.Clients.Remove(bookstoreDBContext.Clients.Where(b => b.CredentialsId == clientf.CredentialsId).FirstOrDefault());
                    bookstoreDBContext.SaveChanges();
                    MessageBox.Show($"Saved! {admin.Login} saved and moved to administration");
                    this.Close();
                }
            }           
        }


        private bool Audit()
        {
            if (credentialsf.Login != LoginBox.Text)
            {
                if (string.IsNullOrEmpty(LoginBox.Text))
                {
                    MessageBox.Show("Enter your login");
                    return false;
                }

                if (LoginBox.Text.Length < 4 || LoginBox.Text.Length > 20)
                {
                    MessageBox.Show("Login should be between 4 and 20 characters long");
                    return false;
                }

                if (!char.IsUpper(LoginBox.Text[0]))
                {
                    MessageBox.Show("Login should start with an uppercase letter");
                    return false;
                }

                if (!LoginBox.Text.All(c => char.IsLetterOrDigit(c) || c == '_'))
                {
                    MessageBox.Show("Login should only contain letters, digits, and underscores");
                    return false;
                }
            }

            if (credentialsf.Password != PasswordBox.Text)
            {
                if (string.IsNullOrEmpty(PasswordBox.Text))
                {
                    MessageBox.Show("Enter your password");
                    return false;
                }

                if (!IsPasswordValid(PasswordBox.Text))
                {
                    MessageBox.Show("Password should be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, and one digit.");
                    return false;
                }
            }
            ///
            if (clientf.Email != EmailBox.Text)
            {
                if (string.IsNullOrEmpty(EmailBox.Text))
                {
                    MessageBox.Show("Enter your email address");
                    return false;
                }

                if (!IsValidEmail(EmailBox.Text))
                {
                    MessageBox.Show("Enter a valid email address");
                    return false;
                }

                var user = bookstoreDBContext.Clients.FirstOrDefault(x => x.Email == EmailBox.Text);
                var user2 = bookstoreDBContext.Administrators.FirstOrDefault(x => x.Email == EmailBox.Text);
                if (user != null || user2 != null)
                {
                    MessageBox.Show("A user with this login already exists");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(NameBox.Text))
            {
                MessageBox.Show("Enter your Name");
                return false;
            }

            if (!ValidateName(NameBox.Text))
            {
                MessageBox.Show("Name should start with an uppercase letter and contain 4 to 20 characters (letters, numbers, spaces, dashes, underscores).");
                return false;
            }


            if (string.IsNullOrEmpty(PhoneTextBox.Text))
            {
                MessageBox.Show("Enter your Phone");
                return false;
            }


            if (!ValidatePhone(PhoneTextBox.Text))
            {
                MessageBox.Show("The phone number is incorrect, example: +380123456789");
                return false;
            }

            if (clientf.PhoneNumber != PhoneTextBox.Text)
            {
                var phone = bookstoreDBContext.Clients.FirstOrDefault(x => x.PhoneNumber == PhoneTextBox.Text);
                var phone2 = bookstoreDBContext.Administrators.FirstOrDefault(x => x.PhoneNumber == PhoneTextBox.Text);
                if (phone != null || phone2 != null)
                {
                    MessageBox.Show("A user with this phone number already exists");
                    return false;
                }
            }
           

            return true;
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

        private bool ValidatePhone(string phone)
        {
            Regex regex = new Regex(@"^\+380\d{9}$");
            return regex.IsMatch(phone);
        }



        private void closeWind(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minApp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
