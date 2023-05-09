﻿using Bookstore.Entities;
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
using System.IO;
using Path = System.IO.Path;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Net.Sockets;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {

        IRepository<Book> repository = new Repository<Book>(new BookstoreDBContext());
        BookstoreDBContext bookstoreDBContext = new BookstoreDBContext();

        ViewModel model;
        public AdminPage()
        {
            InitializeComponent();
            model = new ViewModel();
            dataGrid.SelectionChanged += DataGrid_SelectionChanged;
            this.DataContext = model;
            AdminCC.IsReadOnly = false;
            RefreshBook();
            RefreshOrder();
            RefreshAuthors();
            RefreshClient();
            RefreshReserved();
        }

        private void RefreshAll()
        {
            RefreshBook();
            RefreshOrder();
            RefreshAuthors();
            RefreshClient();
            RefreshReserved();
            RefreshComment();
            RefreshImage();
            RefreshBookInfo();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshComment();
            RefreshImage();
            RefreshBookInfo();
        }

        public void RefreshBookInfo()
        {
            model.Books = null;
            if (dataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)dataGrid.SelectedItem;
                int id = selectedRow.Id;
                var book = bookstoreDBContext.Books.Where(b => b.Id == id).Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.Genre.Name, AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname }).FirstOrDefault();

                //List<object> ff = new List<object>();
                model.Books = $"Title: {book.Title}\nAuthor: {book.AuthorSurname} {book.AuthorName}\nPrice: {book.Price} UAH\nPublisher: {book.Publisher}\nYear of publication: {book.Year}";
                //ff.Add(book);
                //model.AddCDGBook(ff);
            }
                
        }

        public void RefreshImage()
        {
            model.Image = null;
            if (dataGrid.SelectedItem != null)
            {
                
                var selectedRow = (dynamic)dataGrid.SelectedItem;
                int id = selectedRow.Id;
                byte[] imageData = bookstoreDBContext.Photos.Where(p => p.BookId == id).Select(p => p.ImageData).FirstOrDefault();
                if (imageData != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream(imageData))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        model.Image = bitmapImage;
                    }
                }
            }
        }

        public void RefreshComment()
        {
            model.Image = null;
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    var selectedRow = (dynamic)dataGrid.SelectedItem;
                    int id = selectedRow.Id;//bug
                    model.ClearComment();
                    var comment = bookstoreDBContext.Comments.Where(b => b.BookId == id).Select(c => new { ID = c.Id, Name = c.Client.Name, Date = c.CreatedAt, Text = c.Text }).ToList();

                    foreach (var item in comment)
                    {
                        CommentInfo commentInfo = new CommentInfo();
                        commentInfo.Name = item.Name;
                        commentInfo.Date = item.Date.ToShortDateString();
                        commentInfo.Text = item.Text;
                        commentInfo.IdComment = item.ID;
                        model.AddComment(commentInfo);

                    }
                }
                catch (Exception)
                {

                    throw;
                }
               
            }
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            AddingBooks addingBooks = new AddingBooks();
            this.IsEnabled = false;
            addingBooks.Closed += AddingBooks_Closed;
            addingBooks.Show();
            
            //RefreshDataGrid();
            //repository.Insert(newBook);
            // repository.Save(); // зберігаємо зміни в базу даних
        }

        private void AddingBooks_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshBook();
        }

        private void DeleteBookBTN(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems != null)
            {
                if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete the book?\nAfter all, all the information associated with it will also be deleted!", "WARNING", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (var book in dataGrid.SelectedItems)
                    {
                        // bookstoreDBContext.Books.Remove(book as Book);
                        //repository.Delete(((Book)book).Id);
                        bookstoreDBContext.Books.Remove(bookstoreDBContext.Books.Where(b => b.Id == ((Book)book).Id).FirstOrDefault());
                        bookstoreDBContext.Photos.Remove(bookstoreDBContext.Photos.Where(p => p.BookId == ((Book)book).Id).FirstOrDefault());
                        bookstoreDBContext.Comments.RemoveRange(bookstoreDBContext.Comments.Where(c => c.BookId == ((Book)book).Id).ToList());
                        bookstoreDBContext.BookAuthors.RemoveRange(bookstoreDBContext.BookAuthors.Where(ba => ba.BookId == ((Book)book).Id).ToList());
                        var idOrders = bookstoreDBContext.OrderBooks.Where(OB => OB.BookId == ((Book)book).Id).ToList();
                        bookstoreDBContext.OrderBooks.RemoveRange(bookstoreDBContext.OrderBooks.Where(Ob => Ob.BookId == ((Book)book).Id).ToList());
                        bookstoreDBContext.Reservations.RemoveRange(bookstoreDBContext.Reservations.Where(r=>r.BookId == ((Book)book).Id).ToList());
                        foreach (var item in idOrders)
                        {
                            bookstoreDBContext.Orders.Remove(bookstoreDBContext.Orders.Where(O => O.Id == item.OrderId).FirstOrDefault());
                        }

                    }
                    bookstoreDBContext.SaveChanges();
                    RefreshAll();
                }
               
            }
        }

        private void RefreshBook()
        {
            dataGrid.ItemsSource = repository.GetAll().ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedBook = (Book)dataGrid.SelectedItem;
                UpdateBook(selectedBook);
                RefreshComment();
                RefreshImage();
                RefreshBookInfo();
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
            RefreshBook();
        }

        private void DeleteCommentBTN(object sender, RoutedEventArgs e)
        {
            if (CommentBox.SelectedItem != null)
            {
                var selectedRow = (dynamic)CommentBox.SelectedItem;
                int id = selectedRow.IdComment;
                bookstoreDBContext.Comments.Remove(bookstoreDBContext.Comments.Where(b => b.Id == id).FirstOrDefault());
                bookstoreDBContext.SaveChanges();
                RefreshComment();
                RefreshImage();
                RefreshBookInfo();
            }
        }

        private void AddPhotoBTN(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedBook = (Book)dataGrid.SelectedItem;
               
              
                
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string filePath = fileDialog.FileName;
                      
                        using (var context = new BookstoreDBContext())
                        {

                            var photo2 = bookstoreDBContext.Photos.Where(p => p.BookId == selectedBook.Id).FirstOrDefault();

                            if (photo2 != null)
                            {
                                photo2.Name = Path.GetFileName(filePath);
                                photo2.ImageData = File.ReadAllBytes(filePath);

                               
                                
                                context.Photos.Update(photo2);
                                context.SaveChanges();
                                RefreshComment();
                                RefreshImage();
                                RefreshBookInfo();
                            }
                        }

                    }    
            }
        }

        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            Login loin = new Login();
            loin.Show();
            this.Close();
        }
        //Order
        private void OrderAll_ButtonClick(object sender, RoutedEventArgs e)
        {
            AdminOrder.ItemsSource = bookstoreDBContext.Orders.Include(o => o.OrderBooks).ThenInclude(ob => ob.Book).Include(o => o.Clients)
         .Select(o => new { Id = o.Id, Date = o.Date, Price = o.Price, Quantity = o.Quantity, Payment_status = o.Payment_status, BookTitle = o.OrderBooks.FirstOrDefault().Book.Title, ClientName = o.Clients.Name }).ToList();
        }

        private void DeleteOrderBTN(object sender, RoutedEventArgs e)
        {
            if (AdminOrder.SelectedItem != null)
            {
                var selectedRow = (dynamic)AdminOrder.SelectedItem;
                int id = selectedRow.Id;
                bookstoreDBContext.Orders.Remove(bookstoreDBContext.Orders.Where(b => b.Id == id).FirstOrDefault());
                bookstoreDBContext.OrderBooks.Remove(bookstoreDBContext.OrderBooks.Where(b => b.OrderId == id).FirstOrDefault());
                bookstoreDBContext.SaveChanges();
                RefreshOrder();
            }
        }

        private void RefreshOrder()
        {
            AdminOrder.ItemsSource = bookstoreDBContext.Orders.Include(o => o.OrderBooks).ThenInclude(ob => ob.Book).Include(o => o.Clients)
        .Select(o => new { Id = o.Id, Date = o.Date, Price = o.Price, Quantity = o.Quantity, Payment_status = o.Payment_status, BookTitle = o.OrderBooks.FirstOrDefault().Book.Title, ClientName = o.Clients.Name }).ToList();
        }

        private void RefreshAuthors()
        {
            AdminAuthor.ItemsSource = bookstoreDBContext.Authors.Select(a => new { Id = a.Id, Name = a.Name, Surname = a.Surname }).ToList();
        }

        private void GetAuthorAllBTN(object sender, RoutedEventArgs e)
        {
            RefreshAuthors();
        }

        private void AddAuthorBTN(object sender, RoutedEventArgs e)
        {
            AddingAuthors addingAuthors = new AddingAuthors();
            addingAuthors.Closed += AddingAuthors_Closed;
            this.IsEnabled = false;
            addingAuthors.Show();
        }

        private void AddingAuthors_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshAuthors();
        }

        private void DeleteAuthorBTN(object sender, RoutedEventArgs e)
        {
            if(AdminAuthor.SelectedItem != null)
            {
                if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete the author?\nAfter all, all the information associated with it will also be deleted!", "WARNING", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    var selectedRow = (dynamic)AdminAuthor.SelectedItem;
                    int id = selectedRow.Id;
                    var idB = bookstoreDBContext.BookAuthors.Where(ba => ba.AuthorId == id).ToList();
                    foreach (var bookid in idB)
                    {
                        var orderId = bookstoreDBContext.OrderBooks.Where(ob => ob.BookId == bookid.BookId).ToList();
                        bookstoreDBContext.Comments.RemoveRange(bookstoreDBContext.Comments.Where(c => c.BookId == bookid.BookId).ToList());
                        bookstoreDBContext.Reservations.RemoveRange(bookstoreDBContext.Reservations.Where(r => r.BookId == bookid.BookId).ToList());
                        bookstoreDBContext.Photos.Remove(bookstoreDBContext.Photos.Where(p => p.BookId == bookid.BookId).FirstOrDefault());
                        foreach (var idO in orderId)
                        {
                            bookstoreDBContext.Orders.Remove(bookstoreDBContext.Orders.Where(o => o.Id == idO.OrderId).FirstOrDefault());
                        }
                        bookstoreDBContext.OrderBooks.RemoveRange(orderId);
                        bookstoreDBContext.Books.Remove(bookstoreDBContext.Books.Where(b => b.Id == bookid.BookId).FirstOrDefault());
                    }
                    bookstoreDBContext.BookAuthors.RemoveRange(idB);
                    bookstoreDBContext.Authors.Remove(bookstoreDBContext.Authors.Where(a => a.Id == id).FirstOrDefault());

                    bookstoreDBContext.SaveChanges();
                    RefreshAll();
                }
               
            }
        }

        private void GetClientAllBTN(object sender, RoutedEventArgs e)
        {
            RefreshClient();
        }

        private void RefreshClient()
        {
            var clientList = bookstoreDBContext.Clients.Include(c => c.Credentials).Select(c => new { CredentialsId = c.CredentialsId, Name = c.Name, Email = c.Email, Status_admin = c.Status_admin, Login = c.Credentials.Login, Password = c.Credentials.Password }).ToList();
            AdminCC.ItemsSource = clientList;
        }

        private void DeleteClientBTN(object sender, RoutedEventArgs e)
        {
            if (AdminCC.SelectedItem != null)
            {
                var selectedd = (dynamic)AdminCC.SelectedItem;
                int id = selectedd.CredentialsId;
                bookstoreDBContext.Clients.Remove(bookstoreDBContext.Clients.Where(c => c.CredentialsId == id).FirstOrDefault());
                bookstoreDBContext.Credentials.Remove(bookstoreDBContext.Credentials.Where(c => c.Id == id).FirstOrDefault());
                foreach (var item in bookstoreDBContext.Orders.Where(o => o.ClientId == id).ToList())
                {
                    bookstoreDBContext.OrderBooks.Remove(bookstoreDBContext.OrderBooks.Where(o => o.OrderId == item.Id).FirstOrDefault());
                }
                bookstoreDBContext.Orders.RemoveRange(bookstoreDBContext.Orders.Where(o => o.ClientId == id).ToList());
                bookstoreDBContext.Comments.RemoveRange(bookstoreDBContext.Comments.Where(c => c.ClientId == id).ToList());
                bookstoreDBContext.Reservations.RemoveRange(bookstoreDBContext.Reservations.Where(r=>r.ClientId == id).ToList());
                bookstoreDBContext.SaveChanges();
                RefreshOrder();
                RefreshComment();
                RefreshClient();
                RefreshReserved();

            }
        }

        private void UpdateClientBTN(object sender, RoutedEventArgs e)
        {
            if (AdminCC.SelectedItem != null)
            {
                var selectedd = (dynamic)AdminCC.SelectedItem;
                int id = selectedd.CredentialsId;
                if (selectedd.Status_admin == false)
                {
                    ChangeDC changeDC = new ChangeDC(id);
                    this.IsEnabled = false;
                    changeDC.Closed += ChangeDC_Closed;
                    changeDC.Show();
                }
               
            }
            
        }

        private void ChangeDC_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshClient();
        }

        private void RefreshReserved()
        {
            ReservDGAdmin.ItemsSource = bookstoreDBContext.Reservations.Include(r => r.Client).Include(r => r.Book).Select(r => new {
                     Id = r.Id,
                     ClientName = r.Client.Name,
                     BookTitle = r.Book.Title,
                     ReservationDate = r.ReservationDate,
                     CheckOutDate = r.CheckoutDate,
                     DueDate = r.ReturnDate,
                     IsReturned = r.IsReturned
                 }).ToList();
        }

        private void UpdateReserveAdmin_BTN(object sender, RoutedEventArgs e)
        {
            RefreshReserved();
        }

        private void ChangeReserveAdmin_BTN(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteReserveAdmin_BTN(object sender, RoutedEventArgs e)
        {
            if (ReservDGAdmin.SelectedItem != null)
            {
                var selectedd = (dynamic)ReservDGAdmin.SelectedItem;
                int id = selectedd.Id;
                bookstoreDBContext.Remove(bookstoreDBContext.Reservations.Where(r => r.Id == id).FirstOrDefault());
            }
        }
    }
}
