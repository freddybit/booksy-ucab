using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using backend.models;
using backend.repositories;

namespace backend.services;

public class BookService {
    private readonly BookRepository _repository = BookRepository.Instance;
    
    public Book RegisterBook(BookDTO dto) {
        if (_repository.ExistsBook("_id", dto.Id) == true)
            throw new Exception("Ya existe un libro con ese id.");

        Book book = new Book(dto.Id, dto.NameBook, dto.Subtitle, dto.Series, dto.Author, dto.Language, dto.Publisher,
                             dto.BookCover, dto.TypeBook, dto.BookVolume, dto.BookHeight, dto.BookWidth,
                             dto.CategoryList, dto.NumPages, dto.PublishYear, dto.Cost, dto.Description, dto.Seller);
        
        _repository.AddBook(book);
        _repository.Save();
        return book;
    }
    
    public Book DeleteBook(BookDTO dto) {
        if (_repository.ExistsBook("_id", dto.Id) == false)
            throw new Exception("No existe un libro con ese id.");

        Book book = new Book(dto.Id, dto.NameBook, dto.Subtitle, dto.Series, dto.Author, dto.Language, dto.Publisher,
                             dto.BookCover, dto.TypeBook, dto.BookVolume, dto.BookHeight, dto.BookWidth,
                             dto.CategoryList, dto.NumPages, dto.PublishYear, dto.Cost, dto.Description, dto.Seller);
        
        _repository.RemoveBook(book);
        _repository.Save();
        return book;
    }

    public Book EditBook(BookDTO dto) {
        if (_repository.ExistsBook("_id", dto.Id) == false)
            throw new Exception("No existe un libro con ese id.");
        Book book = new Book(dto.Id, dto.NameBook, dto.Subtitle, dto.Series, dto.Author, dto.Language, dto.Publisher,
                             dto.BookCover, dto.TypeBook, dto.BookVolume, dto.BookHeight, dto.BookWidth,
                             dto.CategoryList, dto.NumPages, dto.PublishYear, dto.Cost, dto.Description, dto.Seller);
        
        _repository.RemoveBook(book);
        _repository.Save();
        return book;
    }

    public List<Book> GetBookList() {
        return _repository.LoadBookList();
    }
    
}

public class BookDTO {
        
    [JsonPropertyName("_id")]
    public required int Id { get; set; }

    [JsonPropertyName("_nameBook")]
    public required string NameBook { get; set; }

    [JsonPropertyName("_subtitle")]
    public required string Subtitle { get; set; }

    [JsonPropertyName("_series")]
    public required string Series { get; set; }

    [JsonPropertyName("_author")]
    public required string Author { get; set; }

    [JsonPropertyName("_language")]
    public required string Language { get; set; }

    [JsonPropertyName("_publisher")]
    public required string Publisher { get; set; }

    [JsonPropertyName("_bookCover")]
    public string BookCover { get; set; }

    [JsonPropertyName("_typeBook")]
    public required string TypeBook { get; set; }

    [JsonPropertyName("_bookVolume")]
    public required string BookVolume { get; set; }

    [JsonPropertyName("_bookHeight")]
    public required float BookHeight { get; set; }

    [JsonPropertyName("_bookWidth")]
    public required float BookWidth { get; set; }

    [JsonPropertyName("_categoryList")]
    public List<string> CategoryList { get; set; }

    [JsonPropertyName("_numPages")]
    public int NumPages { get; set; }

    [JsonPropertyName("_publishYear")]
    public required DateTime PublishYear { get; set; }

    [JsonPropertyName("_cost")]
    public float Cost { get; set; }

    [JsonPropertyName("_description")]
    public required string Description { get; set; }

    [JsonPropertyName("_seller")] 
    public required Seller Seller { get; set; }
    
}