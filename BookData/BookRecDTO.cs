using System;

namespace BookData
{
    public class BookRecDTO
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public Decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
    }

    public enum Genre
    {
        Computer,
        Fantasy,
        Romance,
        Horror
    }
}
