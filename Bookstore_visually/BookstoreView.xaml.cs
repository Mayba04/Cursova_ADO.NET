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

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for Bookstore.xaml
    /// </summary>
    public partial class BookstoreView : Window
    {
        IRepository<Book> repository = new Repository<Book>(new BookstoreDBContext());
        IRepository<Order> repositoryO = new Repository<Order>(new BookstoreDBContext());
        ViewModel model;

        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();
        private object Credential_user;
        private int idClient;
        public BookstoreView(object credential)
        {
           
            InitializeComponent();
            model = new ViewModel();
            this.DataContext = model;
            Credential_user = credential;
            //OrderBooksDataGrid.ItemsSource = repository.GetAll().ToList();
           
            idClient = bookstoreDBContext.Clients.Where(c => c.CredentialsId == ((Credentials)Credential_user).Id).Select(c => c.CredentialsId).FirstOrDefault();
            RefreshBook();
            RefreshOrderBooks();
            RefreshReserverBook();
            RefreshOrderDG();

            //

        }
        //book


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //dataGrid.ItemsSource = bookstoreDBContext.Books.ToList();
            // dataGrid.ItemsSource = repository.GetAll().ToList();
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
                //int? id = ((Order)OrderDataGrid.SelectedItem).Id;
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
                    RefreshReserverBook();
                    //надсилання даних на електрону адресу
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
            var book = bookstoreDBContext.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.Genre).Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.Genre.Name, AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname }).ToList();
            ff.AddRange(book);
            model.AddCDGBook(ff);
        }

        //book
        //search
        private void AuthorSearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> ff = new List<object>();

            /*BooksSADataGrid.ItemsSource*/ 
            var book = bookstoreDBContext.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(g=>g.Genre).
                Where(b => b.BookAuthors.Any(ba => ba.Author.Name.Contains(AuthorSearchTextBox.Text))).
                Select(b=> new {Id = b.Id, Title=b.Title, Publisher=b.Publisher, Year=b.Year, Price=b.Price, Quantity=b.Quantity, Genre =b.Genre.Name, AuthorName = b.BookAuthors.FirstOrDefault().Author.Name,AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname}).ToList();
            ff.AddRange(book);

            model.AddCDGSBookAuthor(ff);
        
        }

        private void TitleSearchButton_Click(object sender, RoutedEventArgs e)
        {
            /*BooksSTDataGrid.ItemsSource*/
            List<object> ff = new List<object>();
            var book = bookstoreDBContext.Books.Include(g => g.Genre).
               Where(b => b.Title.Contains(TitleSearchTextBox.Text)).
               Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.Genre.Name }).ToList();
            ff.AddRange(book);
            model.AddCDGSBookTitle(ff);
        }

        private void GenreSearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<object> ff = new List<object>();
            /*BooksSGDataGrid.ItemsSource*/ 
            var book = bookstoreDBContext.Books.Include(g => g.Genre).
               Where(g => g.Genre.Name.Contains(GenreSearchTextBox.Text)).
               Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.Genre.Name }).ToList();
            ff.AddRange(book);
            model.AddCDGSBookGenre(ff);
        }

        //search
        //novelity 

        private void NewBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            List<object> ff = new List<object>();
            /*NewBooksDataGrid.ItemsSource*/ 
            var book = bookstoreDBContext.Books.Include(b => b.Genre).OrderByDescending(b => b.Id).Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.Genre.Name }).Take(5).ToList();

            ff.AddRange(book);

            model.AddCDGNewBook(ff);

        }
        //novelity 
        //MOst sold
        private void MostSoldBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            List<object> ff = new List<object>();
            /*MostSlodBooksDataGrid.ItemsSource */
            var book = bookstoreDBContext.Books
                .Include(b => b.OrderBooks).ThenInclude(ob => ob.Order).AsEnumerable().GroupBy(b => new { b.Id, b.Title })
                .Select(g => new {
                    Id = g.Key.Id,
                    Title = g.Key.Title,
                    TotalSold = g.Sum(ob => ob.OrderBooks.Sum(o => o.Order.Quantity))
                }).OrderByDescending(g => g.TotalSold).Take(5).ToList();

            ff.AddRange(book);

            model.AddCDGMostSold(ff);
        }
        //MOst sold
        //MOst popular Authors
        private void MostAuthorBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            //MostAuthorBooksDataGrid.ItemsSource = bookstoreDBContext.BookAuthors .Include(ba => ba.Book).ThenInclude(b => b.OrderBooks).ThenInclude(ob => ob.Order).Include(ba => ba.Author).AsEnumerable()
            //.GroupBy(ba => ba.Author).Select(g => new { Name = g.Key.Name, Surname = g.Key.Surname, TotalSold = g.SelectMany(ba => ba.Book.OrderBooks).Sum(ob => ob.Order.Quantity)}).OrderByDescending(a => a.TotalSold).Take(1);
            List<object> ff = new List<object>();
            /*MostAuthorBooksDataGrid.ItemsSource*/ 
            var BookAuthor = bookstoreDBContext.BookAuthors.Include(ba => ba.Book).ThenInclude(b => b.OrderBooks).ThenInclude(ob => ob.Order).Include(ba => ba.Author).Where(ba => ba.Book.OrderBooks.Any(ob => ob.Order.Payment_status == true)).AsEnumerable()
            .GroupBy(ba => ba.Author).Select(g => new { Name = g.Key.Name, Surname = g.Key.Surname, TotalSold = g.SelectMany(ba => ba.Book.OrderBooks).Sum(ob => ob.Order.Quantity) }).OrderByDescending(a => a.TotalSold).Take(1);
            ff.AddRange(BookAuthor);
            model.AddCDGMPAuthor(ff);
        }
        //MOst popular Authors
        //MOst popular Genre
        private void MostgenresBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            //MostgenresBooksDataGrid.ItemsSource = bookstoreDBContext.Genres.Include(g => g.Books).ThenInclude(b => b.OrderBooks).ThenInclude(ob => ob.Order).AsEnumerable().
            //Select(g => new {
            //    Genre = g.Name,
            //    TotalSold = g.Books.SelectMany(b => b.OrderBooks)
            //.Sum(ob => ob.Order.Quantity)}).OrderByDescending(g => g.TotalSold).Take(1);
            List<object> ff = new List<object>();

            /*MostgenresBooksDataGrid.ItemsSource*/ 
            var genre = bookstoreDBContext.Genres
                .Include(g => g.Books)
                    .ThenInclude(b => b.OrderBooks)
                        .ThenInclude(ob => ob.Order)
                .AsEnumerable()
                .Select(g => new {
                    Genre = g.Name,
                    TotalSold = g.Books.SelectMany(b => b.OrderBooks)
                        .Where(ob => ob.Order.Payment_status == true) // додати умову на статус
                        .Sum(ob => ob.Order.Quantity)
                })
                .OrderByDescending(g => g.TotalSold)
                .Take(1);
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
            //OrderDataGrid.ItemsSource = bookstoreDBContext.Orders.Where(o => o.ClientId == idClient).ToList();

            OrderDataGrid.ItemsSource = bookstoreDBContext.Orders.Where(o => o.ClientId == idClient).Include(o => o.OrderBooks).ThenInclude(ob => ob.Book).Include(o => o.Clients)
            .Select(o => new { Id = o.Id, Date = o.Date, Price = o.Price, Quantity = o.Quantity, Payment_status = o.Payment_status, BookTitle = o.OrderBooks.FirstOrDefault().Book.Title, ClientName = o.Clients.Name }).ToList();

            //OrderDataGrid.ItemsSource = repositoryO.GetAll().Where(o => o.ClientId == idClient).ToList();
        }



        private void RefreshOrderBooks()
        {
            OrderBooksDataGrid.ItemsSource = bookstoreDBContext.Books.Include(b => b.BookAuthors).
                ThenInclude(ba => ba.Author).Include(b => b.Genre).
                Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher,
                    Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.Genre.Name,
                    AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, 
                    AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname }).ToList();
        }

        private void OrderBooks_ButtonClick(object sender, RoutedEventArgs e)
        {
            int idClient = bookstoreDBContext.Clients.Where(c => c.CredentialsId == ((Credentials)Credential_user).Id).Select(c => c.CredentialsId).FirstOrDefault(); // Id користувача який зайшов в програму 
            //var book = OrderBooksDataGrid.SelectedItem;

            //перевірка чи вибрно об'єкт 
            //перевірка чи достатньо в нас книжок 
            // створення об'єкта Order
            // добавлення замовлення
            // видаленя екземпоярів book
            // добавлення orderbook
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

                        //if (bookstoreDBContext.Books.Where(b => b.Id == ((Book)OrderBooksDataGrid.SelectedItem).Id).Select(b=>b.Quantity).FirstOrDefault() - Quantity_B >= 0)
                        if (book != null && book.Quantity - Quantity_B >= 0)
                        {
                            Order neworder =
                             new Order()
                             {
                                 Date = DateTime.Now,
                                 ClientId = idClient,
                                 Price = book.Price * Quantity_B, /*(bookstoreDBContext.Books.Where(b => b.Id == ((Book)OrderBooksDataGrid.SelectedItem).Id).Select(b => b.Price).FirstOrDefault()) * Quantity_B,*/
                                 Quantity = Quantity_B,
                                 Clients = bookstoreDBContext.Clients.Where(i => i.CredentialsId == idClient).FirstOrDefault(),
                                 Payment_status=false,
                                 OrderBooks = (ICollection<OrderBook>)new List<OrderBook>()
                             };
                             bookstoreDBContext.Orders.Add(neworder);
                            bookstoreDBContext.SaveChanges();

                            if (dbBook != null)
                            {
                                dbBook.Quantity = book.Quantity - Quantity_B; /*bookstoreDBContext.Books.Where(b => b.Id == ((Book)OrderBooksDataGrid.SelectedItem).Id).Select(b => b.Quantity).FirstOrDefault() - Quantity_B;*/
                                bookstoreDBContext.SaveChanges();
                            }

                            OrderBooksDataGrid.ItemsSource = repository.GetAll().ToList();

                            int latestOrder = bookstoreDBContext.Orders.OrderByDescending(o => o.Id).Select(o=> o.Id).FirstOrDefault();
                            OrderBook neworderbook =
                            new OrderBook()
                            {

                                OrderId = latestOrder,
                                BookId = book.Id, /* bookstoreDBContext.Books.Where(b => b.Id == ((Book)OrderBooksDataGrid.SelectedItem).Id).Select(b => b.Id).FirstOrDefault(),*/
                                Order = bookstoreDBContext.Orders.Where(i => i.Id == latestOrder).FirstOrDefault(),
                                Book = dbBook
                            };

                            bookstoreDBContext.OrderBooks.Add(neworderbook);
                            bookstoreDBContext.SaveChanges();

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
            //OrderBooksDataGrid.ItemsSource = bookstoreDBContext.Books.ToList();
           //OrderBooksDataGrid.ItemsSource = bookstoreDBContext.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.Genre).Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.Genre.Name, AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname }).ToList();
           RefreshOrderBooks();
           RefreshOrderDG();

        }

      
        

        private void Payment_ButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedRow = (dynamic)OrderDataGrid.SelectedItem;
            int id = selectedRow.Id;
            if (selectedRow.Payment_status != true)
            {
                Order order = bookstoreDBContext.Orders.FirstOrDefault(b => b.Id == id);
                //int? id = ((Order)OrderDataGrid.SelectedItem).Id;
                var dbEntry = bookstoreDBContext.Orders.FirstOrDefault(x => x.Id == order.Id);

                if (dbEntry != null)
                {
                    var Order = new
                    {
                        version = 3,
                        public_key = Config.LiqyPublicKey,
                        action = "pay",
                        amount = dbEntry.Price,
                        currency = "UAH",
                        description = "Pay book whit order " + dbEntry.Id,
                        order_id = dbEntry.Id,
                        language = "uk",
                        paytypes = ""
                    };
                    string data = System.Text.Json.JsonSerializer.Serialize(Order);
                    string data64 = LiqyPayHelper.CreateData(data); //Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
                    string privateKey = Bookstore.Config.LiqypayPrivateKey;

                    string signature_sourse = privateKey + data64 + privateKey;
                    var sha1 = SHA1.Create();
                    string signature = LiqyPayHelper.CreateSign(data64, privateKey);//Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(signature_sourse)))));
                    Payment payment = new Payment();
                    this.IsEnabled = false;
                    payment.Closed += Payment_Closed;
                    payment.Show();
                    payment.StartWebResourceRequest(Config.LiqyPatCheckoutURl, data64, signature);

                    //Task task = new Task(()=>GetStatus());
                    // task.Start();
                    //task.Wait();

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
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Update_ButtonClick(object sender, RoutedEventArgs e)
        {
            RefreshReserverBook();
        }

        private void RefreshReserverBook()
        {
            ReservDataGrid.ItemsSource = bookstoreDBContext.Reservations.Include(r => r.Client).Include(r => r.Book).Where(r => r.ClientId == idClient).Select(r => new { Id=r.Id, ClientName = r.Client.Name, 
                BookTitle = r.Book.Title,ReservationDate = r.ReservationDate, CheckOutDate = r.CheckoutDate,DueDate = r.ReturnDate,IsReturned = r.IsReturned}).ToList();
        }
    }
}
