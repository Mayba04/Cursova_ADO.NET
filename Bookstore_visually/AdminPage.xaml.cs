using Bookstore.Entities;
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
using Bookstore.Repository;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {

        IRepository<Book> repository = new Repository<Book>(new BookstoreDBContext());
        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();

        public AdminPage()
        {
            InitializeComponent();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BookAuthor book = new BookAuthor();
            Book newBook =
                new Book()
                {
                    Title = "default",
                    Publisher = "default",
                    Year = 0,
                    Price = 0,
                    Quantity = 0,
                    GenreId = 1,
                    BookAuthors = (ICollection<BookAuthor>)new List<BookAuthor>(),
                    Genre = bookstoreDBContext.Genres.Where(i => i.Id == 1).FirstOrDefault(),
                    OrderBooks = (ICollection<OrderBook>)new List<OrderBook>()
                };
            bookstoreDBContext.Books.Add(newBook);

            bookstoreDBContext.SaveChanges();
            RefreshDataGrid();
            //repository.Insert(newBook);
            // repository.Save(); // зберігаємо зміни в базу даних
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems != null)
            {
                foreach (var book in dataGrid.SelectedItems)
                {
                    // bookstoreDBContext.Books.Remove(book as Book);
                    //repository.Delete(((Book)book).Id);
                    bookstoreDBContext.Books.Remove(bookstoreDBContext.Books.Where(b => b.Id == ((Book)book).Id).FirstOrDefault());
                }
                bookstoreDBContext.SaveChanges();
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            dataGrid.ItemsSource = repository.GetAll().ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedBook = (Book)dataGrid.SelectedItem;
                UpdateBook(selectedBook);
            }
        }

        private void UpdateBook(Book book)
        {
            using (var context = new BookstoreDBContext())
            {

                var dbBook = context.Books.FirstOrDefault(b => b.Id == book.Id);

                if (dbBook != null)
                {
                    dbBook.Title = book.Title;
                    dbBook.Publisher = book.Publisher;
                    dbBook.Year = book.Year;
                    dbBook.Price = book.Price;
                    dbBook.GenreId = book.GenreId;
                    dbBook.Quantity = book.Quantity;

                    context.SaveChanges();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //dataGrid.ItemsSource = bookstoreDBContext.Books.ToList();
            // dataGrid.ItemsSource = repository.GetAll().ToList();
            RefreshDataGrid();
        }

    }
}
