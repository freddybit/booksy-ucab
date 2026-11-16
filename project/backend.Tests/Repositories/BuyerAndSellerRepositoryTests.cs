using backend.models;
using backend.repositories;

namespace backend.Tests.Repositories;

public class BuyerRepositoryTests
{
    private readonly BuyerRepository _repository = BuyerRepository.Instance;

    [Fact]
    public void Instance_ReturnsSameObject()
    {
        Assert.Same(BuyerRepository.Instance, BuyerRepository.Instance);
    }

    [Fact]
    public void AddBuyer_MakesItRetrievableIgnoringEmailCase()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email));

        Assert.NotNull(_repository.ReturnBuyer(email.ToUpper()));
    }

    [Fact]
    public void ReturnBuyer_UnknownEmail_ReturnsNull()
    {
        Assert.Null(_repository.ReturnBuyer("no-existe@ucab.edu.ve"));
    }

    [Fact]
    public void LoginBuyer_CorrectCredentials_ReturnsBuyer()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email, "clave123"));

        var buyer = _repository.LoginBuyer(email, "clave123");

        Assert.NotNull(buyer);
        Assert.Equal(email, buyer!.Email);
    }

    [Fact]
    public void LoginBuyer_WrongPassword_ReturnsNull()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email, "clave123"));

        Assert.Null(_repository.LoginBuyer(email, "incorrecta"));
    }

    [Fact]
    public void LoginBuyer_PasswordIsCaseSensitive()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email, "Clave123"));

        Assert.Null(_repository.LoginBuyer(email, "clave123"));
    }

    [Fact]
    public void ExistsBuyer_MatchingEmail_ReturnsTrue()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email));

        Assert.True(_repository.ExistsBuyer("Email", email));
        Assert.False(_repository.ExistsBuyer("Email", "otro@ucab.edu.ve"));
        Assert.False(_repository.ExistsBuyer("Inexistente", email));
    }

    [Fact]
    public void RemoveBuyer_RemovesIt()
    {
        var email = Unique.Email();
        var buyer = Builders.MakeBuyer(email);
        _repository.AddBuyer(buyer);

        _repository.RemoveBuyer(buyer);

        Assert.Null(_repository.ReturnBuyer(email));
    }

    [Fact]
    public void AddPurchase_AddsToMatchingBuyerAndPersists()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email));
        var bookId = Unique.Id();

        _repository.AddPurchase(Builders.MakeBook(bookId), email);

        var buyer = _repository.ReturnBuyer(email)!;
        Assert.Contains(buyer.PurchaseHistory, p => p.Book.Id == bookId);
    }

    [Fact]
    public void AddPurchase_UnknownEmail_DoesNotThrow()
    {
        var exception = Record.Exception(() => _repository.AddPurchase(Builders.MakeBook(1), "no-existe@ucab.edu.ve"));

        Assert.Null(exception);
    }

    [Fact]
    public void RemovePurchaseBuyer_RemovesPurchaseFromBuyer()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email));
        var bookId = Unique.Id();
        _repository.AddPurchase(Builders.MakeBook(bookId), email);

        _repository.RemovePurchaseBuyer(bookId, email);

        Assert.DoesNotContain(_repository.ReturnBuyer(email)!.PurchaseHistory, p => p.Book.Id == bookId);
    }

    [Fact]
    public void GetBuyerIndexByEmail_KnownEmail_ReturnsPositiveIndex()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email));

        Assert.True(_repository.GetBuyerIndexByEmail(email.ToUpper()) > 0);
    }

    [Fact]
    public void GetBuyerIndexByEmail_UnknownEmail_ReturnsZero()
    {
        Assert.Equal(0, _repository.GetBuyerIndexByEmail("no-existe@ucab.edu.ve"));
    }

    [Fact]
    public void DeleteBuyerByEmail_ExistingBuyer_RemovesIt()
    {
        var email = Unique.Email();
        _repository.AddBuyer(Builders.MakeBuyer(email));

        _repository.DeleteBuyerByEmail(email);

        Assert.Null(_repository.ReturnBuyer(email));
    }

    [Fact]
    public void DeleteBuyerByEmail_UnknownEmail_Throws()
    {
        var exception = Assert.Throws<Exception>(() => _repository.DeleteBuyerByEmail("no-existe@ucab.edu.ve"));

        Assert.Equal("El comprador no existe en el sistema.", exception.Message);
    }
}

public class SellerRepositoryTests
{
    private readonly SellerRepository _repository = SellerRepository.Instance;

    [Fact]
    public void Instance_ReturnsSameObject()
    {
        Assert.Same(SellerRepository.Instance, SellerRepository.Instance);
    }

