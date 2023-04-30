using Bookstore;
using Bookstore.Entities;
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
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using TextBox = System.Windows.Controls.TextBox;

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
        }

        private void UpdateAuthors()
        {
            AddAuthorsdataGrid.ItemsSource = bookstoreDBContext.Authors.Select(a => new { Id = a.Id, Name = a.Name, Surname = a.Surname }).ToList();
        }

        private void UpdateGenre()
        {
            AddBookdataGrid.ItemsSource = bookstoreDBContext.Genres.Select(g => new { Id = g.Id, Name = g.Name }).ToList();
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
                GenreNameBox.Text = selectedRow.Name;
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

        private void Button_Click(object sender, RoutedEventArgs e)
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
                                                var IdAuthor = bookstoreDBContext.Authors.Where(a => a.Name == AuthorNameBox.Text && a.Surname == AuthorSurnameBox.Text).FirstOrDefault();
                                                var idGenre = bookstoreDBContext.Genres.Where(g => g.Name == GenreNameBox.Text).FirstOrDefault();

                                                Book newBook =
                                                    new Book()
                                                    {
                                                        Title = TitleBox.Text,
                                                        Publisher = PublisherBox.Text,
                                                        Year = year,
                                                        Price = price,
                                                        Quantity = quantiti,
                                                        GenreId = idGenre.Id,
                                                        BookAuthors = (ICollection<BookAuthor>)new List<BookAuthor>(),
                                                        Genre = bookstoreDBContext.Genres.Where(i => i.Id == idGenre.Id).FirstOrDefault(),
                                                        OrderBooks = (ICollection<OrderBook>)new List<OrderBook>()
                                                    };
                                                bookstoreDBContext.Books.Add(newBook);

                                                bookstoreDBContext.SaveChanges();


                                                Photo photo = new Photo()
                                                {
                                                    Name = Path.GetFileName("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"),
                                                    ImageData = File.ReadAllBytes("D:\\ШАГ\\ADO.NET\\Cursova_ADO.NET\\Bookstore\\Image\\genericBookCover.jpg"),
                                                    BookId = bookstoreDBContext.Books.OrderByDescending(o => o.Id).Select(o => o.Id).FirstOrDefault()
                                                };
                                                bookstoreDBContext.Photos.Add(photo);
                                                bookstoreDBContext.SaveChanges();

                                                BookAuthor book = new BookAuthor()
                                                { 
                                                    AuthorId = IdAuthor.Id,
                                                    BookId= bookstoreDBContext.Books.OrderByDescending(o => o.Id).Select(o => o.Id).FirstOrDefault()
                                                };

                                                bookstoreDBContext.BookAuthors.Add(book);
                                                bookstoreDBContext.SaveChanges();

                                                MessageBox.Show("Added successfully!");
                                                ClearAll();
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
    }
}
