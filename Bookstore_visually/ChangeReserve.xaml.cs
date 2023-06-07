using Bookstore;
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
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

namespace Bookstore_visually
{
    /// <summary>
    /// Interaction logic for ChangeReserve.xaml
    /// </summary>
    public partial class ChangeReserve : Window
    {
        BookstoreDBContext bookstoreDBContext;
        private int id;

        public bool RecevD { get; set; }
       
        public bool ReturnD { get; set; }
     
        public bool ResrvD { get; set; }

        DateTime reservDate;
        DateTime recevDate;
        DateTime returnDate;

        public ChangeReserve(int res)
        {
            InitializeComponent();
            bookstoreDBContext = new BookstoreDBContext();
            id = res;
            recevDate = DateTime.Now;
            returnDate = DateTime.Now;
            ReservDate.SelectedDateChanged += ReservDate_SelectedDateChanged;
            RecevDate.SelectedDateChanged += RecevDate_SelectedDateChanged;
            ReturnDate.SelectedDateChanged += ReturnDate_SelectedDateChanged;
            RecevDate.BlackoutDates.AddDatesInPast();
            ReturnDate.BlackoutDates.AddDatesInPast();

            Inizialize();
        }

        private void ReservDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReservDate.SelectedDate.HasValue)
            {
                DateTime newDate = ReservDate.SelectedDate.Value;

                if (newDate > recevDate || newDate > returnDate)
                {
                    ReservDate.SelectedDate = reservDate;  
                }
                else
                {
                    reservDate = newDate;
                }
                ResrvD = true;
                
            }
        }

        private void RecevDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecevDate.SelectedDate.HasValue)
            {
                DateTime newDate = RecevDate.SelectedDate.Value;

                if (newDate < reservDate || newDate > returnDate)
                {
                    RecevDate.SelectedDate = recevDate;
                }
                else
                {
                    recevDate = newDate;
                }
                RecevD = true;
            }
        }

        private void ReturnDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReturnDate.SelectedDate.HasValue)
            {
                DateTime newDate = ReturnDate.SelectedDate.Value;
                if (recevDate.Year !=1)
                {
                    if (newDate < recevDate || newDate < reservDate)
                    {
                        ReturnDate.SelectedDate = returnDate;
                    }
                    else
                    {
                        returnDate = newDate;
                    }
                }
                else
                {
                    e.Handled = true;
                }
                ReturnD = true;


            }
        }

        


        private void Inizialize()
        {
            var rr = bookstoreDBContext.Reservations.Where(r => r.Id == id).Include(r => r.Client).Include(r => r.Book).Select(r => new {
                Id = r.Id,
                ClientName = r.Client.Name,
                BookTitle = r.Book.Title,
                ReservationDate = r.ReservationDate,
                ReceivingDate = r.CheckoutDate,
                ReturnDate = r.ReturnDate,
                IsReturned = r.IsReturned
            }).FirstOrDefault();
            
            CNameBox.Text = rr.ClientName;
            BookTBox.Text = rr.BookTitle;
            reservDate = rr.ReservationDate;
            ReservDate.Text = rr.ReservationDate.ToLongDateString();
            
            if (rr.ReceivingDate.Year != 1 || rr.ReturnDate.Year != 1)
            {
                RecevDate.Text = rr.ReceivingDate.ToLongDateString();
                ReturnDate.Text = rr.ReturnDate.ToLongDateString();
                RecevD = false;
                ReturnD = false;
            }
            IsReturn.IsChecked = rr.IsReturned;
           
        }

        

        private void closeWind(object sender, RoutedEventArgs e)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = bookstoreDBContext.Reservations.Where(r => r.Id == id).FirstOrDefault();
            if (RecevD == true || ReturnD == true || ResrvD == true)
            {
                DateTime dateReturn;
                if (DateTime.TryParse(ReservDate.Text, out dateReturn))
                {
                    reservation.ReservationDate = DateTime.Parse(ReservDate.Text);
                }
                if (DateTime.TryParse(RecevDate.Text, out dateReturn))
                {
                    reservation.CheckoutDate = DateTime.Parse(RecevDate.Text);
                }
                if (DateTime.TryParse(ReturnDate.Text, out dateReturn))
                {
                    reservation.ReturnDate = DateTime.Parse(ReturnDate.Text);
                }
                
               
                if (DateTime.TryParse(ReturnDate.Text, out dateReturn) == IsReturn.IsChecked )
                {
                    reservation.IsReturned = bool.Parse(IsReturn.IsChecked.ToString());
                    bookstoreDBContext.Reservations.Update(reservation);
                    bookstoreDBContext.SaveChanges();
                    var book = bookstoreDBContext.Books.Where(b => b.Id == reservation.BookId).FirstOrDefault();
                    book.Quantity++;
                    bookstoreDBContext.Update(book);
                    bookstoreDBContext.SaveChanges();

                    MessageBox.Show("Changed successfully");
                }
                else
                {
                    MessageBox.Show("Check the data");
                }
                
               
            }
        }

    }
}
