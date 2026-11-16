using backend.models;

namespace backend.Tests.Models;

public class SellerTests
{
    [Fact]
    public void DefaultConstructor_InitializesEmptyCollections()
    {
        var seller = new Seller();

        Assert.Equal("", seller.Email);
        Assert.Equal("", seller.BankName);
        Assert.Equal("", seller.PhoneNumber);
        Assert.Equal(0, seller.Id);
        Assert.Empty(seller.Catalog);
        Assert.Empty(seller.Ratings);
        Assert.Empty(seller.SalesHistory);
    }

    [Fact]
    public void ParameterizedConstructor_AssignsValues()
    {
        var catalog = new List<Book> { Builders.MakeBook(1) };
        var ratings = new List<float> { 4.5f };
        var sales = new List<BookPurchase>();

        var seller = new Seller("v@ucab.edu.ve", "Ana", "Perez", 30, "clave", "Banesco", 9, "0414", catalog, ratings, sales);

        Assert.Equal("v@ucab.edu.ve", seller.Email);
        Assert.Equal("Banesco", seller.BankName);
        Assert.Equal(9, seller.Id);
        Assert.Equal("0414", seller.PhoneNumber);
        Assert.Same(catalog, seller.Catalog);
        Assert.Same(ratings, seller.Ratings);
        Assert.Same(sales, seller.SalesHistory);
    }

    [Fact]
    public void AddBook_AppendsBookToCatalog()
    {
        var seller = new Seller();
        var book = Builders.MakeBook(1);

        seller.AddBook(book);

        Assert.Same(book, Assert.Single(seller.Catalog));
    }

    [Fact]
    public void RemoveBookById_ExistingBook_RemovesIt()
    {
        var seller = new Seller();
        seller.AddBook(Builders.MakeBook(1));
        seller.AddBook(Builders.MakeBook(2));

        seller.RemoveBookById(1);

        Assert.Equal(2, Assert.Single(seller.Catalog).Id);
    }

    [Fact]
    public void RemoveBookById_UnknownBook_LeavesCatalogUntouched()
    {
        var seller = new Seller();
        seller.AddBook(Builders.MakeBook(1));

        seller.RemoveBookById(50);

        Assert.Single(seller.Catalog);
    }

    [Fact]
    public void Setters_NullValue_Throw()
    {
        var seller = new Seller();

        Assert.Throws<ArgumentNullException>(() => seller.BankName = null!);
        Assert.Throws<ArgumentNullException>(() => seller.PhoneNumber = null!);
        Assert.Throws<ArgumentNullException>(() => seller.Catalog = null!);
        Assert.Throws<ArgumentNullException>(() => seller.Ratings = null!);
        Assert.Throws<ArgumentNullException>(() => seller.SalesHistory = null!);
    }

    [Fact]
    public void Setters_ValidValue_UpdateProperties()
    {
        var seller = new Seller();

        seller.BankName = "Mercantil";
        seller.PhoneNumber = "0412";
        seller.Id = 4;
        seller.Ratings = new List<float> { 5f, 3f };

        Assert.Equal("Mercantil", seller.BankName);
        Assert.Equal("0412", seller.PhoneNumber);
        Assert.Equal(4, seller.Id);
        Assert.Equal(2, seller.Ratings.Count);
    }
}