    [Fact]
    public void AddSeller_MakesItRetrievableByEmail()
    {
        var seller = Builders.MakeSeller();

        _repository.AddSeller(seller);

        Assert.Same(seller, _repository.ReturnSeller(seller.Email));
        Assert.Contains(_repository.ReturnSellers(), s => s.Email == seller.Email);
    }

    [Fact]
    public void ReturnSeller_UnknownEmail_ReturnsNull()
    {
        Assert.Null(_repository.ReturnSeller("no-existe@ucab.edu.ve"));
    }

    [Fact]
    public void RemoveSeller_RemovesMatchingSeller()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);

        _repository.RemoveSeller(seller);

        Assert.Null(_repository.ReturnSeller(seller.Email));
    }

    [Fact]
    public void RemoveSeller_Null_DoesNotThrow()
    {
        var exception = Record.Exception(() => _repository.RemoveSeller(null));

        Assert.Null(exception);
    }

    [Fact]
    public void RemoveSellerByEmail_IgnoresCase()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);

        _repository.RemoveSellerByEmail(seller.Email.ToUpper());

        Assert.Null(_repository.ReturnSeller(seller.Email));
    }

    [Fact]
    public void UpdateSeller_ExistingSeller_ReplacesData()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);
        var updated = Builders.MakeSeller(seller.Email);
        updated.BankName = "Provincial";

        _repository.UpdateSeller(updated);

        Assert.Equal("Provincial", _repository.ReturnSeller(seller.Email)!.BankName);
    }

    [Fact]
    public void UpdateSeller_NewSeller_AddsIt()
    {
        var seller = Builders.MakeSeller();

        _repository.UpdateSeller(seller);

        Assert.NotNull(_repository.ReturnSeller(seller.Email));
    }

    [Fact]
    public void ExistsSeller_ChecksPropertyValue()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);

        Assert.True(_repository.ExistsSeller("Email", seller.Email));
        Assert.False(_repository.ExistsSeller("Email", "otro@ucab.edu.ve"));
        Assert.False(_repository.ExistsSeller("Inexistente", seller.Email));
    }

    [Fact]
    public void AddBookBySeller_AddsBookToSellerCatalog()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);
        var bookId = Unique.Id();

        _repository.AddBookBySeller(Builders.MakeBook(bookId), seller.Email);

        Assert.Contains(_repository.ReturnSeller(seller.Email)!.Catalog, b => b.Id == bookId);
    }

    [Fact]
    public void RemoveBooksBySeller_RemovesBookFromCatalog()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);
        var bookId = Unique.Id();
        _repository.AddBookBySeller(Builders.MakeBook(bookId), seller.Email);

        _repository.RemoveBooksBySeller(bookId, seller.Email);

        Assert.DoesNotContain(_repository.ReturnSeller(seller.Email)!.Catalog, b => b.Id == bookId);
    }

    [Fact]
    public void ReturnSellerByBookId_FindsOwnerOfBook()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);
        var bookId = Unique.Id();
        _repository.AddBookBySeller(Builders.MakeBook(bookId), seller.Email);

        Assert.Equal(seller.Email, _repository.ReturnSellerByBookId(bookId)!.Email);
        Assert.Null(_repository.ReturnSellerByBookId(-5));
    }

    [Fact]
    public void AddSaleToSeller_AppendsPurchaseToSalesHistory()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);
        var bookId = Unique.Id();

        _repository.AddSaleToSeller(new BookPurchase(Builders.MakeBook(bookId), DateTime.Now), seller.Email);

        Assert.Contains(_repository.ReturnSeller(seller.Email)!.SalesHistory, p => p.Book.Id == bookId);
    }

    [Fact]
    public void RemoveSaleFromSeller_RemovesPurchaseFromSalesHistory()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);
        var bookId = Unique.Id();
        _repository.AddSaleToSeller(new BookPurchase(Builders.MakeBook(bookId), DateTime.Now), seller.Email);

        _repository.RemoveSaleFromSeller(bookId, seller.Email);

        Assert.DoesNotContain(_repository.ReturnSeller(seller.Email)!.SalesHistory, p => p.Book.Id == bookId);
    }

    [Fact]
    public void GetSellerIndexByEmail_KnownAndUnknownEmail()
    {
        var seller = Builders.MakeSeller();
        _repository.AddSeller(seller);

        Assert.True(_repository.GetSellerIndexByEmail(seller.Email.ToUpper()) > 0);
        Assert.Equal(0, _repository.GetSellerIndexByEmail("no-existe@ucab.edu.ve"));
    }
}
