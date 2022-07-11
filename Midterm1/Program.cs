using Midterm1;
using System.Globalization;
//File for importing the library
string filePath = "../../../LibraryBackUp.txt";

//Initialize list of books in library
List<Book> AllBooks1 = new List<Book>();

StreamReader reader = new StreamReader(filePath);
//read in the library.
if (File.Exists(filePath))
{
    while (true)
    {
        string line = reader.ReadLine();
        if (line == null)
        {
            break;
        }
        else
        {
            //take line and turn into book
            string[] AllBooksArray = line.Split(',');
            Book newBook = new Book(AllBooksArray[0], AllBooksArray[1], AllBooksArray[2]);
            AllBooks1.Add(newBook);
        }
    }
    reader.Close();
}
else
{
    //Initial library for testing
    AllBooks1 = new List<Book>()
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
}

//MAIN PROGRAM
bool library = true;
while (library)
{
    bool bookInStock1 = false;
    while (bookInStock1 == false)
    {
        bool interactionIsBorrow = Validator.Validator.GetContinue("Would you like to borrow or return a book?", "borrow", "return");
        if (interactionIsBorrow == true)
        {
            CheckOutBook(ref AllBooks1, ref bookInStock1);
        }
        else
        {
            ReturnBook(ref AllBooks1, ref bookInStock1);
        }
        library = Validator.Validator.GetContinue("Would you like to perform another action?");
    }

}
Console.WriteLine("Thanks for coming to the Grand Circus Library!  Enjoy your books.");


//UPDATE FILE WITH NEW LIBRARY LIST
StreamWriter writer = new StreamWriter(filePath, false);
foreach (Book b in AllBooks1)
{
    writer.WriteLine($"{b.Title},{b.Author},{b.Status},{b.ReturnDate}");
}
writer.Close();


//Methods
//Checks to see if search term is unique, if so, checks out single book. Else, loops with request for better search
static void CheckOutBook(ref List<Book> AllBooks, ref bool bookInStock)
{
    while (bookInStock == false)
    {
        //Displays Book List
        AllBooks.ForEach(b => Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-10}", b.Title, b.Author, b.Status)));

        //Prompts for search term
        Console.WriteLine("\nWhat book would you like to check out?\n");
        string choice = "";
        choice = Console.ReadLine().Trim().ToLower();

        //check if multiples that fit the "Contains" method
        List<Book> multipleBooks = checkForMultiples(ref AllBooks, choice);
        if (multipleBooks.Count() <= 1)
        {
            for (int i = 0; i < AllBooks.Count; i++)
            {
                if ((AllBooks[i].Title.ToLower().Contains(choice) || AllBooks[i].Author.ToLower().Contains(choice)) && AllBooks[i].Status == "On Shelf")
                {
                    //If on shelf, gives return date
                    Console.WriteLine($"You checked out {AllBooks[i].Title} by {AllBooks[i].Author}.");
                    Book notAvailableBook = new Book(AllBooks[i].Title, AllBooks[i].Author, "Checked Out", DateTime.Now.AddDays(14));
                    AllBooks[i].Status = "Checked Out";
                    Console.WriteLine($"Please return this book by {notAvailableBook.ReturnDate}\n");
                    bookInStock = true;
                    break;
                }
                else if ((AllBooks[i].Title.ToLower().Contains(choice) || AllBooks[i].Author.ToLower().Contains(choice)) && AllBooks[i].Status == "Checked Out")
                {
                    //If checked out, tells user
                    Console.WriteLine($"\nThis book is currently checked out.\n");
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
                //If not in library, tells user
                Console.WriteLine("\nWe do not have that book.\n");
                bookInStock = true;
            }
        }
        else
        {
            //Gives list of books that match search term. Loops to ask again.
            Console.WriteLine("\nThe books that matched your search are: ");
            multipleBooks.ForEach(x => Console.WriteLine($"{x.Title}"));
            Console.WriteLine("Please refine your search.\n");
        }
    }
}

static void ReturnBook(ref List<Book> AllBooks, ref bool bookInStock)
{
    while (bookInStock == false)
    {
        AllBooks.ForEach(b => Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-10}", b.Title, b.Author, b.Status)));
        Console.WriteLine("\nWhat book would you like to return?\n");
        string choice = "";
        choice = Console.ReadLine();
        //check if multiples that fit the "Contains" method
        List<Book> multipleBooks = checkForMultiples(ref AllBooks, choice);
        if (multipleBooks.Count() <= 1)
        {
            for (int i = 0; i < AllBooks.Count; i++)
            {
                if ((AllBooks[i].Title.ToLower().Contains(choice) || AllBooks[i].Author.ToLower().Contains(choice)) && AllBooks[i].Status == "Checked Out")
                {
                    Console.WriteLine($"\nYou returned {AllBooks[i].Title} by {AllBooks[i].Author}.");
                    //Book AvailableBook = new Book(checkedOutBooks[i].Title, checkedOutBooks[i].Author, "On Shelf");
                    List<Book> returnedBook = AllBooks.Where(x => x.Title.ToLower().Contains(choice) || x.Author.ToLower().Contains(choice)).ToList();
                    returnedBook.ForEach(y => y.Status = "On Shelf");
                    bookInStock = true;
                    break;
                }
                else if ((AllBooks[i].Title.ToLower().Contains(choice) || AllBooks[i].Author.ToLower().Contains(choice)) && AllBooks[i].Status == "On Shelf")
                {
                    //If checked out, tells user
                    Console.WriteLine($"\nThis book is currently available to be checked out.\n");
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
                Console.WriteLine("\nThat is not our book.\n");
            }
        }
        else
        {
            Console.WriteLine("\nThe books that matched your search are: ");
            multipleBooks.ForEach(x => Console.WriteLine($"{x.Title}"));
            Console.WriteLine("Please refine your search.\n");
        }
    }
}

static List<Book> checkForMultiples(ref List<Book> AllBooks, string choice)
{
    List<Book> toReturn = new List<Book>();
    for (int i = 0; i < AllBooks.Count; i++)
    {
        if ((AllBooks[i].Title.ToLower().Contains(choice) || AllBooks[i].Author.ToLower().Contains(choice)))
        {
            toReturn.Add(AllBooks[i]);
        }
    }
    return toReturn;
}