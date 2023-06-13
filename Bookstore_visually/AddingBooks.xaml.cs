using Bookstore;
using Bookstore.Entities;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using TextBox = System.Windows.Controls.TextBox;
using System.Resources;
using Octopus.Client.Model;
using Octopus.Client.Extensibility;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for AddingBooks.xaml
    /// </summary>
    public partial class AddingBooks : Window
    {
        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();
        ViewModel model;
        public AddingBooks()
        {
            InitializeComponent();
            model = new ViewModel();
            UpdateAuthors();
            UpdateGenre();
            AddAuthorsdataGrid.SelectedCellsChanged += AddAuthorsdataGrid_SelectedCellsChanged;
            AddBookdataGrid.SelectedCellsChanged += AddBookdataGrid_SelectedCellsChanged;
            YearsNumeric();
            PriceNumeric();
            QuantityNumeric();
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

        private void UpdateAuthors()
        {
            AddAuthorsdataGrid.ItemsSource = bookstoreDBContext.Authors.Select(a => new { Id = a.Id, Name = a.Name, Surname = a.Surname }).ToList();
        }

        private void UpdateGenre()
        {
            AddBookdataGrid.ItemsSource = bookstoreDBContext.Genres.Select(g => new { Id = g.Id, GenreName = g.Name }).ToList();
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
            }
        }

        private void SelectNameGenres()
        {
            if (AddBookdataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)AddBookdataGrid.SelectedItem;
                int id = selectedRow.Id;
                GenreNameBox.Text = selectedRow.GenreName;
            }
        }

        private void AddGenre(object sender, RoutedEventArgs e)
        {
            AddingGenre addingGenre = new AddingGenre();
            addingGenre.Closed += AddingGenre_Closed;
            this.IsEnabled = false;
            addingGenre.Show();
        }

        private void AddingGenre_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            UpdateGenre();
        }

        private void AddAuthors(object sender, RoutedEventArgs e)
        {
            AddingAuthors addingAuthors = new AddingAuthors();
            addingAuthors.Closed += AddingAuthors_Closed;
            this.IsEnabled = false;
            addingAuthors.Show();
        }

        private void AddingAuthors_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            UpdateAuthors();
        }

        private void Messagbox()
        {
            MessageBox.Show("Fill in the data!");
        }

        private void ClearAll()
        {
            var textBoxes = Enumerable.OfType<TextBox>(LogicalTreeHelper.GetChildren(this));
            foreach (var textBox in textBoxes)
            {
                textBox.Clear();
            }
        }



        private IEnumerable<object> OfType<T>()
        {
            throw new NotImplementedException();
        }

        private void AddBook_Button_Click(object sender, RoutedEventArgs e)
        {
            AddBooks();
        }

        private void AddBooks()
        {
            int year;
            decimal price;
            int quantiti;
            if (TitleBox.Text.Length > 0)
            {
                if (PublisherBox.Text.Length > 0)
                {
                    if (YearBox.Text.Length > 0)
                    {
                        if (PriceBox.Text.Length > 0)
                        {
                            if (QuantitiBox.Text.Length > 0)
                            {
                                if (AuthorNameBox.Text.Length > 0)
                                {
                                    if (AuthorSurnameBox.Text.Length > 0)
                                    {
                                        if (GenreNameBox.Text.Length > 0)
                                        {
                                            try
                                            {
                                                year = int.Parse(YearBox.Text);
                                                price = decimal.Parse(PriceBox.Text);
                                                quantiti = int.Parse(QuantitiBox.Text);
                                                var IdAuthor = bookstoreDBContext.Authors.
                                                    Where(a => a.Name == AuthorNameBox.Text && a.Surname == AuthorSurnameBox.Text).FirstOrDefault();
                                                var idGenre = bookstoreDBContext.Genres.Where(g => g.Name == GenreNameBox.Text).FirstOrDefault();
                                                var IsChekedBook = bookstoreDBContext.Books.
                                                    Where(b => b.Title == TitleBox.Text && b.Publisher == PublisherBox.Text && b.Year == year &&
                                                    b.Price == price && b.Quantity == quantiti && b.BookGenres.FirstOrDefault().GenreId == idGenre.Id 
                                                    && b.BookAuthors.FirstOrDefault().AuthorId == IdAuthor.Id).FirstOrDefault();
                                                if (IsChekedBook == null)
                                                {
                                                    Book newBook =
                                                        new Book()
                                                        {
                                                            Title = TitleBox.Text,
                                                            Publisher = PublisherBox.Text,
                                                            Year = year,
                                                            Price = price,
                                                            Quantity = quantiti,
                                                            BookAuthors = (ICollection<BookAuthor>)new List<BookAuthor>(),
                                                            OrderBooks = (ICollection<OrderBook>)new List<OrderBook>()
                                                        };
                                                    bookstoreDBContext.Books.Add(newBook);
                                                    bookstoreDBContext.SaveChanges();

                                                    BookGenre genreBook = new BookGenre()
                                                    {
                                                        GenreId = idGenre.Id,
                                                        BookId = bookstoreDBContext.Books.OrderByDescending(o => o.Id).Select(o => o.Id).FirstOrDefault()
                                                    };

                                                    bookstoreDBContext.BookGenres.Add(genreBook);
                                                    bookstoreDBContext.SaveChanges();

                                                    Photo photo = new Photo()
                                                    {
                                                        Name = Path.GetFileName("Image\\genericBookCover.jpg"),
                                                        ImageData = File.ReadAllBytes("Image\\genericBookCover.jpg"),
                                                        BookId = bookstoreDBContext.Books.OrderByDescending(o => o.Id).Select(o => o.Id).FirstOrDefault()
                                                    };
                                                    bookstoreDBContext.Photos.Add(photo);
                                                    bookstoreDBContext.SaveChanges();

                                                    BookAuthor book = new BookAuthor()
                                                    {
                                                        AuthorId = IdAuthor.Id,
                                                        BookId = bookstoreDBContext.Books.OrderByDescending(o => o.Id).Select(o => o.Id).FirstOrDefault()
                                                    };

                                                    bookstoreDBContext.BookAuthors.Add(book);
                                                    bookstoreDBContext.SaveChanges();

                                                    MessageBox.Show("Added successfully!");
                                                    ClearAll();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Such a book already exists!");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            Messagbox();
                                        }
                                    }
                                    else
                                    {
                                        Messagbox();
                                    }
                                }
                                else
                                {
                                    Messagbox();
                                }

                            }
                            else
                            {
                                Messagbox();
                            }
                        }
                        else
                        {
                            Messagbox();
                        }
                    }
                    else
                    {
                        Messagbox();
                    }
                }
                else
                {
                    Messagbox();
                }
            }
            else
            {
                Messagbox();
            }
        }

        private void exitApp(object sender, RoutedEventArgs e)
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

            if (!isNumber && input != ",")
            {
                e.Handled = true;
                return;
            }

            bool hasDecimalSeparator = PriceBox.Text.Contains(",");

            if (input == "," && hasDecimalSeparator)
            {
                e.Handled = true;
                return;
            }

            if (input != ",")
            {
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
