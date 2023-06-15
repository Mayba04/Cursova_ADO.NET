using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Bookstore;
using MaterialDesignThemes.Wpf;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper;

        BookstoreDBContext bookstoreDBContext;
        ViewModel model;
        public Login()
        {
            InitializeComponent();
            model = new ViewModel();
            paletteHelper = new PaletteHelper();
            bookstoreDBContext = new BookstoreDBContext();

            //test test = new test();
            //test.Show();
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

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void logInfo_Click(object sender, RoutedEventArgs e)
        {
            string login = textUsername.Text;
            string password = txtPassword.Password;

            bool isValidUser = CheckUserCredentials(login, password);

            if (isValidUser)
            {
                var admin = bookstoreDBContext.Administrators.FirstOrDefault(x => x.Login == login);
                var user = bookstoreDBContext.Clients.FirstOrDefault(u => u.Credentials.Login == login);
                if (admin != null)
                {
                    AdminPage adminPage = new AdminPage(admin);
                    this.Close();
                    adminPage.Show();
                }
                else
                {
                    BookstoreView bookstore = new BookstoreView(user);
                    this.Close();
                    bookstore.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid login details");
            }
        }

        private bool CheckUserCredentials(string login, string password)
        {
            var user = bookstoreDBContext.Credentials.FirstOrDefault(x => x.Login == login);
            var admin = bookstoreDBContext.Administrators.FirstOrDefault(x => x.Login == login);

            if (user != null && user.Password == password || admin != null && admin.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void signupInfo_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            this.Close();
            registerWindow.ShowDialog();
        }

        private void minApp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Help_Button(object sender, RoutedEventArgs e)
        {
            string url = "https://docs.google.com/document/d/1sQKBGx-NLaHek8adIyP6l707hsCWU9U_f0UxksTkG5g/edit?usp=sharing"; 

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
