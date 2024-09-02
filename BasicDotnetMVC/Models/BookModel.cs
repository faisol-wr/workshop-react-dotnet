namespace BasicDotnetMVC.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }

        public List<BookModel> List(int countLoop)
        {
            List<BookModel> list = new List<BookModel>();

            for (int i = 0; i < countLoop; i++)
            {
                list.Add(new BookModel()
                {
                    Id = i,
                    Isbn = "" + i + i + i,
                    Name = "Book Number " + i,
                    Price = (i + 10) * (i + 10)
                });
            }

            return list;
        }
    }
}