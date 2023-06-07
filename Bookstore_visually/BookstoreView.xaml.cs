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
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Diagnostics;
using System.Net;
using System.Collections;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

using MessageBox = System.Windows.MessageBox;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MaterialDesignThemes.Wpf;
using System.IO;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for Bookstore.xaml
    /// </summary>
    public partial class BookstoreView : Window
    {
        IRepository<Book> repository = new Repository<Book>(new BookstoreDBContext());
       
        ViewModel model;
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper;

        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();
        private object Credential_user;
        private int idClient;

        public BookstoreView(object credential)
        {
            InitializeComponent();
            model = new ViewModel();
            paletteHelper = new PaletteHelper();
            this.DataContext = model;
            Credential_user = credential;
            idClient = bookstoreDBContext.Clients.Where(c => c.CredentialsId == ((Client)Credential_user).CredentialsId).Select(c => c.CredentialsId).FirstOrDefault();
            RefreshOrderDG();
            RefreshBook();
            RefreshOrderBooks();
            RefreshReserverBook();
            QuantityNumeric();
            RefreshNewBooks();
            RefreshMostgenresBooks();
            RefreshMostAuthorBooks();
            RefreshMostSoldBooks();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RefreshBook();
        }

        private void WriteCommentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Book.SelectedItem != null)
            {
                var selectedRow = (dynamic)Book.SelectedItems;
                int id = selectedRow[0].Id;
                this.IsEnabled = false;
                CommentWindow comment = new CommentWindow(id, idClient);
                comment.Closed += Comment_Closed;
                comment.Show();
            }
        }

        private void Comment_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
        }

        private void ReserveBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Book.SelectedItem != null)
            {
                var selectedRow = (dynamic)Book.SelectedItems;
                int id = selectedRow[0].Id;

                if (selectedRow[0].Quantity - 1 >= 0)
                {
                    var book = bookstoreDBContext.Books.Where(b => b.Id == id).FirstOrDefault();
                    book.Quantity = book.Quantity - 1;
                    bookstoreDBContext.Books.Update(book);
                    bookstoreDBContext.SaveChanges();
                    Reservation reservation = new Reservation();
                    reservation.ClientId = idClient;
                    reservation.BookId = id;
                    reservation.ReservationDate = DateTime.Today;

                    bookstoreDBContext.Reservations.Add(reservation);
                    bookstoreDBContext.SaveChanges();
                    RefreshBook();
                    RefreshOrderBooks();
                    RefreshReserverBook();
                    MessageBox.Show("The book is reserved! All detailed information has been sent to your mailbox.");
                }
                else
                {
                    MessageBox.Show("There are no books available!");
                }
            }
        }

        private void RefreshBook()
        {
            List<object> ff = new List<object>();
            var book = bookstoreDBContext.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.BookGenres).ThenInclude(ba=> ba.Genre).Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.BookGenres.FirstOrDefault().Genre.Name, AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname }).ToList();
            ff.AddRange(book);
            model.AddCDGBook(ff);
        }

        //book
        //search
        private void AuthorSearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> ff = new List<object>();
            string[] words = AuthorSearchTextBox.Text.Split(new[] { ' ' }, 2);
            {
                var book = bookstoreDBContext.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.BookGenres).ThenInclude(ba => ba.Genre)
                    .Where(b => b.BookAuthors.Any(ba => ba.Author.Name.Contains(words.Count() > 0 ? words[0] : AuthorSearchTextBox.Text)) || b.BookAuthors.Any(ba => ba.Author.Surname.Contains(words.Count() > 1 ? words[1] : AuthorSearchTextBox.Text)))
                    .Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.BookGenres.FirstOrDefault().Genre.Name, AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname })
                    .ToList();

                ff.AddRange(book);
                model.AddCDGSBookAuthor(ff);
            }
        }

        private void TitleSearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> ff = new List<object>();
            var book = bookstoreDBContext.Books.Include(b => b.BookGenres).ThenInclude(ba => ba.Genre).
               Where(b => b.Title.Contains(TitleSearchTextBox.Text)).
               Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.BookGenres.FirstOrDefault().Genre.Name }).ToList();
            ff.AddRange(book);
            model.AddCDGSBookTitle(ff);
        }

        private void GenreSearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> ff = new List<object>();
            var book = bookstoreDBContext.Books.Include(b => b.BookGenres).ThenInclude(ba => ba.Genre).
               Where(g => g.BookGenres.FirstOrDefault().Genre.Name.Contains(GenreSearchTextBox.Text)).
               Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.BookGenres.FirstOrDefault().Genre.Name }).ToList();
            ff.AddRange(book);
            model.AddCDGSBookGenre(ff);
        }

        //search
        //novelity
        private void NewBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshNewBooks();
        }

        private void RefreshNewBooks()
        {
            List<object> ff = new List<object>();
            var book = bookstoreDBContext.Books.
                Include(b => b.BookGenres).
                    ThenInclude(ba => ba.Genre).
                        OrderByDescending(b => b.Id).
                            Select(b => new 
                            { 
                                Id = b.Id, 
                                Title = b.Title, 
                                Publisher = b.Publisher, 
                                Year = b.Year, 
                                Price = b.Price, 
                                Quantity = b.Quantity, 
                                Genre = b.BookGenres.FirstOrDefault().Genre.Name 
                            }).Take(5).ToList();
            ff.AddRange(book);
            model.AddCDGNewBook(ff);
        }
        //novelity 
        //MOst sold
        private void MostSoldBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshMostSoldBooks();
        }

        private void RefreshMostSoldBooks()
        {
            List<object> ff = new List<object>();
            var book = bookstoreDBContext.Books
                .Include(b => b.OrderBooks).
                    ThenInclude(ob => ob.Order).
                        Where(ba => ba.OrderBooks.Any(ob => ob.Order.Payment_status == true)).AsEnumerable().
                            GroupBy(b => new { b.Id, b.Title, b.Publisher })
                            .Select(g => new 
                            {
                                Id = g.Key.Id,
                                Title = g.Key.Title,
                                Publisher = g.Key.Publisher,
                                TotalSold = g.Sum(ob => ob.OrderBooks.Sum(o => o.Order.Quantity)),
                            }).OrderByDescending(g => g.TotalSold).Take(5).ToList();

            ff.AddRange(book);
            model.AddCDGMostSold(ff);
        }
        //MOst sold
        //MOst popular Authors
        private void MostAuthorBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshMostAuthorBooks();
        }

        private void RefreshMostAuthorBooks()
        {
            List<object> ff = new List<object>();

            var BookAuthor = bookstoreDBContext.BookAuthors.
                Include(ba => ba.Book).
                    ThenInclude(b => b.OrderBooks).
                        ThenInclude(ob => ob.Order).
                            Include(ba => ba.Author).
                                Where(ba => ba.Book.OrderBooks.Any(ob => ob.Order.Payment_status == true)).AsEnumerable()
                                    .GroupBy(ba => ba.Author).Select(g => new 
                                    { 
                                        Name = g.Key.Name, 
                                        Surname = g.Key.Surname, 
                                        TotalSold = g.SelectMany(ba => ba.Book.OrderBooks).Sum(ob => ob.Order.Quantity) 
                                    }).Where(a => a.TotalSold > 0).OrderByDescending(a => a.TotalSold).Take(5);
            ff.AddRange(BookAuthor);
            model.AddCDGMPAuthor(ff);
        }
        //MOst popular Authors
        //MOst popular Genre
        private void MostgenresBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshMostgenresBooks();
        }

        private void RefreshMostgenresBooks()
        {
            List<object> ff = new List<object>();

            var genre = bookstoreDBContext.Genres
                .Include(g => g.BookGenres).
                ThenInclude(bg => bg.Book)
                    .ThenInclude(b => b.OrderBooks)
                        .ThenInclude(ob => ob.Order)
                            .AsEnumerable().Select(g => new 
                            {
                                Genre = g.Name,
                                TotalSold = g.BookGenres.
                                    SelectMany(bg => bg.Book.OrderBooks).
                                        Where(ob => ob.Order.Payment_status == true)
                                            .Sum(ob => ob.Order.Quantity)
                            }).Where(g => g.TotalSold > 0).OrderByDescending(g => g.TotalSold).Take(5);
            ff.AddRange(genre);
            model.AddCDGMPGenre(ff);
        }

        //MOst popular Genre
        //order
        private void Order_ButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshOrderDG();
        }

        private void RefreshOrderDG()
        {
            OrderDataGrid.ItemsSource = null;
          
            OrderDataGrid.ItemsSource = bookstoreDBContext.Orders.Where(o => o.ClientId == idClient).Include(o => o.OrderBooks).ThenInclude(ob => ob.Book).Include(o => o.Clients)
            .Select(o => new { Id = o.Id, Date = o.Date, Price = o.Price, Quantity = o.Quantity, Payment_status = o.Payment_status, BookTitle = o.OrderBooks.FirstOrDefault().Book.Title, ClientName = o.Clients.Name }).ToList();
        }



        private void RefreshOrderBooks()
        {
            OrderBooksDataGrid.ItemsSource = bookstoreDBContext.Books.Include(b => b.BookAuthors).
                ThenInclude(ba => ba.Author).Include(b => b.BookGenres).ThenInclude(ba => ba.Genre).
                Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher,
                    Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.BookGenres.FirstOrDefault().Genre.Name,
                    AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, 
                    AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname }).ToList();
        }

        private void OrderBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            int idClient = bookstoreDBContext.Clients.Where(c => c.CredentialsId == ((Client)Credential_user).CredentialsId).Select(c => c.CredentialsId).FirstOrDefault(); // Id користувача який зайшов в програму 
          
            int Quantity_B;
          
            if (OrderBooksDataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)OrderBooksDataGrid.SelectedItem;
                int id = selectedRow.Id;
                var dbBook = bookstoreDBContext.Books.FirstOrDefault(b => b.Id == id);
                Book book = bookstoreDBContext.Books.FirstOrDefault(b => b.Id == id);
                if (Quantity_Book.Text.Length > 0)
                {
                    try
                    {
                        Quantity_B = int.Parse(Quantity_Book.Text);

                        if (book != null && book.Quantity - Quantity_B >= 0)
                        {
                            Order neworder =
                             new Order()
                             {
                                 Date = DateTime.Now,
                                 ClientId = idClient,
                                 Price = book.Price * Quantity_B, 
                                 Quantity = Quantity_B,
                                 Clients = bookstoreDBContext.Clients.Where(i => i.CredentialsId == idClient).FirstOrDefault(),
                                 Payment_status=false,
                                 OrderBooks = (ICollection<OrderBook>)new List<OrderBook>()
                             };
                             bookstoreDBContext.Orders.Add(neworder);
                            bookstoreDBContext.SaveChanges();

                            if (dbBook != null)
                            {
                                dbBook.Quantity = book.Quantity - Quantity_B;
                                bookstoreDBContext.SaveChanges();
                            }


                            int latestOrder = bookstoreDBContext.Orders.OrderByDescending(o => o.Id).Select(o=> o.Id).FirstOrDefault();
                            OrderBook neworderbook =
                            new OrderBook()
                            {
                                OrderId = latestOrder,
                                BookId = book.Id, 
                                Order = bookstoreDBContext.Orders.Where(i => i.Id == latestOrder).FirstOrDefault(),
                                Book = dbBook
                            };
                            bookstoreDBContext.OrderBooks.Add(neworderbook);
                            bookstoreDBContext.SaveChanges();

                            MessageBox.Show("Order added");
                        }
                        else
                        {
                            MessageBox.Show("There are no book available");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Enter the number of books");
                }
            }
            else
            {
                MessageBox.Show("Select a book !");
            }
           RefreshOrderBooks();
           RefreshOrderDG();
           RefreshBook();
        } 

        private void Payment_ButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedRow = (dynamic)OrderDataGrid.SelectedItem;
            int id = selectedRow.Id;
            if (selectedRow.Payment_status != true)
            {
                Order order = bookstoreDBContext.Orders.FirstOrDefault(b => b.Id == id);

                var dbEntry = bookstoreDBContext.Orders.FirstOrDefault(x => x.Id == order.Id);
                var dbClient = bookstoreDBContext.Clients.FirstOrDefault(c => c.CredentialsId == dbEntry.ClientId);

                if (dbEntry != null)
                {
                    var Order = new
                    {
                        version = 3,
                        public_key = Config.LiqyPublicKey,
                        action = "pay",
                        amount = dbEntry.Price,
                        currency = "UAH",
                        description = "Pay book whit order " + dbEntry.Id + $",Client phone: ({dbClient.PhoneNumber})",
                        order_id = dbEntry.Id,
                        language = "uk",
                        paytypes = "",
                        phone = dbClient.PhoneNumber

                    };
                    string data = System.Text.Json.JsonSerializer.Serialize(Order);
                    string data64 = LiqyPayHelper.CreateData(data); 
                    string privateKey = Bookstore.Config.LiqypayPrivateKey;

                    string signature_sourse = privateKey + data64 + privateKey;
                    var sha1 = SHA1.Create();
                    string signature = LiqyPayHelper.CreateSign(data64, privateKey);
                    Payment payment = new Payment();
                    this.IsEnabled = false;
                    payment.Closed += Payment_Closed;
                    payment.Show();
                    payment.StartWebResourceRequest(Config.LiqyPatCheckoutURl, data64, signature);
                }
                else { MessageBox.Show("Not selected order"); }
            }
            else
            {
                MessageBox.Show("The book has already been paid for");
            }
        }

        private void Payment_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshOrderDG();
            RefreshMostgenresBooks();
            RefreshMostAuthorBooks();
            RefreshMostSoldBooks();
        }

        private void Update_ButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshReserverBook();
        }

        private void RefreshReserverBook()
        {
            ReservDataGrid.ItemsSource = bookstoreDBContext.Reservations.
                Include(r => r.Client).
                    Include(r => r.Book).
                        Where(r => r.ClientId == idClient).Select(r => new 
                        {
                            Id=r.Id, 
                            ClientName = r.Client.Name, 
                            BookTitle = r.Book.Title,
                            ReservationDate = r.ReservationDate.ToShortDateString(), 
                            CheckOutDate = r.CheckoutDate.ToShortDateString(),
                            ReturnDate = r.ReturnDate.ToShortDateString(),
                            IsReturned = r.IsReturned
                        }).ToList();
        }

        private void CloseWind(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
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

        private void minApp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void QuantityBook_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
                Quantity_Book.Text = "0";
            }
            if (number > (int)1000)
            {
                e.Handled = true;
                Quantity_Book.Text = "1000";
            }
        }

        private void QuantityNumeric()
        {
            List<int> numbers = Enumerable.Range(0, 1000).ToList();
            Quantity_Book.ItemsSource = numbers;

        }

        private void DeleteOrder_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)OrderDataGrid.SelectedItem;
                int id = selectedRow.Id;
                var order = bookstoreDBContext.Orders.Where(o => o.Id == id).FirstOrDefault();
                if (order.Payment_status != true)
                {
                    bookstoreDBContext.Remove(order);
                    var orderb = bookstoreDBContext.OrderBooks.FirstOrDefault(o => o.OrderId == order.Id);
                    var book = bookstoreDBContext.Books.Where(b => b.Id == orderb.BookId).FirstOrDefault();
                    bookstoreDBContext.OrderBooks.Remove(orderb);
                    bookstoreDBContext.SaveChanges();

                    book.Quantity += (int)order.Quantity;
                    bookstoreDBContext.Update(book);
                    bookstoreDBContext.SaveChanges();

                    RefreshOrderDG();
                    RefreshBook();
                    RefreshOrderBooks();
                }
                else
                {
                    MessageBox.Show("This order has already been paid for!");
                }
            }
           
        }

        private void DeleteReservbook_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (ReservDataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)ReservDataGrid.SelectedItem;
                int id = selectedRow.Id;
                var reservation = bookstoreDBContext.Reservations.Where(o => o.Id == id).FirstOrDefault();
                if (reservation.IsReturned != true && reservation.CheckoutDate.Year <= 1)
                {
                    bookstoreDBContext.Remove(reservation);
                    bookstoreDBContext.SaveChanges();
                    var book = bookstoreDBContext.Books.Where(b => b.Id == reservation.BookId).FirstOrDefault();
                    book.Quantity++;
                    bookstoreDBContext.Update(book);
                    bookstoreDBContext.SaveChanges();
                    RefreshReserverBook();
                    RefreshBook();
                    RefreshOrderBooks();
                }
                else
                {
                    MessageBox.Show("You have taken the book, so it is not possible to remove the reservation!");
                }
            }
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
