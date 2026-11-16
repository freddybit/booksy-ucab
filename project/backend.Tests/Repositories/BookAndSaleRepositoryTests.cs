using backend.models;
using backend.repositories;

namespace backend.Tests.Repositories;

public class BookRepositoryTests
{
    private readonly BookRepository _repository = BookRepository.Instance;

    [Fact]
    public void Instance_ReturnsSameObject()
    {
        Assert.Same(BookRepository.Instance, BookRepository.Instance);
    }

    [Fact]
    public void AddBook_StoresBookInMemoryAndOnDisk()
    {
        var id = Unique.Id();

        _repository.AddBook(Builders.MakeBook(id, "Persistido"));

        Assert.NotNull(_repository.GetBookById(id));
        Assert.Contains(_repository.LoadBookList(), b => b.Id == id && b.NameBook == "Persistido");
    }

    [Fact]
    public void GetAllBooks_IncludesAddedBook()
    {
        var id = Unique.Id();

        _repository.AddBook(Builders.MakeBook(id));

        Assert.Contains(_repository.GetAllBooks(), b => b.Id == id);
    }

    [Fact]
    public void GetBookById_UnknownId_ReturnsNull()
    {
        Assert.Null(_repository.GetBookById(-12345));
    }

    [Fact]
    public void RemoveBook_ExistingBook_RemovesItFromMemoryAndDisk()
    {
        var id = Unique.Id();
        _repository.AddBook(Builders.MakeBook(id));

        _repository.RemoveBook(id.ToString());

        Assert.Null(_repository.GetBookById(id));
        Assert.DoesNotContain(_repository.LoadBookList(), b => b.Id == id);
    }

    [Fact]
    public void RemoveBook_UnknownId_DoesNotThrow()
    {
        var exception = Record.Exception(() => _repository.RemoveBook("-999999"));

        Assert.Null(exception);
    }

    [Fact]
    public void UpdateBook_ExistingBook_ReplacesIt()
    {
        var id = Unique.Id();
        _repository.AddBook(Builders.MakeBook(id, "Original"));

        _repository.UpdateBook(Builders.MakeBook(id, "Actualizado"));

        Assert.Equal("Actualizado", _repository.GetBookById(id)!.NameBook);
        Assert.Contains(_repository.LoadBookList(), b => b.Id == id && b.NameBook == "Actualizado");
    }

    [Fact]
    public void UpdateBook_NewBook_AddsIt()
    {
        var id = Unique.Id();

        _repository.UpdateBook(Builders.MakeBook(id, "Nuevo"));

        Assert.Equal("Nuevo", _repository.GetBookById(id)!.NameBook);
    }

    [Fact]
    public void ExistsBook_MatchingProperty_ReturnsTrue()
    {
        var id = Unique.Id();
        _repository.AddBook(Builders.MakeBook(id, "Buscable"));

        Assert.True(_repository.ExistsBook("Id", id));
        Assert.True(_repository.ExistsBook("NameBook", "Buscable"));
    }

    [Fact]
    public void ExistsBook_NonMatchingValueOrUnknownProperty_ReturnsFalse()
    {
        Assert.False(_repository.ExistsBook("Id", -777));
        Assert.False(_repository.ExistsBook("PropiedadInexistente", 1));
    }
}

public class SaleRepositoryTests
{
    private readonly SaleRepository _repository = SaleRepository.Instance;

    [Fact]
    public void Instance_ReturnsSameObject()
    {
        Assert.Same(SaleRepository.Instance, SaleRepository.Instance);
    }

    [Fact]
    public void AddVenta_MakesItRetrievableByCode()
    {
        var codigo = Unique.Id();

        _repository.AddVenta(Builders.MakeVenta(codigo));

        var venta = _repository.ReturnVentaByCodigo(codigo);
        Assert.NotNull(venta);
        Assert.Equal(25.5, venta!.MontoTotal);
        Assert.Contains(_repository.ReturnVentas(), v => v.CodigoDeCompra == codigo);
    }

    [Fact]
    public void ReturnVentaByCodigo_UnknownCode_ReturnsNull()
    {
        Assert.Null(_repository.ReturnVentaByCodigo(-1));
    }

    [Fact]
    public void RemoveVenta_RemovesIt()
    {
        var codigo = Unique.Id();
        var venta = Builders.MakeVenta(codigo);
        _repository.AddVenta(venta);

        _repository.RemoveVenta(venta);

        Assert.Null(_repository.ReturnVentaByCodigo(codigo));
    }

    [Fact]
    public void RemoveVentaById_RemovesSaleForThatBook()
    {
        var codigo = Unique.Id();
        var libroId = Unique.Id();
        _repository.AddVenta(Builders.MakeVenta(codigo, libroId));

        _repository.RemoveVentaById(libroId);

        Assert.Null(_repository.ReturnVentaByCodigo(codigo));
    }

    [Fact]
    public void RemoveVentaById_UnknownBook_DoesNotThrow()
    {
        var exception = Record.Exception(() => _repository.RemoveVentaById(-4321));

        Assert.Null(exception);
    }
}
