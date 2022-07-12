using Midterm1;
using System.Globalization;
//File for importing the libraries
string filePath = "../../../LibraryBackUp.txt";
string filePath2 = "../../../Alexandria.txt";

//Initialize list of books in library
List<Book> AllBooks1 = new List<Book>();

//read in the original library.
if (File.Exists(filePath))
{
    StreamReader reader = new StreamReader(filePath);

    while (true)
    {
        //checks to see if each line contains characters or not
        string line = reader.ReadLine();
        if (line == null)
        {
            break;
        }
        //if line isn't null, reader reads the line and splits at every comma into a string array
        else
        {
            try
            {
                string[] AllBooksArray = line.Split(',');
                Book newBook = new Book(AllBooksArray[0], AllBooksArray[1], AllBooksArray[2], AllBooksArray[3]);
                AllBooks1.Add(newBook);
            }
            catch (Exception)
            {
                string[] AllBooksArray = line.Split(',');
                Book newBook = new Book(AllBooksArray[0], AllBooksArray[1], AllBooksArray[2]);
                AllBooks1.Add(newBook);
            }
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



List<Book> oldBooks = new List<Book>();
//read in the OLD library.
if (File.Exists(filePath2))
{
    StreamReader reader2 = new StreamReader(filePath2);
    while (true)
    {
        string line = reader2.ReadLine();
        if (line == null)
        {
            break;
        }
        else
        {
            //take line and turn into book
            try
            {
                string[] AllBooksArray = line.Split(',');
                Book newBook = new Book(AllBooksArray[0], AllBooksArray[1], AllBooksArray[2], AllBooksArray[3]);
                oldBooks.Add(newBook);
            }
            catch (Exception)
            {
                string[] AllBooksArray = line.Split(',');
                Book newBook = new Book(AllBooksArray[0], AllBooksArray[1], AllBooksArray[2]);
                oldBooks.Add(newBook);
            }
            
        }
    }
    reader2.Close();
}
else
{
    oldBooks = new List<Book>()
    {
    new Book("How to Die of Sepsis", "Sepsis victim #12", "Burning"),
    new Book("Building Pyramids With Aliens 101", "Baskakeren III", "Burning"),
    new Book("Praying to Cats", "Pharaoh Hatshepsut", "Burning"),
    new Book("How to Train Your Dragon", "Pixar 2012", "Burning"),
    new Book("Irrigating the Nile River Valley", "King Ptolemy I Soter", "Burning"),
    new Book("New World Order: Starting a Modern Cult", "The Illuminati", "Burning"),
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

//Beginning of the burning of the library
bool bookInStock2 = false;
BurnTheLibrary(ref oldBooks, ref bookInStock2);
//creates a new list out of oldBooks where status equals checked out
List<Book> savedBooks = oldBooks.Where(x => x.Status == "Checked Out").ToList();
//only runs if there are books in the savedBooks list
if(savedBooks.Count > 0)
{
    for (int i = savedBooks.Count-1; i >= 0 ; i--)
    {
        savedBooks[i].Status = "On Shelf";
        //adds to AllBooks form savedBooks
        AllBooks1.Add(savedBooks[i]);
        //because we just added element to the end of AllBooks, we can use .Last to remove it from savedBooks
        savedBooks.Remove(AllBooks1.Last());
        oldBooks.Remove(AllBooks1.Last());
    }
}

//UPDATE FILE WITH NEW LIBRARY LIST
StreamWriter writer = new StreamWriter(filePath, false);
foreach (Book b in AllBooks1)
{
    writer.WriteLine($"{b.Title},{b.Author},{b.Status},{b.ReturnDate}");
}
writer.Close();

//Update Alexandria Library
StreamWriter writer1 = new StreamWriter(filePath2, false);
foreach (Book b in oldBooks)
{
    writer1.WriteLine($"{b.Title},{b.Author},{b.Status}");
}
writer1.Close();


//Methods
//Checks to see if search term is unique, if so, checks out single book. Else, loops with request for better search
static void CheckOutBook(ref List<Book> AllBooks, ref bool bookInStock)
{
    while (bookInStock == false)
    {
        //Displays Book List
        AllBooks.ForEach(b => {
            if(b.Status.ToLower() == "burning")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-10}", b.Title, b.Author, b.Status));
            Console.ResetColor();
            });

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
                    //uses the overloaded constructor to give the book a return date
                    //Book notAvailableBook = new Book(AllBooks[i].Title, AllBooks[i].Author, "Checked Out", DateTime.Now.AddDays(14).ToString());
                    //updates on shelf to checked out
                    AllBooks[i].Status = "Checked Out";
                    AllBooks[i].ReturnDate = DateTime.Now.AddDays(14).ToString();
                    Console.WriteLine($"Please return this book by {AllBooks[i].ReturnDate}\n");
                    bookInStock = true;
                    break;
                }
                else if ((AllBooks[i].Title.ToLower().Contains(choice) || AllBooks[i].Author.ToLower().Contains(choice)) && AllBooks[i].Status == "Burning")
                {
                    //If on shelf, gives return date
                    Console.WriteLine($"You saved {AllBooks[i].Title} by {AllBooks[i].Author} from the flames.");
                    //uses the overloaded constructor to give the book a return date
                    //Book notAvailableBook = new Book(AllBooks[i].Title, AllBooks[i].Author, "Checked Out", DateTime.Now.AddDays(14).ToString());
                    //updates on shelf to checked out
                    AllBooks[i].Status = "Checked Out";
                    AllBooks[i].ReturnDate = DateTime.Now.AddDays(14).ToString();
                    //Console.WriteLine($"Please return this book by {AllBooks[i].ReturnDate}\n");
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
                    //runs if the user input doesn't match any books
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
                    //updates checked out to on shelf
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
                    //if user input doesn't match book
                    bookInStock = false;
                }
            }
            if (bookInStock == false)
            {
                //if user input doesn't match book
                Console.WriteLine("\nThat is not our book.\n");
            }
        }
        else
        {
            //if user search entry returns multiple books
            Console.WriteLine("\nThe books that matched your search are: ");
            multipleBooks.ForEach(x => Console.WriteLine($"{x.Title}"));
            Console.WriteLine("Please refine your search.\n");
        }
    }
}

//returns a list of books that match search term
static List<Book> checkForMultiples(ref List<Book> AllBooks, string choice)
{
    List<Book> toReturn = new List<Book>();
    for (int i = 0; i < AllBooks.Count; i++)
    {
        //checks to see if each book in AllBooks matches choice, if so it gets added to list
        if ((AllBooks[i].Title.ToLower().Contains(choice) || AllBooks[i].Author.ToLower().Contains(choice)))
        {
            toReturn.Add(AllBooks[i]);
        }
    }
    return toReturn;
}

//the burner
static void BurnTheLibrary(ref List<Book> AllBooks, ref bool bookInStock)
{
    bool isLeft;
    string x = "";
    Console.WriteLine("You go to leave the library and you see two doors.\nThe one you entered through is on the right.\nThe door on the left is unmarked.");
    isLeft = Validator.Validator.GetContinue("Which door would you like to go through?", "left", "right");
    if (isLeft == true)
    {
        Console.WriteLine("\nYou cautiously open the door on the left.\nIn the blink of an eye you are suddenly transported back in time to the year 48 B.C.\nPress any key to continue...");
        Console.ReadKey();
        Console.WriteLine("\nYou find it hard to breathe, and see the room filling with smoke.\npress any key to continue...");
        Console.ReadKey();
        Console.WriteLine("\n You look around and see hundreds of men fighting.\npress any key to continue...");
        Console.ReadKey();
        Console.WriteLine("\n You see a lot of books being destroyed, and feel the overwhelming urge to save some of them.\n press any key to continue...");
        Console.ReadKey();


        bool runProgram2 = true;
        while (runProgram2)
        {
            Console.WriteLine("What books do you save?");
            bookInStock = false;
            //references the CheckOutBook method instead of rewriting code
            CheckOutBook(ref AllBooks, ref bookInStock);
            //validator to allow user to save multiple books
            runProgram2 = Validator.Validator.GetContinue("Would you like to save another book?");
        }

        Console.WriteLine("You quickly step back through the door you came from, put your saved item(s) back on the library shelf, and leave.");



    }
    else
    {
        Console.WriteLine("\nYou walk out the front door and go about your day.");
    }
}
