using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infrastructure.DataAccess;
using TechLibrary.Infrastructure.Services.LoggedUser;

namespace TechLibrary.Application.UseCases.Checkouts;
public class RegisterBookCheckoutUseCase
{
    private const int MAX_LOAN_DAYS = 7;
    private readonly LoggedUserService _loggedUser;
    public RegisterBookCheckoutUseCase(LoggedUserService loggedUser)
    {
        _loggedUser = loggedUser;
    }
    public void Execute(Guid bookId)
    {
        var dbContext = new TechLibraryDbContext();

        Validate(dbContext, bookId);

        var user = _loggedUser.User(dbContext);

        var entity = new Checkout
        {
            UserId = user.Id,
            BookId = bookId,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
        };

        dbContext.Checkouts.Add(entity);
        dbContext.SaveChanges();
    }
    private void Validate(TechLibraryDbContext dbContext, Guid bookId)
    {
        var book = dbContext.Books.FirstOrDefault(book => book.Id == bookId);
        if (book is null)
            throw new NotFoundException("Book not found.");

        var amountBookNotReturned = dbContext
            .Checkouts.Count(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);

        if (amountBookNotReturned == book.Amount)
            throw new ConflictException("The book is not available for loan.");
    }
}
