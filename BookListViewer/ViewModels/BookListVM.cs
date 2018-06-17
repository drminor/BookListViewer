using BookData;
using BookListViewer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace BookListViewer.ViewModels
{
    public class BookListVM : INotifyPropertyChanged
    {
        #region Private Properties

        private CancellationTokenSource _cancellationTS;
        private ObservableCollection<BookReadOnly> _catalog= new ObservableCollection<BookReadOnly>();

        #endregion

        #region Constructor

        public BookListVM() : this(new List<BookRecDTO>())
        {
        }

        public BookListVM(List<BookRecDTO> lst)
        {
            _cancellationTS = null;

            Catalog = ProcessBookList(lst);
            SelectedBook = Catalog[0];
        }

        public BookListVM(Task<List<BookRecDTO>> task, CancellationTokenSource cancellationTS)
        {
            _cancellationTS = cancellationTS;

            // TODO: Find out what the cancellationToken argument is used for.
            task.ContinueWith(ProcessBooks, cancellationTS.Token);
        }

        private void ProcessBooks(Task<List<BookRecDTO>> task)
        {
            if(task.IsCompleted)
            {
                if(task.IsFaulted)
                {
                    string errMessage = $"An error occured while loading the book data." +
                        $" The error message is {task.Exception.Message}.";

                    throw new InvalidOperationException(errMessage, task.Exception);
                }

                if (!task.IsCanceled)
                {
                    List<BookRecDTO> lst = task.Result;

                    Catalog = ProcessBookList(lst);
                    SelectedBook = Catalog[0];
                }
            }
        }

        private ObservableCollection<BookReadOnly> ProcessBookList(List<BookRecDTO> lst)
        {
            //System.Diagnostics.Debug.WriteLine($"Processing Book List @: {System.DateTime.Now}. ");

            ObservableCollection<BookReadOnly> result = new ObservableCollection<BookReadOnly>();

            if(lst != null)
            {
                foreach (BookRecDTO br in lst)
                {
                    if(_cancellationTS?.IsCancellationRequested == true)
                    {
                        return null;
                    }

                    BookReadOnly bookReadOnly = new BookReadOnly(br);
                    result.Add(bookReadOnly);
                }
            }

            return result;
        }

        #endregion

        #region Public Methods

        public void CancelDataLoading()
        {
            if (_cancellationTS != null) _cancellationTS.Cancel();
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
