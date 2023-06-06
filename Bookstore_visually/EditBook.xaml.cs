using Bookstore;
using Bookstore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for EditBook.xaml
    /// </summary>
    public partial class EditBook : Window
    {
        BookstoreDBContext bookstoreDBContext;
        Book book1;
        Authors author;
        Genre genre;
        BookAuthor bookAuthor;
        BookGenre bookGenre;
        int IdAuthor;
        int IdGenre;
        public EditBook(int bookid)
        {   
            InitializeComponent();
            bookstoreDBContext = new BookstoreDBContext();
            book1 = bookstoreDBContext.Books.Where(b => b.Id == bookid).FirstOrDefault();
            Init();
            TitleBox.Text = book1.Title;
            PublisherBox.Text = book1.Publisher;
            YearBox.Text = book1.Year.ToString();
            PriceBox.Text = book1.Price.ToString();
            QuantitiBox.Text = book1.Quantity.ToString();
            AuthorNameBox.Text = author.Name;
            AuthorSurnameBox.Text = author.Surname;
            GenreNameBox.Text = genre.Name;
            AddAuthorsdataGrid.SelectedCellsChanged += AddAuthorsdataGrid_SelectedCellsChanged;
            AddBookdataGrid.SelectedCellsChanged += AddBookdataGrid_SelectedCellsChanged;
            UpdateAuthors();
            UpdateGenre();
            YearsNumeric();
            PriceNumeric();
            QuantityNumeric();
        }

        private void Init()
        {
            bookAuthor = bookstoreDBContext.BookAuthors.Where(ba => ba.BookId == book1.Id).FirstOrDefault();
            if (bookAuthor != null)
            {
                author = bookstoreDBContext.Authors.Where(a => a.Id == bookAuthor.AuthorId).FirstOrDefault();
                IdAuthor = author.Id;
            }
            else { this.Close();}
            bookGenre = bookstoreDBContext.BookGenres.Where(bg => bg.BookId == book1.Id).FirstOrDefault();
            if (bookGenre != null)
            {
                genre = bookstoreDBContext.Genres.Where(g => g.Id == bookGenre.GenreId).FirstOrDefault();
                IdGenre = genre.Id;
            }
            else { this.Close(); }
        }

        private void AddAuthorsdataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectAuthors();
        }

        private void AddBookdataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SelectNameGenres();
        }

        private void SelectAuthors()
        {
            if (AddAuthorsdataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)AddAuthorsdataGrid.SelectedItem;
                int id = selectedRow.Id;
                AuthorSurnameBox.Text = selectedRow.Surname;
                AuthorNameBox.Text = selectedRow.Name;
                IdAuthor = id;
            }
        }

        private void SelectNameGenres()
        {
            if (AddBookdataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)AddBookdataGrid.SelectedItem;
                int id = selectedRow.Id;
                GenreNameBox.Text = selectedRow.GenreName;
                IdGenre = id;
            }
        }

        private void YearsNumeric()
        {
            List<int> numbers = Enumerable.Range(1900, 124).ToList();
            YearBox.ItemsSource = numbers;
        }

        private void PriceNumeric()
        {
            List<double> numbers = Enumerable.Range(0, 400001).Select(i => i / 100.0).ToList();
            PriceBox.ItemsSource = numbers;
        }

        private void QuantityNumeric()
        {
            List<int> numbers = Enumerable.Range(0, 1000).ToList();
            QuantitiBox.ItemsSource = numbers;
        }

        private void closeWind(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minApp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void UpdateAuthors()
        {
            AddAuthorsdataGrid.ItemsSource = bookstoreDBContext.Authors.Select(a => new { Id = a.Id, Name = a.Name, Surname = a.Surname }).ToList();
        }

        private void UpdateGenre()
        {
            AddBookdataGrid.ItemsSource = bookstoreDBContext.Genres.Select(g => new { Id = g.Id, GenreName = g.Name }).ToList();
        }


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void Change_book(object sender, RoutedEventArgs e)
        {
            if (IdGenre != genre.Id || IdAuthor != author.Id || TitleBox.Text != book1.Title || PublisherBox.Text != book1.Publisher
                        || YearBox.Text != book1.Year.ToString() || PriceBox.Text != book1.Price.ToString() || QuantitiBox.Text != book1.Quantity.ToString())
            {
                book1.Title = TitleBox.Text;
                book1.Publisher = PublisherBox.Text;
                book1.Year = int.Parse(YearBox.Text);
                book1.Price = decimal.Parse(PriceBox.Text);
                book1.Quantity = int.Parse(QuantitiBox.Text);

                if (IdGenre != genre.Id)
                {
                    var IdGenres = bookstoreDBContext.Genres.Where(g => g.Name == GenreNameBox.Text && g.Id == IdGenre).FirstOrDefault();
                    bookstoreDBContext.Remove(bookGenre);
                    bookstoreDBContext.SaveChanges();
                    BookGenre bookg = new BookGenre()
                    {
                        GenreId = IdGenres.Id,
                        BookId = book1.Id,
                    };
                    bookstoreDBContext.BookGenres.Add(bookg);
                    bookstoreDBContext.SaveChanges();
                    genre = IdGenres;
                    bookGenre = bookstoreDBContext.BookGenres.Where(bg => bg.BookId == book1.Id).FirstOrDefault();

                }
                if (IdAuthor != author.Id)
                {
                    var IdAuthors = bookstoreDBContext.Authors.Where(a => a.Name == AuthorNameBox.Text && a.Surname == AuthorSurnameBox.Text && a.Id == IdAuthor).FirstOrDefault();
                    bookstoreDBContext.Remove(bookAuthor);
                    bookstoreDBContext.SaveChanges();
                    BookAuthor book = new BookAuthor()
                    {
                        AuthorId = IdAuthors.Id,
                        BookId = book1.Id,
                    };
                    bookstoreDBContext.BookAuthors.Add(book);
                    bookstoreDBContext.SaveChanges();
                    author = IdAuthors;
                    bookAuthor = bookstoreDBContext.BookAuthors.Where(bg => bg.BookId == book1.Id).FirstOrDefault();
                }
                bookstoreDBContext.Books.Update(book1);
                bookstoreDBContext.SaveChanges();
                MessageBox.Show("Changes saved!");

            }
        }
            
        private void closebtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void YearBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = e.Text;
            bool isNumber = false;
            int decimalValue;
            isNumber = int.TryParse(input, out decimalValue);

            if (!isNumber)
            {
                e.Handled = true;
                return;
            }

            int number = int.Parse(e.Text);
            if (number < 1900)
            {
                e.Handled = true;
                YearBox.Text = "1900";
            }
            if (number > (int)2023)
            {
                e.Handled = true;
                YearBox.Text = "2023";
            }
        }


        private void PriceBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = e.Text;

            bool isNumber = false;

            decimal decimalValue;
            isNumber = decimal.TryParse(input, out decimalValue);



            if (!isNumber)
            {
                e.Handled = true;
                return;
            }

            decimal number = decimal.Parse(e.Text);
            if (number < 0)
            {
                e.Handled = true;
            }
            if (number > (decimal)4000)
            {
                e.Handled = true;
            }
        }

        private void QuantitiBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = e.Text;

            bool isNumber = false;

            int decimalValue;
            isNumber = int.TryParse(input, out decimalValue);



            if (!isNumber)
            {
                e.Handled = true;
                return;
            }

            int number = int.Parse(e.Text);
            if (number < 0)
            {
                e.Handled = true;
            }
            if (number > (int)999)
            {
                e.Handled = true;
            }
        }
    }
}
