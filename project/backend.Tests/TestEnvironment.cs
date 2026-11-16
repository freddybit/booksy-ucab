using System.Runtime.CompilerServices;
using backend.models;
using backend.services;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace backend.Tests;

internal static class TestEnvironment
{
    public static string Root { get; private set; } = "";

    [ModuleInitializer]
    internal static void Initialize()
    {
        Root = Path.Combine(Path.GetTempPath(), "booksy-tests-" + Guid.NewGuid().ToString("N"));
        var data = Path.Combine(Root, "models", "data");
        Directory.CreateDirectory(data);
        foreach (var file in new[] { "book.json", "buyers.json", "sellers.json", "ventas.json" })
        {
            File.WriteAllText(Path.Combine(data, file), "[]");
        }
        Directory.SetCurrentDirectory(Root);
    }
}

internal static class Unique
{
    private static int _counter = 100000;

    public static int Id() => Interlocked.Increment(ref _counter);

    public static string Email() => $"{Guid.NewGuid():N}@ucab.edu.ve";
}

internal static class Builders
{
    public static Book MakeBook(int id, string name = "Libro de prueba", float cost = 10f) =>
        new Book { Id = id, NameBook = name, Cost = cost };

    public static Seller MakeSeller(string? email = null) =>
        new Seller
        {
            Email = email ?? Unique.Email(),
            FirstName = "Ana",
            LastName = "Perez",
            Age = 30,
            Password = "clave123",
            BankName = "Banesco",
            Id = Unique.Id(),
            PhoneNumber = "04141234567"
        };

    public static Buyer MakeBuyer(string? email = null, string password = "clave123") =>
        new Buyer(email ?? Unique.Email(), "Luis", "Gomez", 22, password);

    public static BookDTO MakeBookDto(int id, string name = "Libro DTO") =>
        new BookDTO
        {
            UrlImg = "img.png",
            Id = id,
            NameBook = name,
            Subtitle = "Subtitulo",
            Series = "Serie",
            Author = "Autor",
            Language = "es",
            Publisher = "Editorial",
            BookCover = "Tapa blanda",
            TypeBook = "Novela",
            BookVolume = "1",
            BookHeight = 20f,
            BookWidth = 14f,
            CategoryList = new List<string> { "Novela" },
            NumPages = 100,
            PublishYear = new DateTime(2020, 1, 1),
            Cost = 15f,
            Description = "Descripcion",
            Seller = new Seller()
        };

    public static SellerDTO MakeSellerDto(string? email = null) =>
        new SellerDTO
        {
            Email = email ?? Unique.Email(),
            FirstName = "Ana",
            LastName = "Perez",
            Age = 30,
            Password = "clave123",
            BankName = "Banesco",
            Id = Unique.Id(),
            PhoneNumber = "04141234567"
        };

    public static BuyerDTO MakeBuyerDto(string? email = null) =>
        new BuyerDTO
        {
            Email = email ?? Unique.Email(),
            FirstName = "Luis",
            LastName = "Gomez",
            Age = 22,
            Password = "clave123"
        };

    public static Venta MakeVenta(int codigo, int libroId = 1, bool isDeleted = false) =>
        new Venta(codigo, DateTime.Now, 25.5, libroId, 1, 2, isDeleted, "vendedor@ucab.edu.ve", "comprador@ucab.edu.ve");
}
