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
            RefreshDataGrid();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateComment();
            UpdateImage();
            UpdateBookInfo();
        }

        public void UpdateBookInfo()
        {
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

        public void UpdateImage()
        {
            if (dataGrid.SelectedItem != null)
            {
                model.Image = null;
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

        public void UpdateComment()
        {
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
            RefreshDataGrid();
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
                    bookstoreDBContext.Photos.Remove(bookstoreDBContext.Photos.Where(p => p.BookId == ((Book)book).Id).FirstOrDefault());
                }
                bookstoreDBContext.SaveChanges();
                RefreshDataGrid();
                UpdateComment();
                UpdateImage();
                UpdateBookInfo();
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
                UpdateComment();
                UpdateImage();
                UpdateBookInfo();
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

        private void DeleteCommentBTN(object sender, RoutedEventArgs e)
        {
            if (CommentBox.SelectedItem != null)
            {
                var selectedRow = (dynamic)CommentBox.SelectedItem;
                int id = selectedRow.IdComment;
                bookstoreDBContext.Comments.Remove(bookstoreDBContext.Comments.Where(b => b.Id == id).FirstOrDefault());
                bookstoreDBContext.SaveChanges();
                UpdateComment();
                UpdateImage();
                UpdateBookInfo();
            }
        }

        private void AddPhotoBTN(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selectedBook = (Book)dataGrid.SelectedItem;
               
              
                
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (fileDialog.ShowDialog() == true)
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
                                UpdateComment();
                                UpdateImage();
                                UpdateBookInfo();
                            }
                        }

                    }
                  

                
                
                
            }
        }

        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
