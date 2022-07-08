using Midterm1;

List<Book> AllBooks = new List<Book>()
{
    new Book("Ready Player One", "Ernest Cline", "On Shelf"),
    new Book("1984", "George Orwell", "On Shelf"),
    new Book("Starship Troopers", "Robert A. Heinlein", "On Shelf"),
    new Book("2001: A Space Odyssey", "Arthur C. Clarke", "On Shelf"),
    new Book("Twilight", "Stephenie Meyer", "On Shelf"),
    new Book("The Hunger Games", "Suzanne Collins", "On Shelf"),
    new Book("Astrophysics for People in a Hurry", "Neil deGrasse Tyson", "On Shelf"),
    new Book("Scott Pilgrim Vol. 1-6", "Bryan Lee O'Malley", "On Shelf"),
    new Book("One Piece Vol. 1-102", "Eiichiro Oda", "On Shelf"),
    new Book("The Hobbit", "J.R.R. Tolkien", "On Shelf"),
    new Book("Ender's Game", "Orson Scott Card", "On Shelf"),
    new Book("The Great Gatsby", "F. Scott Fitzgerald", "On Shelf"),
};

////Test for DateTime
//DateTime testDate = DateTime.Now;
//Console.WriteLine(testDate);
//DateTime returnDate = testDate.AddDays(14);
//Console.WriteLine(returnDate);

//Display Book List
AllBooks.ForEach(b => Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-10}", b.Title, b.Author, b.Status)));




//Book bookToCheckOut = AllBooks.Where(b => b.Title == choice);
//Console.WriteLine(bookToCheckOut);

List<Book> checkedOutBooks = new List<Book>()
{

};

bool bookInStock = false;
while (bookInStock == false)
{
    Console.WriteLine("What book would you like to check out?");
    string choice = "";
    choice = Console.ReadLine();

    for (int i = 0; i < AllBooks.Count; i++)
    {
        if ((AllBooks[i].Title == choice || AllBooks[i].Author == choice) && AllBooks[i].Status == "On Shelf")
        {
            Console.WriteLine($"You checked out {AllBooks[i].Title} by {AllBooks[i].Author}.");
            checkedOutBooks.Add(AllBooks[i]);
            AllBooks.RemoveAt(i);

            Book notAvailableBook = AllBooks[i.];

            bookInStock = true;
            break;
        }
        else
        {
            bookInStock = false;
        }
    }
    if(bookInStock == false)
    {
        Console.WriteLine("We do not have that book.");
    }
    
}












