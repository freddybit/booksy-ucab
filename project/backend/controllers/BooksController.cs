using Microsoft.AspNetCore.Mvc;
using backend.models;
using System.Text.Json;
using System.IO;

namespace backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController :ControllerBase {

    [HttpGet]
    public IActionResult GetBooks() {
        Book b = new Book();
        string rootFile = Path.Combine("models", "data", "book.json");
        if (!System.IO.File.Exists(rootFile)) return Ok(new List<Book>());
        List<Book> books = b.LoadBooks(rootFile);
        return Ok(books);
    }

    [HttpPost]
    public IActionResult PostBook([FromBody] Book book)
    {
        if (book == null) return BadRequest("Book is null");

        string rootFile = Path.Combine("models", "data", "book.json");
        var loader = new Book();
        List<Book> books = loader.LoadBooks(rootFile) ?? new List<Book>();

        books.Add(book);

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(books, options);
        // Ensure directory exists
        var dir = Path.GetDirectoryName(rootFile) ?? "models/data";
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        System.IO.File.WriteAllText(rootFile, json);

        // Devolver 201 Created con el libro añadido
        return CreatedAtAction(nameof(GetBooks), null, book);
    }
}