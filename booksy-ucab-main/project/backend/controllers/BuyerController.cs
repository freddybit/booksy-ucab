using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;
using backend.repositories;
using System.Text.Json;
namespace backend.controllers
{
    /**
     * @class BuyerController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con compradores.
     *
     * Expone endpoints para registrar y consultar perfiles de tipo Buyer.
     */
    [ApiController]
    [Route("api/buyer")]
    public class BuyerController : ControllerBase
    {
        private readonly BuyerService _service = new BuyerService();

        /**
         * @brief Endpoint para registrar un nuevo comprador.
         * @param dto Datos del comprador enviados desde el frontend.
         * @return Resultado HTTP con el comprador creado o error.
         */
        [HttpPost("register")]
        public IActionResult RegisterBuyer([FromBody] BuyerDTO dto)
        {
            try
            {
                Buyer buyer = _service.RegisterBuyer(dto);
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Endpoint para consultar un comprador por nombre y apellido.
         * @param firstName Nombre del comprador.
         * @param lastName Apellido del comprador.
         * @return Resultado HTTP con el comprador encontrado o error.
         */
        [HttpGet("getByEmail")]
        public IActionResult GetBuyer([FromQuery] string email) {
            try {
                Buyer? buyer = _service.GetBuyer(email);
                if (buyer == null)
                    return NotFound(new { error = "Comprador no encontrado." });
                return Ok(buyer);
            } catch (Exception e) {
                return StatusCode(500, new { error = e.Message, stack = e.StackTrace });
            }
        }

        [HttpPost("login")]
        public IActionResult LoginBuyer([FromBody] BuyerLoginDTO dto) {
            try {
                Buyer? buyer = _service.LoginBuyer(dto.Email, dto.Password);
                if (buyer == null) {
                    return Unauthorized(new { success = false, error = "Credenciales incorrectas" });
                }
                return Ok(new { success = true, buyer });
            } catch (Exception e) {
                return StatusCode(500, new { error = e.Message, stack = e.StackTrace });
            }
        }


        
    [HttpPost("confirm-purchase")]
    public IActionResult ConfirmPurchase([FromBody] PurchaseRequest request)
    {
        //var buyerRepo = BuyerRepository.Instance; //no funciona porque password suelta error
        var buyerRepo= BuyerRepository.Instance;
        var bookRepo = BookRepository.Instance;
        var sellerRepo = SellerRepository.Instance;

        // 1. Buscar comprador
        var buyer = buyerRepo.ReturnBuyer(request.Email);
        if (buyer == null)
            return BadRequest("Comprador no encontrado");

        // 2. Buscar libro
        Console.WriteLine("busca el libro");
        var book = bookRepo.GetBookById(request.BookId);
        Console.WriteLine($"Libro encontrado: {book?.NameBook}");
        if (book == null)
            return BadRequest("Libro no encontrado");
            Console.WriteLine("libro existe");

        // 3. Registrar compra en el comprador
        buyerRepo.AddPurchase(book, request.Email);
        Console.WriteLine("registrar compra");

        // 4. Eliminar libro del catálogo
        string bookId=request.BookId.ToString();
        bookRepo.RemoveBook(bookId);
        Console.WriteLine("eliminar libro del catalogo");

        // 5. Eliminar libro del vendedor
        var seller = sellerRepo.ReturnSellerByBookId(request.BookId);
        if (seller != null)
        {
            var purchase= new BookPurchase(book, DateTime.Now);
            string sellerEmail=seller.Email;
            sellerRepo.AddSaleToSeller(purchase, sellerEmail);
            sellerRepo.RemoveBooksBySeller(request.BookId, sellerEmail);
            Console.WriteLine($"Libro eliminado del vendedor: {seller.Email}");
        }
        Console.WriteLine("eliminar libro del vendedor");

        // 6. Confirmación
        Console.WriteLine("confirmacion");
        return Ok(new { success = true });
    }
    [HttpDelete("delete-purchase")]
    public IActionResult deletePurchase([FromBody] PurchaseRequest request)
        {
            var buyerRepo = BuyerRepository.Instance;
            var bookRepo = BookRepository.Instance;
            var sellerRepo = SellerRepository.Instance;    //usar en vendedor el email para evitar conflictos con la base de datos
            Console.WriteLine("iniciar eliminar compra");

                // 1. Buscar comprador
            var buyer = buyerRepo.ReturnBuyer(request.Email);
            if (buyer == null)
                return BadRequest("Comprador no encontrado");

                // 2. Buscar libro en el catalogo del comprador
            var purchase = buyer.PurchaseHistory.FirstOrDefault(b => b.Book.Id == request.BookId);
            if (purchase == null){
                return BadRequest("Libro no encontrado en las compras del comprador");
            }
            var book = new Book(
                purchase.Book.UrlImg, 
                purchase.Book.Id, 
                purchase.Book.NameBook, 
                purchase.Book.Subtitle,
                purchase.Book.Series,
                purchase.Book.Author,
                purchase.Book.Language,
                purchase.Book.Publisher,
                purchase.Book.BookCover,
                purchase.Book.TypeBook,
                purchase.Book.BookVolume,
                purchase.Book.BookHeight,
                purchase.Book.BookWidth,
                purchase.Book.CategoryList,
                purchase.Book.NumPages,
                purchase.Book.PublishYear,
                purchase.Book.Cost,
                purchase.Book.Description,
                purchase.Book.Seller
                );
            Console.WriteLine($"Libro encontrado en compras: {book.NameBook}");
            DateTime hoy = DateTime.Now;
            
                // 3. eliminar compra del comprador
            buyerRepo.RemovePurchaseBuyer(request.BookId,request.Email);

                //4. agregar libro en el catalogo
            bookRepo.AddBook(book);

                //5. agregar libro en el vendedor
            var seller= book.Seller;
            if(seller!=null)    
            {
                sellerRepo.RemoveSaleFromSeller(request.BookId,seller.Email);
                sellerRepo.AddBookBySeller(book,book.Seller.Email);
            }

            Console.WriteLine("eliminar compra terminado");
                //6.confirmacion
            return Ok(new {success = true});
        }

    [HttpGet("purchases")]
    public IActionResult GetPurchases([FromQuery] string email)
    {
        var buyerRepo = BuyerRepository.Instance;
        var buyer = buyerRepo.ReturnBuyer(email);

        if (buyer == null)
            return NotFound(new { error = "Comprador no encontrado. back" });

        Console.WriteLine("cantidad de purchases: ", buyer.PurchaseHistory.Count, "esta");
        return Ok(buyer.PurchaseHistory);
    }
}
        public class PurchaseRequest
    {
        public string Email { get; set; } = string.Empty;
        public int BookId { get; set; }
    }
}
