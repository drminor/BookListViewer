using System;

namespace BookData
{
    //public class BookTitlePlusId
    //{

    //}

    public class BookRec
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public BookGenre BookGenre { get; set; }
        public Decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
    }

    public enum BookGenre
    {
        Computer,
        Fantasy,
        Romance,
        Horror
    }
}
