
class RentingService
{
  private Dictionary<Book, int> bookInventory;
  private Dictionary<Book, int> currentlyBorrowed;

  public RentingService()
  {
    //lage et sett med bøker
    Book martian = new Book("martian", "Jim");
    Book foundation = new Book("foundation", "Jack");

    //Opprette data strukturen
    bookInventory = new Dictionary<Book, int>();
    currentlyBorrowed = new Dictionary<Book, int>();

    //legge til bøker med antall
    bookInventory.Add(martian, 10);
    currentlyBorrowed.Add(martian, 0);
    bookInventory.Add(foundation, 2);
    currentlyBorrowed.Add(foundation, 0);
  }

  public Dictionary<Book, int> ListAllBooks()
  {
    return bookInventory;
  }
public BorrowReciept? BorrowBook(string title){
    //sjekke om vi har book med tittelen
    var inventoryEntry = bookInventory
    .Where(entry => entry.Key.Title == title) //finner alle elementer som matcher
    .FirstOrDefault(); //ta lle føre elementene
    Book book = inventoryEntry.Key;
    int inventoryAmount = inventoryEntry.Value;
    if ( inventoryEntry.Key == null) {
        return null; 
    }
    //sjekke om via minst ett eksemplar tilgjengelig
    int borrowedAmound = currentlyBorrowed[book];
    bool isAvailable = inventoryAmount - borrowedAmound >= 1;

    //hvis ikke retun ingenting (null)
    if(!isAvailable) {
        return null;
    }


    //hvis vi har et ekemplar tilgjennglig
    // lage en ny kvittering
    BorrowReciept reciept = new BorrowReciept(book.Title);
    //oppdatere antall utlånte
    currentlyBorrowed[book]++;
    //rertun kvittering 
    return reciept;
}
    public ReturnReciept ReturnBook(string title){
        //throw new NotImplementedException();
        // Find the book in the inventory
        var inventoryEntry = bookInventory
            .Where(entry => entry.Key.Title == title)
            .FirstOrDefault();

        if (inventoryEntry.Key == null)
        {
            throw new InvalidOperationException("Book not found in inventory.");
        }

        Book book = inventoryEntry.Key;

        // Check if the book is currently borrowed
        if (!currentlyBorrowed.ContainsKey(book) || currentlyBorrowed[book] == 0)
        {
            throw new InvalidOperationException("This book is not currently borrowed.");
        }

        // Decrease the number of borrowed copies
        currentlyBorrowed[book]--;

        // Create and return a return receipt
        return new ReturnReciept(book.Title);
    }
}

class Book
{
  public string Title;
  public string Author;

  public Book(string title, string author)
  {
    Title = title;
    Author = author;
  }
}

class BorrowReciept
{
   public DateTime BorrowingDate;
    public DateTime DueDate;
    public string BookTitle;

   public BorrowReciept(string bookTitle){
        BookTitle = bookTitle;
        BorrowingDate = DateTime.Today;
        DueDate = DateTime.Today.AddDays(30);
    }
}

class ReturnReciept
{
public DateTime BorrowingDate { get; }
    public DateTime DueDate { get; }
    public string BookTitle { get; }

    public ReturnReciept(string bookTitle)
    {
        BookTitle = bookTitle;
        BorrowingDate = DateTime.Today;
        DueDate = DateTime.Today.AddDays(30);
    }
}
