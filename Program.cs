var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {
    var jasonPayload = new {message = "hello", content = "testing testing"};
    return Results.Ok(jasonPayload);
});
app.MapPost("/", (BorrowRequest requestBody) => {
    Console.WriteLine($"Message: {requestBody.Message}");
    Console.WriteLine($"Number: { requestBody.Number}");

    return Results.Accepted;
});
app.MapPut("/{articelID}", (int articelID) => {
    Console.WriteLine($"At dynmic segment: {articelID}");
    return Results.Accepted;
    
});
app.MapDelete("/", () => {
    return Results.Created();
});

app.Run();