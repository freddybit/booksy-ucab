using System.Text.Json.Serialization;
using backend.models;
using backend.repositories;
using ProfileFactory = backend.factories.ProfileFactory;

namespace backend.services
{

    public class BuyerService
    {
        private readonly BuyerRepository _repository = BuyerRepository.Instance;
        private readonly SellerRepository _sellerRepository = SellerRepository.Instance;
        private readonly BookRepository _bookRepository = BookRepository.Instance; 
        
        public Buyer RegisterBuyer(BuyerDTO dto)
        {
            if (_repository.ExistsBuyer("_email", dto.Email))
                throw new Exception("Ya existe un comprador con ese correo.");

            Buyer buyer = ProfileFactory.CreateBuyer(dto.Email, dto.FirstName, dto.LastName, dto.Age, dto.Password);
            _repository.AddBuyer(buyer);
            _repository.Save();
            return buyer;
        }
        
        public Buyer? LoginBuyer(string email, string password) {
            if (_repository.ExistsBuyer("Email", email) == false) {
                throw new Exception("No se encontro esta direccion de correo");
            }
            return _repository.LoginBuyer(email, password) ;
        }
        
        public Buyer? GetBuyer(string email) {
            return _repository.ReturnBuyer(email);
        }
        
        public void DeleteBuyer(string email) {
            
            Buyer? buyer = _repository.ReturnBuyer(email);
            if (buyer == null) throw new Exception("Usuario no encontrado.");
            _repository.RemoveBuyer(buyer);
            _repository.Save();
            
            Seller? seller = _sellerRepository.ReturnSeller(email);
            if (seller != null) { 
                foreach (var book in seller.Catalog) {
                    _bookRepository.RemoveBook(book.Id.ToString());
                }
                _sellerRepository.RemoveSeller(seller);
            }
        }
        
    }

    /**
     * @class BuyerDTO
     * @brief Objeto de transferencia de datos para registrar compradores.
     */
    public class BuyerDTO {
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Password { get; set; }
    }
    
    public class BuyerLoginDTO {
        
        [JsonPropertyName("_email")]
        public required string Email { get; set; }
        
        [JsonPropertyName("_password")]
        public required string Password { get; set; }
    }

}
