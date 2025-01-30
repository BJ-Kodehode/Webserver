var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

RentingService rentingService = new RentingService();


app.MapGet("/", () => {
    var bookInventory = rentingService.ListAllBooks();
    var bookList = bookInventory.Select(inventoryEntry => inventoryEntry.Key);

    return Results.Ok(bookList);
});

app.MapPost("/borrow", (BorrowRequest borrowRequest) => {
    BorrowReciept? reciept= rentingService.BorrowBook(borrowRequest.bookTitle);

    if( reciept== null){
    return Results.BadRequest("not avaliale");
    } else{
        return Results.Accepted($"borroed book: {reciept.BookTitle}");

    }

});

app.Run();