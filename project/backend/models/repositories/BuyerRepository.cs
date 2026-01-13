using System.Text.Json;
using System.Text.Json.Serialization;
using backend.models;
using backend.factories;
using System.Reflection;
using ProfileFactory = backend.factories.ProfileFactory;

namespace backend.repositories {

    public class BuyerRepository {

        private static readonly BuyerRepository _instance = new BuyerRepository();
        
        private readonly string _jsonPath = @"models/data/buyers.json";
        
        private List<Buyer> _buyers;
        
        private BuyerRepository()
        {
            _buyers = new List<Buyer>();
            Load();
        }
        
        public static BuyerRepository Instance => _instance;
        
        private void Load() {
            if (!File.Exists(_jsonPath)) return;

            string json = File.ReadAllText(_jsonPath);
            var rawBuyers = JsonSerializer.Deserialize<List<JsonElement>>(json);

            if (rawBuyers == null) return;

            foreach (var element in rawBuyers)
            {
                string email = element.GetProperty("_email").GetString()!;
                string firstName = element.GetProperty("_firstName").GetString()!;
                string lastName = element.GetProperty("_lastName").GetString()!;
                int age = element.GetProperty("_age").GetInt32()!;
                string password = element.GetProperty("_password").GetString()!;

                Buyer buyer = ProfileFactory.CreateBuyer(email, firstName, lastName, age, password);
                        // reconstruir historial de compras
                if (element.TryGetProperty("PurchaseHistory", out var historyProp))
                {
                    foreach (var purchaseElement in historyProp.EnumerateArray())
                    {
                        var bookJson = purchaseElement.GetProperty("Book");
                        var purchaseDate = purchaseElement.GetProperty("PurchaseDate").GetDateTime();

                        Book book = JsonSerializer.Deserialize<Book>(bookJson.GetRawText())!;
                        buyer.PurchaseHistory.Add(new BookPurchase(book, purchaseDate));
                    }
                }
                _buyers.Add(buyer);
            }
        }
        
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_buyers, options);
            File.WriteAllText(_jsonPath, json);
        }
        
        public void AddBuyer(Buyer buyer)
        {
            _buyers.Add(buyer);
        }
        
        public void RemoveBuyer(Buyer buyer)
        {
            _buyers.Remove(buyer);
        }
        
        public Buyer? ReturnBuyer(string email) {
            return _buyers.FirstOrDefault(b =>b.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        
        public Buyer? LoginBuyer(string email, string password) {
            return _buyers.FirstOrDefault(b =>
                b.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                b.Password.Equals(password, StringComparison.Ordinal));
        }
        
        public bool ExistsBuyer(string attribute, object value)
        {
            foreach (var buyer in _buyers)
            {
                PropertyInfo? prop = buyer.GetType().GetProperty(attribute);
                if (prop != null)
                {
                    var propValue = prop.GetValue(buyer);
                    if (propValue != null && propValue.Equals(value))
                        return true;
                }
            }
            return false;
        }

        public void AddPurchase(Book book, string email)
        {
            for(int i=0; i<_buyers.Count; i++)
            {
                if(_buyers[i].Email==email)
                {
                    _buyers[i].AddPurchase(book);
                    Save();
                    break;
                }
            }
        }

        public void RemovePurchaseBuyer(int bookId, string email)
        {
            for(int i=0; i<_buyers.Count; i++)
            {
                if(_buyers[i].Email==email)
                {
                    _buyers[i].RemovePurchaseByBookId(bookId);
                    Save();
                    break;
                }
            }
        }
        public int GetBuyerIndexByEmail(string email)
        {
            for (int i = 0; i < _buyers.Count; i++)
            {
                if (_buyers[i].Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    return i + 1; // 1-based index
            }
            return 0;
        }
        
        public void DeleteBuyerByEmail(string email) {
            var buyer = _buyers.FirstOrDefault(b => b.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (buyer != null) {
                _buyers.Remove(buyer);
                Save();
            }
            else {
                throw new Exception("El comprador no existe en el sistema.");
            }
        }
        
    }
}
