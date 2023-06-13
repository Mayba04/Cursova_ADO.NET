using Bookstore;
using Bookstore.Entities;
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

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for AddingAuthors.xaml
    /// </summary>
    public partial class AddingAuthors : Window
    {
        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();

        public AddingAuthors()
        {
            InitializeComponent();
        }

        private void AddAthorBTN(object sender, RoutedEventArgs e)
        {
            AddAthor();
        }

        private void AddAthor()
        {
            if (AuthorNameBox.Text.Length > 0)
            {
                if (AuthorSurnameBox.Text.Length > 0)
                {
                    var authordb = bookstoreDBContext.Authors.Where(a => a.Name == AuthorNameBox.Text && a.Surname == AuthorSurnameBox.Text).FirstOrDefault();
                    if (authordb == null)
                    {
                        Authors author = new Authors();
                        author.Name = AuthorNameBox.Text;
                        author.Surname = AuthorSurnameBox.Text;
                        bookstoreDBContext.Authors.Add(author);
                        bookstoreDBContext.SaveChanges();
                        MessageBox.Show("Added successfully");
                    }
                    else
                    {
                        MessageBox.Show("Such an author already exists");
                    }

                }
                else
                {
                    MessageBox.Show("Enter surname Author");
                }
            }
            else
            {
                MessageBox.Show("Enter name Author!");
            }
        }


        private void closebtn(object sender, RoutedEventArgs e)
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
