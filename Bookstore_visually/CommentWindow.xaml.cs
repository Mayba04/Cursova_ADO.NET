using Bookstore;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using MessageBox = System.Windows.Forms.MessageBox;

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for CommentWindow.xaml
    /// </summary>
    public partial class CommentWindow : Window
    {
        ViewModel model;
        BookstoreDBContext bookstoreDBContext;
        private int ID;
        private int IdClient;

        public CommentWindow(int BookId, int idC)
        {
            InitializeComponent();
            ID = BookId;
            IdClient = idC;
            model = new ViewModel();
            bookstoreDBContext = new BookstoreDBContext();
            this.DataContext = model;
            UpdateBook();
            UpdateComment();
           
            byte[] imageData = bookstoreDBContext.Photos.Where(p => p.BookId == ID).Select(p => p.ImageData).FirstOrDefault();
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

        private void UpdateBook()
        {
            var book = bookstoreDBContext.Books.
                Where(b=>b.Id == ID).
                Select(b => new 
                { 
                    Id = b.Id, 
                    Title = b.Title, 
                    Publisher = b.Publisher, 
                    Year = b.Year, 
                    Price = b.Price, 
                    Quantity = b.Quantity, 
                    Genre = b.BookGenres.FirstOrDefault().Genre.Name, 
                    AuthorName = b.BookAuthors.FirstOrDefault().Author.Name, 
                    AuthorSurname = b.BookAuthors.FirstOrDefault().Author.Surname 
                }).FirstOrDefault();

            List<object> ff = new List<object>();
            model.Books = $"Title: " +
                $"{book.Title}" +
                $"\nAuthor: {book.AuthorSurname} {book.AuthorName}" +
                $"\nPrice: {book.Price} UAH" +
                $"\nPublisher: {book.Publisher}" +
                $"\nYear of publication: {book.Year}";
            ff.Add(book);
            model.AddCDGBook(ff);
        }

        private void UpdateComment()
        {
            model.ClearComment();
            var comment = bookstoreDBContext.Comments.
                Where(b => b.BookId == ID).
                Select(c => new { ID = c.Id, Name = c.Client.Name, Date = c.CreatedAt, Text = c.Text }).ToList();
            
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

        private void SendCommentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Length > 0)
            {
                var client = bookstoreDBContext.Clients.Where(c => c.CredentialsId == IdClient).FirstOrDefault();
                Comment comment = new Comment()
                {
                    Text = TextBox.Text,
                    CreatedAt = DateTime.Now,
                    BookId = ID,
                    ClientId = IdClient,
                };

                bookstoreDBContext.Comments.Add(comment);
                bookstoreDBContext.SaveChanges();
                UpdateComment();
            }
            TextBox.Clear();
        }

        private void CloseWind(object sender, RoutedEventArgs e)
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

        private void DeleteComment_Btn_Clik(object sender, RoutedEventArgs e)
        {
            DeleteComment();
        }

        private void DeleteComment()
        {
            if (CommentBox.SelectedItem != null)
            {
                var comment = (dynamic)CommentBox.SelectedItem;
                string n = comment.Name;
                string t = comment.Text;
                int id = comment.IdComment;
                var coomentdb = bookstoreDBContext.Comments.
                    Include(c => c.Client).
                        Where(c => c.Text == t && c.Client.Name == n && c.Id == id && c.ClientId == IdClient).
                            FirstOrDefault();
                if (coomentdb != null)
                {
                    bookstoreDBContext.Remove(coomentdb);
                    bookstoreDBContext.SaveChanges();
                    UpdateComment();
                    MessageBox.Show("Comment deleted!");
                }
                else
                {
                    MessageBox.Show("You cannot delete comments that are not your own");
                }
            }
        }
    }
}
