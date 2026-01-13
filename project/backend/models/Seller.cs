using System.Text.Json.Serialization;

namespace backend.models
{
    /**
     * @class Seller
     * @brief Representa un usuario vendedor en el sistema Booksy.
     *
     * Hereda de la clase abstracta Profile y añade funcionalidades específicas para vendedores,
     * como un catálogo de libros en venta y una lista de calificaciones recibidas.
     */
    public class Seller : Profile
    {
        // Atributos privados

        private string _bankName;
        private int _id;
        private string _phoneNumber;
        private List<Book> _catalog;    //**@brief Lista de libros que el vendedor tiene en su catálogo.
        private List<float> _ratings;   //**@brief Lista de calificaciones recibidas por el vendedor.
        private List<BookPurchase> _salesHistory;


        public Seller(string email, string firstName, string lastName, int age, string password, string bankName, int id, string phoneNumber, List<Book> catalog, List<float> ratings, List<BookPurchase> salesHistory) : base(email, firstName, lastName, age, password) {
            _bankName = bankName;
            _id = id;
            _phoneNumber = phoneNumber;
            _catalog = catalog;
            _ratings = ratings;
            _salesHistory = salesHistory;
        }

        public Seller() : base() {
            _bankName = "";
            _id = 0;
            _phoneNumber = "";
            _catalog = new List<Book>();
            _ratings = new List<float>();
            _salesHistory = new List<BookPurchase>();
        }
        
        [JsonPropertyName("_bankName")]
        public string BankName {
            get => _bankName;
            set => _bankName = value ?? throw new ArgumentNullException(nameof(value));
        }

        [JsonPropertyName("_id")]
        public int Id {
            get => _id;
            set => _id = value;
        }

        [JsonPropertyName("_phoneNumber")]
        public string PhoneNumber {
            get => _phoneNumber;
            set => _phoneNumber = value ?? throw new ArgumentNullException(nameof(value));
        }

        [JsonPropertyName("_catalog")]
        public List<Book> Catalog {
            get => _catalog;
            set => _catalog = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        [JsonPropertyName("_ratings")]
        public List<float> Ratings {
            get => _ratings;
            set => _ratings = value ?? throw new ArgumentNullException(nameof(value));
        }

        [JsonPropertyName("_salesHistory")]
        public List<BookPurchase> SalesHistory {
            get => _salesHistory;
            set => _salesHistory = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void AddBook(Book book)
        {
            _catalog.Add(book);
        }

        public void RemoveBookById(int id)
        {
            var bookInList = _catalog.FirstOrDefault(b => b.Id == id);
            if (bookInList != null) {
                _catalog.Remove(bookInList);
            }
        }
    }
}
