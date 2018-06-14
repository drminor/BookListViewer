using BookData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BookListViewer.ViewModels
{
    public class BookListVM : INotifyPropertyChanged
    {
        private ObservableCollection<BookRec> _books = new ObservableCollection<BookRec>();

        public event PropertyChangedEventHandler PropertyChanged;

        public BookListVM(List<BookRec> lst)
        {
            ObservableCollection<BookRec> temp = new ObservableCollection<BookRec>();

            foreach(BookRec br in lst)
            {
                temp.Add(br);
            }

            Books = temp;
        }

        public ObservableCollection<BookRec> Books
        {
            get
            {
                return _books;
            }

            set
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
