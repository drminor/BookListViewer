using BookData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

using BookListViewer.Models;

namespace BookListViewer.ViewModels
{
    public class BookListVM : INotifyPropertyChanged
    {
        #region Private Properties

        private ObservableCollection<BookReadOnly> _catalog= new ObservableCollection<BookReadOnly>();

        #endregion

        #region Constructor

        public BookListVM(List<BookRecDTO> lst)
        {
            Catalog = ProcessBookList(lst);
        }

        private ObservableCollection<BookReadOnly> ProcessBookList(List<BookRecDTO> lst)
        {
            ObservableCollection<BookReadOnly> result = new ObservableCollection<BookReadOnly>();

            foreach (BookRecDTO br in lst)
            {
                BookReadOnly bookReadOnly = new BookReadOnly(br);
                result.Add(bookReadOnly);
            }

            return result;
        }

        #endregion

        #region Public Events and Properties

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BookReadOnly> Catalog
        {
            get
            {
                return _catalog;
            }

            set
            {
                _catalog = value;
                OnPropertyChanged(nameof(Catalog));
            }
        }

        #endregion

        #region Property Changed Helpers

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
