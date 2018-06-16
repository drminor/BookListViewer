using BookData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

using BookListViewer.Models;
using System.Threading.Tasks;

using System.Threading;

namespace BookListViewer.ViewModels
{
    public class BookListVM : INotifyPropertyChanged
    {
        #region Private Properties

        private ObservableCollection<BookReadOnly> _catalog= new ObservableCollection<BookReadOnly>();

        private CancellationTokenSource _cancellationTS = null;

        #endregion

        public BookListVM GetBookListVM(List<BookRecDTO> lst)
        {
            return new BookListVM(lst);
        }

        #region Constructor

        public BookListVM() : this(new List<BookRecDTO>())
        {
        }

        public BookListVM(List<BookRecDTO> lst = null)
        {
            Catalog = ProcessBookList(lst);
            SelectedBook = Catalog[0];
        }

        public BookListVM(Task<List<BookRecDTO>> task, CancellationTokenSource cancellationTS)
        {
            _cancellationTS = cancellationTS;
            task.ContinueWith(ProcessBooks, _cancellationTS.Token);
        }

        private void ProcessBooks(Task<List<BookRecDTO>> task)
        {
            if(task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                List<BookRecDTO> lst = task.Result;

                Catalog = ProcessBookList(lst);
                SelectedBook = Catalog[0];
            }
        }

        private ObservableCollection<BookReadOnly> ProcessBookList(List<BookRecDTO> lst)
        {
            System.Diagnostics.Debug.WriteLine($"Processing Book List @: {System.DateTime.Now}. ");

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
                System.Diagnostics.Debug.WriteLine($"Reading from Catalog @: {System.DateTime.Now}.");
                return _catalog;
            }

            set
            {
                _catalog = value;
                OnPropertyChanged(nameof(Catalog));
            }
        }

        BookReadOnly _selectedBook;
        public BookReadOnly SelectedBook
        {
            get => _selectedBook;
            set
            {
                if(_selectedBook != value)
                {
                    _selectedBook = value;
                    OnPropertyChanged("SelectedBook");
                }
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
