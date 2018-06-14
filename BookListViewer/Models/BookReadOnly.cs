using BookData;
using System;
using System.ComponentModel;

namespace BookListViewer.Models
{
    public class BookReadOnly : INotifyPropertyChanged
    {
        public int Id { get; }
        public string Author { get; }
        public string Title { get; }
        public string Genre { get; }
        public Decimal Price { get; }
        public DateTime PublishDate { get; }
        public string Description { get; }

        // We must implement the INotifyPropertyChanged to avoid memory leaks with WPF,
        // however since this is a readonly implementation, the event is never raised.
#pragma warning disable 67, CS0535
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67, CS0535


        public BookReadOnly(BookRecDTO bookRec)
        {
            Id = bookRec.Id;
            Author = bookRec.Author;
            Title = bookRec.Title;
            Genre = bookRec.Genre;
            Price = bookRec.Price;
            PublishDate = bookRec.PublishDate;
            Description = bookRec.Description;
        }
    }

}
