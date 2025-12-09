using Microsoft.AspNetCore.Mvc;
using backend.models;
using System.Text.Json;
using System.IO;
using backend.services;

namespace backend.controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController :ControllerBase {

    private readonly BookService _service;

    public BooksController(BookService service) {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetBooks() {
        List<Book> books = _service.GetBookList();
        return Ok(books);
    }
    
    [HttpGet("{id}")] 
    public IActionResult GetBookById(int id)
    {
        // Lógica simulada para buscar un libro
        var book = _service.GetBookList().FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        return Ok(book);
    }
    
    [HttpPost]
    public IActionResult PostBook([FromBody] BookDTO dto) {

        try {
            Book book = _service.RegisterBook(dto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book); // Devolver 201 Created con el libro añadido
        }
        catch (Exception e) {
            return BadRequest(new { error = e.Message });
        }
        
    }
}