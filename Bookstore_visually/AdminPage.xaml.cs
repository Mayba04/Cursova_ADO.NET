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
using System.IO;
using Path = System.IO.Path;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Net.Sockets;
using Application = System.Windows.Application;
using MaterialDesignThemes.Wpf;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Security.Policy;
using System.Security.Cryptography.Pkcs;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {

        IRepository<Book> repository = new Repository<Book>(new BookstoreDBContext());
        BookstoreDBContext bookstoreDBContext;
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper;
        ViewModel model;
        Admin Admin_;
        public AdminPage(Admin admin)
        {
            InitializeComponent();
            model = new ViewModel();
            dataGrid.SelectionChanged += DataGrid_SelectionChanged;
            paletteHelper = new PaletteHelper();
            bookstoreDBContext = new BookstoreDBContext();
            this.DataContext = model;
            AdminCC.IsReadOnly = false;
            Admin_ = admin;
            RefreshAll();
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
            RefreshGenres();
            RefreshAdministrators();
            RefreshSetting();
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
                var book = bookstoreDBContext.Books.Where(b => b.Id == id).Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity, Genre = b.BookGenres.FirstOrDefault().Genre.Name, AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname }).FirstOrDefault();
                model.Books = $"Title: {book.Title}\nAuthor: {book.AuthorSurname} {book.AuthorName}\nPrice: {book.Price} UAH\nPublisher: {book.Publisher}\nYear of publication: {book.Year}";
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
                    int id = selectedRow.Id;
                    model.ClearComment();
                    var comment = bookstoreDBContext.Comments.Where(b => b.BookId == id).Select(c => new { ID = c.Id, Name = c.Client.Name, Date = c.CreatedAt, Text = c.Text }).ToList();

                    if (comment.Count != 0)
                    {
                        foreach (var item in comment)
                        {
                            CommentInfoBase commentInfo = new CommentInfoBase();
                            commentInfo.Name = item.Name;
                            commentInfo.Date = item.Date.ToShortDateString();
                            commentInfo.Text = item.Text;
                            commentInfo.IdComment = item.ID;
                            model.AddComment(commentInfo);
                        }
                    }
                    else
                    {
                        CommentInfoBase commentInfo = new CommentInfoBase();
                        commentInfo.Name = "No comments yet";
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
        }

        private void AddingBooks_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshBook();
            RefreshAuthors();
            RefreshGenres();
        }

        private void DeleteBookBTN(object sender, RoutedEventArgs e)
        {
            DeleteBook();
        }

        private void DeleteBook()
        {
            if (dataGrid.SelectedItem != null)
            {
                if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete the book?" +
                    "\nAfter all, all the information associated with it will also be deleted!", "WARNING", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    var selectedRow = (dynamic)dataGrid.SelectedItem;
                    int id = selectedRow.Id;

                    bookstoreDBContext.Books.Remove(bookstoreDBContext.Books.Where(b => b.Id == id).FirstOrDefault());
                    bookstoreDBContext.Photos.Remove(bookstoreDBContext.Photos.Where(p => p.BookId == id).FirstOrDefault());
                    bookstoreDBContext.Comments.RemoveRange(bookstoreDBContext.Comments.Where(c => c.BookId == id).ToList());
                    bookstoreDBContext.BookAuthors.RemoveRange(bookstoreDBContext.BookAuthors.Where(ba => ba.BookId == id).ToList());
                    bookstoreDBContext.BookGenres.RemoveRange(bookstoreDBContext.BookGenres.Where(ba => ba.BookId == id).ToList());
                    var idOrders = bookstoreDBContext.OrderBooks.Where(OB => OB.BookId == id).ToList();
                    bookstoreDBContext.OrderBooks.RemoveRange(bookstoreDBContext.OrderBooks.Where(Ob => Ob.BookId == id).ToList());
                    bookstoreDBContext.Reservations.RemoveRange(bookstoreDBContext.Reservations.Where(r => r.BookId == id).ToList());

                    foreach (var item in idOrders)
                    {
                        bookstoreDBContext.Orders.Remove(bookstoreDBContext.Orders.Where(O => O.Id == item.OrderId).FirstOrDefault());
                    }
                    bookstoreDBContext.SaveChanges();
                    RefreshAll();
                }

            }
        }

        private void RefreshBook()
        {
            dataGrid.ItemsSource = bookstoreDBContext.Books.Select(b => new { Id = b.Id, Title = b.Title, Publisher = b.Publisher, Year = b.Year, Price = b.Price, Quantity = b.Quantity }).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedRow = (dynamic)dataGrid.SelectedItem;
                int id = selectedRow.Id;
                EditBook editBook = new EditBook(id);
                this.IsEnabled = false;
                editBook.Closed += EditBook_Closed;
                editBook.Show();
            }
        }

        private void EditBook_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshBook();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RefreshBook();
        }

        private void DeleteCommentBTN(object sender, RoutedEventArgs e)
        {
            if (CommentBox.SelectedItem != null)
            {
                var selectedRow = (dynamic)CommentBox.SelectedItem;
                int id = selectedRow.IdComment;
                if (id > 0)
                {
                    bookstoreDBContext.Comments.Remove(bookstoreDBContext.Comments.Where(b => b.Id == id).FirstOrDefault());
                    bookstoreDBContext.SaveChanges();
                    RefreshComment();
                    RefreshImage();
                    RefreshBookInfo();
                }
            }
        }

        private void DeleteComment()
        {
            if (CommentBox.SelectedItem != null)
            {
                var selectedRow = (dynamic)CommentBox.SelectedItem;
                int id = selectedRow.IdComment;
                if (id > 0)
                {
                    bookstoreDBContext.Comments.Remove(bookstoreDBContext.Comments.Where(b => b.Id == id).FirstOrDefault());
                    bookstoreDBContext.SaveChanges();
                    RefreshComment();
                    RefreshImage();
                    RefreshBookInfo();
                }
            }
        }

        private void AddPhotoBTN(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                    var selectedRow = (dynamic)dataGrid.SelectedItem;
                    int id = selectedRow.Id;
                    
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string filePath = fileDialog.FileName;
                      
                        using (var context = new BookstoreDBContext())
                        {

                            var photo2 = bookstoreDBContext.Photos.Where(p => p.BookId == id).FirstOrDefault();

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
              
              

                var order = bookstoreDBContext.Orders.Where(o => o.Id == id).FirstOrDefault();
                if (order.Payment_status == false)
                {
                    bookstoreDBContext.Remove(order);
                    var orderb = bookstoreDBContext.OrderBooks.FirstOrDefault(o => o.OrderId == order.Id);
                    var book = bookstoreDBContext.Books.Where(b => b.Id == orderb.BookId).FirstOrDefault();
                    bookstoreDBContext.OrderBooks.Remove(orderb);
                    bookstoreDBContext.SaveChanges();
                    book.Quantity += (int)order.Quantity;
                    bookstoreDBContext.Update(book);
                    bookstoreDBContext.SaveChanges();
                    RefreshBook();
                }
                else
                {
                    bookstoreDBContext.Orders.Remove(bookstoreDBContext.Orders.Where(b => b.Id == id).FirstOrDefault());
                    bookstoreDBContext.OrderBooks.Remove(bookstoreDBContext.OrderBooks.Where(b => b.OrderId == id).FirstOrDefault());
                    bookstoreDBContext.SaveChanges();
                }
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
                        bookstoreDBContext.BookGenres.Remove(bookstoreDBContext.BookGenres.Where(bg => bg.BookId == bookid.BookId).FirstOrDefault());
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
            var clientList = bookstoreDBContext.Clients.Include(c => c.Credentials).Select(c => new { CredentialsId = c.CredentialsId, Name = c.Name, Email = c.Email, Login = c.Credentials.Login, Password = c.Credentials.Password, Phone = c.PhoneNumber }).ToList();
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
                ChangeDC changeDC = new ChangeDC(id);
                this.IsEnabled = false;
                changeDC.Closed += ChangeDC_Closed;
                changeDC.Show();
                
               
            }
            
        }

        private void ChangeDC_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshClient();
            RefreshAdministrators();
        }

        private void RefreshReserved()
        {
            ReservDGAdmin.ItemsSource = bookstoreDBContext.Reservations.Include(r => r.Client).Include(r => r.Book).Select(r => new {
                     Id = r.Id,
                     ClientName = r.Client.Name,
                     BookTitle = r.Book.Title,
                     ReservationDate = r.ReservationDate.ToShortDateString(),
                     ReceivingDate = r.CheckoutDate.ToShortDateString(),
                     ReturnDate = r.ReturnDate.ToShortDateString(),
                     IsReturned = r.IsReturned
                 }).ToList();
        }

        private void UpdateReserveAdmin_BTN(object sender, RoutedEventArgs e)
        {
            RefreshReserved();
        }

        private void ChangeReserveAdmin_BTN(object sender, RoutedEventArgs e)
        {
            if (ReservDGAdmin.SelectedItem != null)
            {
                var selectedd = (dynamic)ReservDGAdmin.SelectedItem;
                if (selectedd.IsReturned != true)
                {
                    ChangeReserve changeReserve = new ChangeReserve(selectedd.Id);
                    this.IsEnabled = false;
                    changeReserve.Closed += ChangeReserve_Closed;
                    changeReserve.Show();
                }
                else
                {
                    MessageBox.Show("The book is returned!");
                }
            }
        }

        private void ChangeReserve_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshReserved();
            RefreshBook();
        }

        private void DeleteReserveAdmin_BTN(object sender, RoutedEventArgs e)
        {
            if (ReservDGAdmin.SelectedItem != null)
            {
                var selectedd = (dynamic)ReservDGAdmin.SelectedItem;
                int id = selectedd.Id;
                var reservation = bookstoreDBContext.Reservations.Where(r => r.Id == id).FirstOrDefault();
               

                if (reservation.IsReturned != true && reservation.CheckoutDate.Year <= 1)
                {
                    bookstoreDBContext.Remove(reservation);
                    bookstoreDBContext.SaveChanges();
                    var book = bookstoreDBContext.Books.Where(b => b.Id == reservation.BookId).FirstOrDefault();
                    book.Quantity++;
                    bookstoreDBContext.Update(book);
                    bookstoreDBContext.SaveChanges();
                    RefreshBook();
                }
                else if (reservation.IsReturned == true && reservation.CheckoutDate.Year >= 1 && reservation.ReturnDate.Year >=1)
                {
                    bookstoreDBContext.Remove(reservation);
                    bookstoreDBContext.SaveChanges();
                }
                else
                {
                    MessageBox.Show("The book has been taken or already returned!");
                }
              
                RefreshReserved();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Login loin = new Login();
            loin.Show();
            this.Close();
        }

        private void minApp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void CloseWind(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddGenreBTN(object sender, RoutedEventArgs e)
        {
            AddingGenre addingGenre = new AddingGenre();
            addingGenre.Closed += AddingGenre_Closed;
            this.IsEnabled = false;
            addingGenre.Show();
        }

        private void AddingGenre_Closed(object? sender, EventArgs e)
        {
            this.IsEnabled = true;
            RefreshGenres();
        }

        private void RefreshGenres()
        {
            GenreDG.ItemsSource = bookstoreDBContext.Genres.Select(a => new { Id = a.Id, GenreName = a.Name }).ToList();
        }

        private void GetGenreAllBTN(object sender, RoutedEventArgs e)
        {
            RefreshGenres();
        }

        private void DeleteGenreBTN(object sender, RoutedEventArgs e)
        {
            if (GenreDG.SelectedItem != null)
            {
                if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete the genre?\nAfter all, all the information associated with it will also be deleted!", "WARNING", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    var selectedRow = (dynamic)GenreDG.SelectedItem;
                    int id = selectedRow.Id;
                    var idB = bookstoreDBContext.Books.Where(b => b.BookGenres.FirstOrDefault().GenreId == id).ToList();

                    foreach (var book in idB)
                    {
                        bookstoreDBContext.Books.Remove(bookstoreDBContext.Books.Where(b => b.Id == ((Book)book).Id).FirstOrDefault());
                        bookstoreDBContext.Photos.Remove(bookstoreDBContext.Photos.Where(p => p.BookId == ((Book)book).Id).FirstOrDefault());
                        bookstoreDBContext.Comments.RemoveRange(bookstoreDBContext.Comments.Where(c => c.BookId == ((Book)book).Id).ToList());
                        bookstoreDBContext.BookAuthors.RemoveRange(bookstoreDBContext.BookAuthors.Where(ba => ba.BookId == ((Book)book).Id).ToList());
                        var idOrders = bookstoreDBContext.OrderBooks.Where(OB => OB.BookId == ((Book)book).Id).ToList();
                        bookstoreDBContext.OrderBooks.RemoveRange(bookstoreDBContext.OrderBooks.Where(Ob => Ob.BookId == ((Book)book).Id).ToList());
                        bookstoreDBContext.Reservations.RemoveRange(bookstoreDBContext.Reservations.Where(r => r.BookId == ((Book)book).Id).ToList());
                        foreach (var item in idOrders)
                        {
                            bookstoreDBContext.Orders.Remove(bookstoreDBContext.Orders.Where(O => O.Id == item.OrderId).FirstOrDefault());
                        }
                        bookstoreDBContext.Genres.Remove(bookstoreDBContext.Genres.Where(g => g.Id == id).FirstOrDefault());
                    }

                    bookstoreDBContext.SaveChanges();
                    RefreshAll();
                }

            }
        }

        private void RefreshAdministrators()
        {
            AdminDG.ItemsSource = bookstoreDBContext.Administrators.Select(a => new { Id = a.Id, Name = a.Name, Email = a.Email, Phone = a.PhoneNumber }).ToList();
        }

        private void DeleteAdministratorBTN(object sender, RoutedEventArgs e)
        {
            DeleteAdministrator();
        }

        private void DeleteAdministrator()
        {
            if (AdminDG.ItemsSource != null)
            {
                var selectedRow = (dynamic)AdminDG.SelectedItem;
                int id = selectedRow.Id;
                var admin = bookstoreDBContext.Administrators.Where(a => a.Id == id).FirstOrDefault();
                if (Admin_.Status_Admin == true)
                {
                    if (admin.Status_Admin != true)
                    {
                        bookstoreDBContext.Remove(admin);
                        bookstoreDBContext.SaveChanges();
                        MessageBox.Show("The administrator has been removed !");
                        RefreshAdministrators();
                    }
                    else
                    {
                        MessageBox.Show("This administrator cannot be removed");
                    }
                }
                else
                {
                    MessageBox.Show("You do not have rights to remove administrators!");
                }
            }
        }

        private void GetAdministratorAllBTN(object sender, RoutedEventArgs e)
        {
            RefreshAdministrators();
        }

        private void Help_Button(object sender, RoutedEventArgs e)
        {
            string url = "https://docs.google.com/document/d/10dTN9hMfxQVV_B1RDUqtlpTcOogBPV7gtqxmqtZJHyg/edit?usp=sharing";

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

        private void ChangeBtn(object sender, RoutedEventArgs e)
        {
            if (Audit())
            {
                Admin_.Email = EmailBox.Text;
                Admin_.Name = NameBox.Text;
                Admin_.Login = LoginBox.Text;
                Admin_.Password = PasswordBox.Text;
                Admin_.PhoneNumber = PhoneTextBox.Text;

                bookstoreDBContext.Administrators.Update(Admin_);
                bookstoreDBContext.SaveChanges();
                MessageBox.Show("Saved!");
                RefreshAdministrators();
            }
        }

       

        private void RefreshSetting()
        {
            LoginBox.Text = Admin_.Login;
            PasswordBox.Text = Admin_.Password;
            NameBox.Text = Admin_.Name;
            EmailBox.Text = Admin_.Email;
            PhoneTextBox.Text = Admin_.PhoneNumber;
        }



        private bool Audit()
        {
            if (Admin_.Login != LoginBox.Text)
            {
                if (string.IsNullOrEmpty(LoginBox.Text))
                {
                    MessageBox.Show("Enter your login");
                    return false;
                }

                if (LoginBox.Text.Length < 4 || LoginBox.Text.Length > 20)
                {
                    MessageBox.Show("Login should be between 4 and 20 characters long");
                    return false;
                }

                if (!char.IsUpper(LoginBox.Text[0]))
                {
                    MessageBox.Show("Login should start with an uppercase letter");
                    return false;
                }

                if (!LoginBox.Text.All(c => char.IsLetterOrDigit(c) || c == '_'))
                {
                    MessageBox.Show("Login should only contain letters, digits, and underscores");
                    return false;
                }

                if (Admin_.Login != LoginBox.Text)
                {
                    var sLoginDb = bookstoreDBContext.Credentials.Where(c => c.Login == LoginBox.Text).FirstOrDefault();
                    var sLoginDb2 = bookstoreDBContext.Administrators.Where(c => c.Login == LoginBox.Text).FirstOrDefault();
                    if (sLoginDb != null || sLoginDb2 != null)
                    {
                        MessageBox.Show("Such a login already exists");
                        return false;
                    }
                }
            }

            if (Admin_.Password != PasswordBox.Text)
            {
                if (string.IsNullOrEmpty(PasswordBox.Text))
                {
                    MessageBox.Show("Enter your password");
                    return false;
                }

                if (!IsPasswordValid(PasswordBox.Text))
                {
                    MessageBox.Show("Password should be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, and one digit.");
                    return false;
                }
            }

            if (Admin_.Email != EmailBox.Text)
            {
                if (string.IsNullOrEmpty(EmailBox.Text))
                {
                    MessageBox.Show("Enter your email address");
                    return false;
                }

                if (!IsValidEmail(EmailBox.Text))
                {
                    MessageBox.Show("Enter a valid email address");
                    return false;
                }

                var user = bookstoreDBContext.Clients.FirstOrDefault(x => x.Email == EmailBox.Text);
                var user2 = bookstoreDBContext.Administrators.FirstOrDefault(x => x.Email == EmailBox.Text);
                if (user != null || user2 != null)
                {
                    MessageBox.Show("A user with this login already exists");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(NameBox.Text))
            {
                MessageBox.Show("Enter your Name");
                return false;
            }

            if (!ValidateName(NameBox.Text))
            {
                MessageBox.Show("Name should start with an uppercase letter and contain 4 to 20 characters (letters, numbers, spaces, dashes, underscores).");
                return false;
            }


            if (string.IsNullOrEmpty(PhoneTextBox.Text))
            {
                MessageBox.Show("Enter your Phone");
                return false;
            }


            if (!ValidatePhone(PhoneTextBox.Text))
            {
                MessageBox.Show("The phone number is incorrect, example: +380123456789");
                return false;
            }

            if (Admin_.PhoneNumber != PhoneTextBox.Text)
            {
                var phone = bookstoreDBContext.Clients.FirstOrDefault(x => x.PhoneNumber == PhoneTextBox.Text);
                var phone2 = bookstoreDBContext.Administrators.FirstOrDefault(x => x.PhoneNumber == PhoneTextBox.Text);
                if (phone != null || phone2 != null)
                {
                    MessageBox.Show("A user with this phone number already exists");
                    return false;
                }
            }

            return true;
        }

        private bool IsPasswordValid(string password)
        {
            var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            return passwordRegex.IsMatch(password);
        }

        private bool IsValidEmail(string email)
        {
            string emailRegexPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(emailRegexPattern);
            return regex.IsMatch(email);
        }

        private bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (name.Length < 4 || name.Length > 20)
                return false;

            if (!char.IsUpper(name[0]))
                return false;

            var allowedCharacters = new Regex(@"^[a-zA-Z0-9\s_-]+$");
            if (!allowedCharacters.IsMatch(name))
                return false;

            return true;
        }

        private bool ValidatePhone(string phone)
        {
            Regex regex = new Regex(@"^\+380\d{9}$");
            return regex.IsMatch(phone);
        }
    }
}
