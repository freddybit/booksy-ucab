using backend.models;

namespace backend.Tests.Models;

public class BuyerTests
{
    [Fact]
    public void NewBuyer_HasEmptyPurchaseHistory()
    {
        var buyer = Builders.MakeBuyer();

        Assert.NotNull(buyer.PurchaseHistory);
        Assert.Empty(buyer.PurchaseHistory);
    }

    [Fact]
    public void AddPurchase_AppendsPurchaseWithBookAndCurrentDate()
    {
        var buyer = Builders.MakeBuyer();
        var book = Builders.MakeBook(1);
        var before = DateTime.Now.AddSeconds(-1);

        buyer.AddPurchase(book);

        var purchase = Assert.Single(buyer.PurchaseHistory);
        Assert.Same(book, purchase.Book);
        Assert.InRange(purchase.PurchaseDate, before, DateTime.Now.AddSeconds(1));
    }

    [Fact]
    public void AddPurchase_MultipleBooks_KeepsInsertionOrder()
    {
        var buyer = Builders.MakeBuyer();

        buyer.AddPurchase(Builders.MakeBook(1));
        buyer.AddPurchase(Builders.MakeBook(2));

        Assert.Equal(new[] { 1, 2 }, buyer.PurchaseHistory.Select(p => p.Book.Id).ToArray());
    }

    [Fact]
    public void RemovePurchaseByBookId_ExistingBook_RemovesOnlyThatPurchase()
    {
        var buyer = Builders.MakeBuyer();
        buyer.AddPurchase(Builders.MakeBook(1));
        buyer.AddPurchase(Builders.MakeBook(2));

        buyer.RemovePurchaseByBookId(1);

        var remaining = Assert.Single(buyer.PurchaseHistory);
        Assert.Equal(2, remaining.Book.Id);
    }

    [Fact]
    public void RemovePurchaseByBookId_UnknownBook_LeavesHistoryUntouched()
    {
        var buyer = Builders.MakeBuyer();
        buyer.AddPurchase(Builders.MakeBook(1));

        buyer.RemovePurchaseByBookId(99);

        Assert.Single(buyer.PurchaseHistory);
    }
}

public class BookPurchaseTests
{
    [Fact]
    public void Constructor_StoresBookAndDate()
    {
        var book = Builders.MakeBook(3);
        var date = new DateTime(2025, 3, 10);

        var purchase = new BookPurchase(book, date);

        Assert.Same(book, purchase.Book);
        Assert.Equal(date, purchase.PurchaseDate);
    }

    [Fact]
    public void Constructor_NullBook_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new BookPurchase(null!, DateTime.Now));
    }
}
