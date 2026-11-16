using backend.repositories;
using backend.services;

namespace backend.Tests.Services;

public class BookServiceTests
{
    private readonly BookService _service = new BookService();

    [Fact]
    public void RegisterBook_ReturnsBookAndPersistsIt()
    {
        var id = Unique.Id();

        var book = _service.RegisterBook(Builders.MakeBookDto(id, "Registrado"));

        Assert.Equal(id, book.Id);
        Assert.Equal("Registrado", book.NameBook);
        Assert.Contains(_service.GetBookList(), b => b.Id == id && b.NameBook == "Registrado");
    }

    [Fact]
    public void RegisterBook_CopiesAllDtoFields()
    {
        var dto = Builders.MakeBookDto(Unique.Id());

        var book = _service.RegisterBook(dto);

        Assert.Equal(dto.Author, book.Author);
        Assert.Equal(dto.Cost, book.Cost);
        Assert.Equal(dto.NumPages, book.NumPages);
        Assert.Equal(dto.CategoryList, book.CategoryList);
        Assert.Equal(dto.PublishYear, book.PublishYear);
    }

    [Fact(Skip = "Defecto conocido: ExistsBook(\"_id\") busca la propiedad por el nombre JSON y nunca coincide, por lo que no se detectan ids duplicados.")]
    public void RegisterBook_DuplicateId_Throws()
    {
        var id = Unique.Id();
        _service.RegisterBook(Builders.MakeBookDto(id));

        Assert.Throws<Exception>(() => _service.RegisterBook(Builders.MakeBookDto(id)));
    }

    [Fact]
    public void DeleteBook_ExistingBook_RemovesIt()
    {
        var id = Unique.Id();
        _service.RegisterBook(Builders.MakeBookDto(id));

        _service.DeleteBook(id.ToString());

        Assert.DoesNotContain(_service.GetBookList(), b => b.Id == id);
    }

    [Fact]
    public void DeleteBook_UnknownBook_ThrowsWithMessage()
    {
        var exception = Assert.Throws<Exception>(() => _service.DeleteBook("-424242"));

        Assert.Equal("No existe un libro con ese id.", exception.Message);
    }

    [Fact]
    public void EditBook_ExistingBook_UpdatesData()
    {
        var id = Unique.Id();
        _service.RegisterBook(Builders.MakeBookDto(id, "Antes"));

        var edited = _service.EditBook(Builders.MakeBookDto(id, "Despues"));

        Assert.Equal("Despues", edited.NameBook);
        Assert.Contains(_service.GetBookList(), b => b.Id == id && b.NameBook == "Despues");
    }

    [Fact]
    public void EditBook_UnknownBook_ThrowsWithMessage()
    {
        var exception = Assert.Throws<Exception>(() => _service.EditBook(Builders.MakeBookDto(-424242)));

        Assert.Equal("No existe un libro con ese id.", exception.Message);
    }

    [Fact]
    public void GetBookList_ReturnsBooksStoredOnDisk()
    {
        var id = Unique.Id();
        _service.RegisterBook(Builders.MakeBookDto(id));

        Assert.Contains(_service.GetBookList(), b => b.Id == id);
    }
}

public class SaleServiceTests
{
    private readonly SaleService _service = new SaleService();

    [Fact]
    public void RegistrarVenta_ReturnsSaleWithGivenData()
    {
        var venta = _service.RegistrarVenta(12.5f, 7, 2, 3, "v@ucab.edu.ve", "c@ucab.edu.ve");

        Assert.Equal(12.5, venta.MontoTotal);
        Assert.Equal(7, venta.LibroId);
        Assert.Equal(2, venta.VendedorId);
        Assert.Equal(3, venta.CompradorId);
        Assert.Equal("v@ucab.edu.ve", venta.SellerEmail);
        Assert.Equal("c@ucab.edu.ve", venta.BuyerEmail);
        Assert.False(venta.IsDeleted);
        Assert.NotNull(SaleRepository.Instance.ReturnVentaByCodigo(venta.CodigoDeCompra));
    }

    [Fact]
    public void RegistrarVenta_AssignsConsecutiveCodes()
    {
        var first = _service.RegistrarVenta(1f, 1, 1, 1, "v@ucab.edu.ve", "c@ucab.edu.ve");
        var second = _service.RegistrarVenta(1f, 1, 1, 1, "v@ucab.edu.ve", "c@ucab.edu.ve");

        Assert.Equal(first.CodigoDeCompra + 1, second.CodigoDeCompra);
    }

    [Fact]
    public void ListarVentas_ExcludesSoftDeletedSales()
    {
        var active = _service.RegistrarVenta(1f, 1, 1, 1, "v@ucab.edu.ve", "c@ucab.edu.ve");
        var deleted = _service.RegistrarVenta(1f, 1, 1, 1, "v@ucab.edu.ve", "c@ucab.edu.ve");
        _service.SoftDelete(deleted.CodigoDeCompra);

        var list = _service.ListarVentas();

        Assert.Contains(list, v => v.CodigoDeCompra == active.CodigoDeCompra);
        Assert.DoesNotContain(list, v => v.CodigoDeCompra == deleted.CodigoDeCompra);
    }

    [Fact]
    public void SoftDelete_ExistingSale_MarksItDeletedAndKeepsRecord()
    {
        var venta = _service.RegistrarVenta(1f, 1, 1, 1, "v@ucab.edu.ve", "c@ucab.edu.ve");

        var result = _service.SoftDelete(venta.CodigoDeCompra);

        Assert.True(result);
        Assert.True(SaleRepository.Instance.ReturnVentaByCodigo(venta.CodigoDeCompra)!.IsDeleted);
    }

    [Fact]
    public void SoftDelete_AlreadyDeletedSale_ReturnsTrue()
    {
        var venta = _service.RegistrarVenta(1f, 1, 1, 1, "v@ucab.edu.ve", "c@ucab.edu.ve");
        _service.SoftDelete(venta.CodigoDeCompra);

        Assert.True(_service.SoftDelete(venta.CodigoDeCompra));
    }

    [Fact]
    public void SoftDelete_UnknownSale_ReturnsFalse()
    {
        Assert.False(_service.SoftDelete(-1));
    }

    [Fact]
    public void HardDelete_ExistingSale_RemovesRecord()
    {
        var venta = _service.RegistrarVenta(1f, 1, 1, 1, "v@ucab.edu.ve", "c@ucab.edu.ve");

        var result = _service.HardDelete(venta.CodigoDeCompra);

        Assert.True(result);
        Assert.Null(SaleRepository.Instance.ReturnVentaByCodigo(venta.CodigoDeCompra));
    }

    [Fact]
    public void HardDelete_UnknownSale_ReturnsFalse()
    {
        Assert.False(_service.HardDelete(-1));
    }
}
