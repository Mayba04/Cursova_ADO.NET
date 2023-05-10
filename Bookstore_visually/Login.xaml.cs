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
    }
}
