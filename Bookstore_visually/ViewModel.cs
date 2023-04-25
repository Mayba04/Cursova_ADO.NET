using System.Collections.Generic;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace Bookstore_visually
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        private ObservableCollection<object> CDGBook;
        private ObservableCollection<object> CDGSBookTitle;
        private ObservableCollection<object> CDGSBookAuthor;
        private ObservableCollection<object> CDGSBookGenre;
        private ObservableCollection<object> CDGNewBooks;
        private ObservableCollection<object> CDGMostSold;
        private ObservableCollection<object> CDGMPAuthor;
        private ObservableCollection<object> CDGMPGenre;


        public ViewModel()
        {
            CDGBook = new ObservableCollection<object>();
            CDGSBookTitle = new ObservableCollection<object>();
            CDGSBookAuthor = new ObservableCollection<object>();
            CDGSBookGenre = new ObservableCollection<object>();
            CDGNewBooks = new ObservableCollection<object>();
            CDGMostSold = new ObservableCollection<object>();
            CDGMPAuthor = new ObservableCollection<object>();
            CDGMPGenre = new ObservableCollection<object>();
        }

        public IEnumerable<object> ClientDataGridDatabook => CDGBook;
        public IEnumerable<object> CDGDSBookTitle => CDGSBookTitle;
        public IEnumerable<object> CDGDSBookAuthor => CDGSBookAuthor;
        public IEnumerable<object> CDGDSBookGenre => CDGSBookGenre;
        public IEnumerable<object> CDGDNewBooks => CDGNewBooks;
        public IEnumerable<object> CDGDMostSold => CDGMostSold;
        public IEnumerable<object> CDGDMPAuthor => CDGMPAuthor;
        public IEnumerable<object> CDGDMPGenre => CDGMPGenre;

        public void AddCDGBook(List<object> list)
        {
            if (CDGBook.Count > 0) CDGBook.Clear();
            foreach (var item in list)
            {
                CDGBook.Add(item);
            }
        }

        public void AddCDGSBookTitle(List<object> list)
        {
            if (CDGSBookTitle.Count > 0) CDGSBookTitle.Clear();
            foreach (var item in list)
            {
                CDGSBookTitle.Add(item);
            }
        }

        public void AddCDGSBookAuthor(List<object> list)
        {
            if (CDGSBookAuthor.Count > 0) CDGSBookAuthor.Clear();
            foreach (var item in list)
            {
                CDGSBookAuthor.Add(item);
            }
        }

        public void AddCDGSBookGenre(List<object> list)
        {
            if (CDGSBookGenre.Count > 0) CDGSBookGenre.Clear();
            foreach (var item in list)
            {
                CDGSBookGenre.Add(item);
            }
        }

        public void AddCDGNewBook(List<object> list)
        {
            if (CDGNewBooks.Count > 0) CDGNewBooks.Clear();
            foreach (var item in list)
            {
                CDGNewBooks.Add(item);
            }
        }

        public void AddCDGMostSold(List<object> list)
        {
            if (CDGMostSold.Count > 0) CDGMostSold.Clear();
            foreach (var item in list)
            {
                CDGMostSold.Add(item);
            }
        }

        public void AddCDGMPAuthor(List<object> list)
        {
            if (CDGMPAuthor.Count > 0) CDGMPAuthor.Clear();
            foreach (var item in list)
            {
                CDGMPAuthor.Add(item);
            }
        }

        public void AddCDGMPGenre(List<object> list)
        {
            if (CDGMPGenre.Count > 0) CDGMPGenre.Clear();
            foreach (var item in list)
            {
                CDGMPGenre.Add(item);
            }
        }

    }


    
}
