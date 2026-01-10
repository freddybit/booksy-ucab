using System.Reflection;
using System.Text.Json;
using backend.models;

namespace backend.repositories;

public class BookRepository {

    private static readonly BookRepository _instance = new BookRepository();
    private readonly string _jsonPath = @"models/data/book.json";
    private List<Book> _books;

    private BookRepository() {
        _books = Load(_jsonPath);
    }
    
    public static BookRepository Instance => _instance;
    private List<Book> Load(string rootFile) {
        try {
            string json = File.ReadAllText(rootFile);
            List<Book>? list = JsonSerializer.Deserialize<List<Book>>(json);
            return  list != null ? list :  new List<Book>();
        }  
        catch (JsonException e) {
            Console.WriteLine($"JsonException: {e.Message}");
            List<Book> list = new List<Book>();
            return list;
        }
    }

    public void Save() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_books, options);
        File.WriteAllText(_jsonPath, json);
    }
    
    public void AddBook(Book book) {
        _books.Add(book);
        Save();
    }

    public List<Book> GetAllBooks() {
        return _books;
    }
    
    public void RemoveBook(string id) {
        if (id != null) {
            // Buscamos por id para asegurar que encontramos el objeto correcto en la lista
            var bookInList = _books.FirstOrDefault(b => b.Id == int.Parse(id));
            if (bookInList != null) {
                _books.Remove(bookInList);
                Save();
                Console.WriteLine($"Libro con Id {id} eliminado del catálogo.");
            }
        }
    }

    public List<Book> LoadBookList() {
        return Load(_jsonPath);
    }

    public void UpdateBook(Book updateBook) {
        int index = _books.FindIndex(b => b.Id == updateBook.Id);
        if (index != -1) {
            _books[index] = updateBook; // Sobreescribe el viejo en la lista
            Save(); // Guarda el archivo limpio
        } else {
            _books.Add(updateBook); // Si es nuevo, lo agrega
            Save();
        }
    }

    public bool ExistsBook(string attribute, object value) {
        foreach (var book in _books)
        {
            PropertyInfo? prop = book.GetType().GetProperty(attribute);
            if (prop != null)
            {
                var propValue = prop.GetValue(book);
                if (propValue != null && propValue.Equals(value))
                    return true;
            }
        }
        return false;
    }
    // ✅ Busca un libro por Id
    public Book? GetBookById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }
    
}