using System.Text.Json.Serialization;
using backend.models;
using backend.factories;
using backend.repositories;
using ProfileFactory = backend.factories.ProfileFactory;

namespace backend.services
{
    /**
     * @class SellerService
     * @brief Servicio que encapsula la lógica de negocio relacionada con vendedores. 
     *
     * Permite registrar nuevos vendedores, consultar perfiles y verificar existencia.
     * Si el correo ya está registrado como comprador, elimina al comprador antes de registrar al vendedor.
     */
    public class SellerService {
        private readonly SellerRepository _sellerRepository = SellerRepository.Instance;
        private readonly BuyerRepository _buyerRepository = BuyerRepository.Instance;

        /**
         * @brief Registra un nuevo vendedor en el sistema.
         * @param dto Objeto DTO con los datos del vendedor.
         * @return Instancia Seller creada.
         * @exception Exception Si el correo ya está registrado como vendedor.
         */
        public Seller RegisterSeller(SellerDTO dto) {
            if (_sellerRepository.ExistsSeller("_email", dto.Email))
                throw new Exception("Ya existe un vendedor con ese correo.");

            Seller seller = ProfileFactory.CreateSeller(dto.Email, dto.FirstName, dto.LastName, dto.Age, dto.Password, dto.BankName, dto.Id, dto.PhoneNumber, dto.Catalog, dto.Ratings, dto.SalesHistory);
            _sellerRepository.AddSeller(seller);
            _sellerRepository.Save();
            return seller;
        }

        /**
         * @brief Consulta un vendedor por nombre y apellido.
         * @param firstName Nombre del vendedor.
         * @param lastName Apellido del vendedor.
         * @return Instancia Seller si se encuentra; null si no existe.
         */
        public Seller? GetSeller(string email) {
            return _sellerRepository.ReturnSeller(email);
        }
    }

    /**
     * @class SellerDTO
     * @brief Objeto de transferencia de datos para registrar vendedores.
     */
    public class SellerDTO {
        [JsonPropertyName("_email")]
        public required string Email { get; set; }
        [JsonPropertyName("_firstName")]
        public required string FirstName { get; set; }
        [JsonPropertyName("_lastName")]
        public required string LastName { get; set; }
        [JsonPropertyName("_age")]
        public required int Age { get; set; }
        [JsonPropertyName("_password")]
        public required string Password { get; set; }
        [JsonPropertyName("_bankName")]
        public required string BankName { get; set; }
        [JsonPropertyName("_id")]
        public required int Id { get; set; }
        [JsonPropertyName("_phoneNumber")]
        public required string PhoneNumber { get; set; }
        [JsonPropertyName("_catalog")]
        public List<Book> Catalog { get; set; } = new();
        [JsonPropertyName("_ratings")]
        public List<float> Ratings { get; set; } = new();
        [JsonPropertyName("_salesHistory")]
        public List<BookPurchase> SalesHistory { get; set; } = new();
    }
}
