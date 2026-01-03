using Microsoft.AspNetCore.Mvc;
using backend.models;
using backend.services;

namespace backend.controllers
{
    /**
     * @class SellerController
     * @brief Controlador HTTP para gestionar operaciones relacionadas con vendedores.
     *
     * Expone endpoints para registrar y consultar perfiles de tipo Seller.
     */
    [ApiController]
    [Route("api/seller")]
    public class SellerController : ControllerBase
    {
        private readonly SellerService _service = new SellerService();

        /**
         * @brief Endpoint para registrar un nuevo vendedor.
         * @param dto Datos del vendedor enviados desde el frontend.
         * @return Resultado HTTP con el vendedor creado o error.
         */
        [HttpPost("register")]
        public IActionResult RegisterSeller([FromBody] SellerDTO dto) {
            try {
                Seller seller = _service.RegisterSeller(dto);
                return Ok(seller);
            } catch (Exception ex) {
                return BadRequest(new { error = ex.Message });
            }
        }

        /**
         * @brief Endpoint para consultar un vendedor por nombre y apellido.
         * @param firstName Nombre del vendedor.
         * @param lastName Apellido del vendedor.
         * @return Resultado HTTP con el vendedor encontrado o error.
         */
        [HttpGet("get")]
        public IActionResult GetSeller([FromQuery] string email) {
            try {
                // Aquí es donde probablemente falla si la DB está caída o pausada
                Seller? seller = _service.GetSeller(email);
        
                if (seller == null)
                    return NotFound(new { message = "Vendedor no encontrado." });

                return Ok(seller);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error en GetSeller: {ex.Message}");
        
                return StatusCode(500, new { 
                    error = "Error interno al conectar con la base de datos.",
                    details = ex.Message 
                });
            }
        }

        [HttpPost("edit")]
        public IActionResult EditSeller([FromBody] SellerDTO dto) {
            try {
                Seller seller = _service.EditSeller(dto);
                return Ok(seller);
            } catch (Exception ex) {
                Console.WriteLine($"Error en EditSeller:{ex.Message}");  
                return StatusCode(500, new { 
                    error = "Error interno al conectar con la base de datos.",
                    details = ex.Message 
                });
            }
        }

    }
}
