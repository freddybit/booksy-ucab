using System.Text.Json;
using backend.models;

namespace backend.Tests.Models;

public class BookTests
{
    [Fact]
    public void DefaultConstructor_InitializesEmptyValues()
    {
        var book = new Book();

        Assert.Equal("", book.NameBook);
        Assert.Equal(0, book.Id);
        Assert.Equal(0f, book.Cost);
        Assert.NotNull(book.CategoryList);
        Assert.Empty(book.CategoryList);
        Assert.NotNull(book.Seller);
    }

    [Fact]
    public void ParameterizedConstructor_AssignsAllFields()
    {
        var seller = new Seller();
        var categories = new List<string> { "Novela", "Aventura" };
        var date = new DateTime(2019, 5, 1);

        var book = new Book("img.png", 7, "Titulo", "Sub", "Serie", "Autor", "es", "Editorial", "Dura", "Novela", "2", 21.5f, 14.2f, categories, 320, date, 19.99f, "Desc", seller);

        Assert.Equal("img.png", book.UrlImg);
        Assert.Equal(7, book.Id);
        Assert.Equal("Titulo", book.NameBook);
        Assert.Equal("Sub", book.Subtitle);
        Assert.Equal("Serie", book.Series);
        Assert.Equal("Autor", book.Author);
        Assert.Equal("es", book.Language);
        Assert.Equal("Editorial", book.Publisher);
        Assert.Equal("Dura", book.BookCover);
        Assert.Equal("Novela", book.TypeBook);
        Assert.Equal("2", book.BookVolume);
        Assert.Equal(21.5f, book.BookHeight);
        Assert.Equal(14.2f, book.BookWidth);
        Assert.Same(categories, book.CategoryList);
        Assert.Equal(320, book.NumPages);
        Assert.Equal(date, book.PublishYear);
        Assert.Equal(19.99f, book.Cost);
        Assert.Equal("Desc", book.Description);
        Assert.Same(seller, book.Seller);
    }

    [Fact]
    public void SearchBooks_ReturnsExactMatchBeforeSimilarOnes()
    {
        var books = new List<Book>
        {
            Builders.MakeBook(1, "Don Quijote"),
            Builders.MakeBook(2, "Quijote"),
            Builders.MakeBook(3, "El Quijote de la Mancha"),
            Builders.MakeBook(4, "Otro libro")
        };

        var result = new Book().SearchBooks(books, "Quijote");

        Assert.Equal(new[] { 2, 1, 3 }, result.Select(b => b.Id).ToArray());
    }

    [Fact]
    public void SearchBooks_IsCaseInsensitive()
    {
        var books = new List<Book> { Builders.MakeBook(1, "Rayuela") };

        var result = new Book().SearchBooks(books, "RAYUELA");

        Assert.Single(result);
    }

    [Fact]
    public void SearchBooks_WithoutMatches_ReturnsEmptyList()
    {
        var books = new List<Book> { Builders.MakeBook(1, "Rayuela") };

        var result = new Book().SearchBooks(books, "Ficciones");

        Assert.Empty(result);
    }

    [Fact]
    public void SearchBooks_EmptyCatalog_ReturnsEmptyList()
    {
        var result = new Book().SearchBooks(new List<Book>(), "algo");

        Assert.Empty(result);
    }

    [Fact]
    public void LoadBooks_ValidFile_ReturnsDeserializedBooks()
    {
        var path = Path.Combine(Path.GetTempPath(), $"books-{Guid.NewGuid():N}.json");
        var source = new List<Book> { Builders.MakeBook(1, "Uno"), Builders.MakeBook(2, "Dos") };
        File.WriteAllText(path, JsonSerializer.Serialize(source));

        try
        {
            var result = new Book().LoadBooks(path);

            Assert.Equal(2, result.Count);
            Assert.Equal("Uno", result[0].NameBook);
            Assert.Equal("Dos", result[1].NameBook);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Theory]
    [InlineData("{esto no es json")]
    [InlineData("null")]
    [InlineData("[]")]
    public void LoadBooks_InvalidOrEmptyContent_ReturnsEmptyList(string content)
    {
        var path = Path.Combine(Path.GetTempPath(), $"books-{Guid.NewGuid():N}.json");
        File.WriteAllText(path, content);

        try
        {
            var result = new Book().LoadBooks(path);

            Assert.NotNull(result);
            Assert.Empty(result);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void LoadBooks_MissingFile_ThrowsFileNotFoundException()
    {
        var path = Path.Combine(Path.GetTempPath(), $"missing-{Guid.NewGuid():N}.json");

        Assert.Throws<FileNotFoundException>(() => new Book().LoadBooks(path));
    }

    [Fact]
    public void Serialization_UsesUnderscorePropertyNames()
    {
        var json = JsonSerializer.Serialize(Builders.MakeBook(5, "Serializado"));

        Assert.Contains("\"_nameBook\":\"Serializado\"", json);
        Assert.Contains("\"_id\":5", json);
        Assert.Contains("\"_seller\"", json);
    }

    [Fact]
    public void ShowBooks_PrintsEachBookName()
    {
        var books = new List<Book> { Builders.MakeBook(1, "Uno"), Builders.MakeBook(2, "Dos") };
        var original = Console.Out;
        var writer = new StringWriter();
        Console.SetOut(writer);

        try
        {
            new Book().ShowBooks(books);
        }
        finally
        {
            Console.SetOut(original);
        }

        var output = writer.ToString();
        Assert.Contains("Nombre del libro: Uno", output);
        Assert.Contains("Nombre del libro: Dos", output);
    }
}
