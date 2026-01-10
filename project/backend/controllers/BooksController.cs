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
    
    [HttpPost ("Edit")]
    public IActionResult EditBook([FromBody] BookDTO dto) {
        try {
            Book book = _service.EditBook(dto);
            return Ok(book);
        } catch (Exception ex) {
            Console.WriteLine($"Error en EditBook:{ex.Message}");  
            return StatusCode(500, new { 
                error = "Error interno al conectar con la base de datos.",
                details = ex.Message 
            });
        }
    }
    
    [HttpDelete("Delete")]
    public IActionResult Delete([FromQuery] string id) {
        // Esto aparecerá en tu consola apenas hagas clic en el botón
        Console.WriteLine($"\n--- INTENTO DE ELIMINACIÓN ---");
        Console.WriteLine($"ID recibido: {id}");

        try {
            if (string.IsNullOrEmpty(id)) {
                Console.WriteLine("Error: El ID llegó vacío.");
                return BadRequest("ID requerido");
            }

            _service.DeleteBook(id);
        
            Console.WriteLine("Resultado: ¡Libro borrado con éxito!");
            return Ok(new { message = "Libro borrado exitosamente!" });
        } catch (Exception ex) {
            // AQUÍ VEREMOS EL CULPABLE DEL ERROR 500
            Console.WriteLine($"\n!!! ERROR EN EL SERVICIO !!!");
            Console.WriteLine($"Mensaje: {ex.Message}");
            if (ex.InnerException != null) {
                Console.WriteLine($"Causa raíz: {ex.InnerException.Message}");
            }
            Console.WriteLine("------------------------------\n");

            return StatusCode(500, new { 
                error = "Error interno.",
                details = ex.Message 
            });
        }
    }
    
}