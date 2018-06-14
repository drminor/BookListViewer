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
        public BookGenre BookGenre { get; }
        public Decimal Price { get; }
        public DateTime PublishDate { get; }
        public string Description { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public BookReadOnly(BookRec bookRec)
        {
            Id = bookRec.Id;
            Author = bookRec.Author;
            Title = bookRec.Title;
            BookGenre = bookRec.BookGenre;
            Price = bookRec.Price;
            PublishDate = bookRec.PublishDate;
            Description = bookRec.Description;
        }
    }

}
