using Midterm1;

List<Book> AllBooks1 = new List<Book>()
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

List<Book> checkedOutBooks1 = new List<Book>();


bool library = true;
while (library)
{
    bool bookInStock1 = false;
    while (bookInStock1 == false)
    {
        bool interactionIsBorrow = Validator.Validator.GetContinue("Would you like to borrow or return a book?", "borrow", "return");
        if (interactionIsBorrow == true)
        {
            CheckOutBook(AllBooks1, checkedOutBooks1,ref bookInStock1);
        }
        else
        {
            ReturnBook(AllBooks1, checkedOutBooks1,ref bookInStock1);
        }
        library = Validator.Validator.GetContinue("Would you like to perform another action?");
    }
    
}
Console.WriteLine("Thanks for coming to the Grand Circus Library!  Enjoy your books.");



//Methods
static void CheckOutBook(List<Book> AllBooks, List<Book> checkedOutBooks, ref bool bookInStock)
{
    AllBooks.ForEach(b => Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-10}", b.Title, b.Author, b.Status)));
    Console.WriteLine("\nWhat book would you like to check out?\n");
    string choice = "";
    choice = Console.ReadLine();

    //Loop over all books currently checked into the library 
    for (int i = 0; i < AllBooks.Count; i++)
    {
        if ((AllBooks[i].Title == choice || AllBooks[i].Author == choice) && AllBooks[i].Status == "On Shelf")
        {
            Console.WriteLine($"You checked out {AllBooks[i].Title} by {AllBooks[i].Author}.");
            Book notAvailableBook = new Book(AllBooks[i].Title, AllBooks[i].Author, "Checked Out", DateTime.Now.AddDays(14));
            checkedOutBooks.Add(notAvailableBook);
            AllBooks[i].Status = "Checked Out";
            Console.WriteLine($"Please return this book by {notAvailableBook.ReturnDate}");
            bookInStock = true;
            break;
        }
        else if ((AllBooks[i].Title == choice || AllBooks[i].Author == choice) && AllBooks[i].Status == "Checked Out")
        {
            Console.WriteLine($"This book is currently checked out.");
            bookInStock = true;
            break;
        }
        else
        {
            bookInStock = false;
        }
    }
        
    if (bookInStock == false)
    {
        Console.WriteLine("We do not have that book.\n");
    }

}

static void ReturnBook(List<Book> AllBooks, List<Book> checkedOutBooks,ref bool bookInStock)
{
    checkedOutBooks.ForEach(b => Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-10}", b.Title, b.Author, b.Status)));
    Console.WriteLine("\nWhat book would you like to return?\n");
    string choice = "";
    choice = Console.ReadLine();
    for (int i = 0; i < checkedOutBooks.Count; i++)
    {
        if ((checkedOutBooks[i].Title == choice || checkedOutBooks[i].Author == choice) && checkedOutBooks[i].Status == "Checked Out")
        {
            Console.WriteLine($"You returned {checkedOutBooks[i].Title} by {checkedOutBooks[i].Author}.");
            //Book AvailableBook = new Book(checkedOutBooks[i].Title, checkedOutBooks[i].Author, "On Shelf");
            var returnedBook = AllBooks.Where(x => x.Title ==choice || x.Author ==choice).ToList();
            returnedBook.ForEach(y => y.Status = "On Shelf");
            checkedOutBooks.RemoveAt(i);
            bookInStock = true;
            break;
        }
        else
        {
            bookInStock = false;
        }
    }
    if (bookInStock == false)
    {
        Console.WriteLine("That is not our book.\n");
    }

}