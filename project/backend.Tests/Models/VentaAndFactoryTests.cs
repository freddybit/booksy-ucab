using backend.models;
using FactoriesProfileFactory = backend.factories.ProfileFactory;
using ModelsProfileFactory = backend.models.ProfileFactory;

namespace backend.Tests.Models;

public class VentaTests
{
    [Fact]
    public void Constructor_AssignsAllFields()
    {
        var fecha = new DateTime(2025, 10, 1);

        var venta = new Venta(5, fecha, 42.5, 10, 20, 30, false, "v@ucab.edu.ve", "c@ucab.edu.ve");

        Assert.Equal(5, venta.CodigoDeCompra);
        Assert.Equal(fecha, venta.FechaDeVenta);
        Assert.Equal(42.5, venta.MontoTotal);
        Assert.Equal(10, venta.LibroId);
        Assert.Equal(20, venta.CompradorId);
        Assert.Equal(30, venta.VendedorId);
        Assert.False(venta.IsDeleted);
        Assert.Equal("v@ucab.edu.ve", venta.SellerEmail);
        Assert.Equal("c@ucab.edu.ve", venta.BuyerEmail);
    }

    [Fact]
    public void Setters_UpdateValues()
    {
        var venta = Builders.MakeVenta(1);

        venta.IsDeleted = true;
        venta.MontoTotal = 99.9;
        venta.SellerEmail = "otro@ucab.edu.ve";

        Assert.True(venta.IsDeleted);
        Assert.Equal(99.9, venta.MontoTotal);
        Assert.Equal("otro@ucab.edu.ve", venta.SellerEmail);
    }
}

public class ProfileFactoryTests
{
    [Fact]
    public void ModelsFactory_Instance_IsSingleton()
    {
        Assert.Same(ModelsProfileFactory.Instance, ModelsProfileFactory.Instance);
    }

    [Fact]
    public void FactoriesFactory_Instance_IsSingleton()
    {
        Assert.Same(FactoriesProfileFactory.Instance, FactoriesProfileFactory.Instance);
    }

    [Fact]
    public void ModelsFactory_CreateBuyer_ReturnsBuyerWithData()
    {
        var buyer = ModelsProfileFactory.CreateBuyer("b@ucab.edu.ve", "Luis", "Gomez", 22, "clave");

        Assert.IsType<Buyer>(buyer);
        Assert.Equal("b@ucab.edu.ve", buyer.Email);
        Assert.Empty(buyer.PurchaseHistory);
    }

    [Fact]
    public void FactoriesFactory_CreateBuyer_ReturnsBuyerWithData()
    {
        var buyer = FactoriesProfileFactory.CreateBuyer("b@ucab.edu.ve", "Luis", "Gomez", 22, "clave");

        Assert.IsType<Buyer>(buyer);
        Assert.Equal("Luis", buyer.FirstName);
    }

    [Fact]
    public void ModelsFactory_CreateSeller_NullCollections_BecomeEmptyLists()
    {
        var seller = ModelsProfileFactory.CreateSeller("s@ucab.edu.ve", "Ana", "Perez", 30, "clave", "Banesco", 1, "0414", null!, null!, null!);

        Assert.NotNull(seller.Catalog);
        Assert.Empty(seller.Catalog);
        Assert.NotNull(seller.Ratings);
        Assert.Empty(seller.Ratings);
    }

    [Fact]
    public void FactoriesFactory_CreateSeller_KeepsProvidedCollections()
    {
        var catalog = new List<Book> { Builders.MakeBook(1) };
        var ratings = new List<float> { 4f };
        var sales = new List<BookPurchase>();

        var seller = FactoriesProfileFactory.CreateSeller("s@ucab.edu.ve", "Ana", "Perez", 30, "clave", "Banesco", 1, "0414", catalog, ratings, sales);

        Assert.Same(catalog, seller.Catalog);
        Assert.Same(ratings, seller.Ratings);
        Assert.Same(sales, seller.SalesHistory);
        Assert.Equal("Banesco", seller.BankName);
    }
}
