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
    /// Interaction logic for AddingGenre.xaml
    /// </summary>
    public partial class AddingGenre : Window
    {
        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();
        public AddingGenre()
        {
            InitializeComponent();
        }

        private void AddGenreBTN(object sender, RoutedEventArgs e)
        {
            if (GenreNameBox.Text.Length > 0)
            {
                var genre = bookstoreDBContext.Genres.Where(g => g.Name == GenreNameBox.Text).FirstOrDefault();
                if (genre == null)
                {
                    Genre genre1 = new Genre();
                    genre1.Name = GenreNameBox.Text;
                    bookstoreDBContext.Genres.Add(genre1);
                    bookstoreDBContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Such a genre already exists!");
                }
            }
            else
            {
                MessageBox.Show("Enter the genre of the book!");
            }
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

        private void closeWind(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
