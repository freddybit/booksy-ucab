using System.Text.Json;
using backend.models;
using System.Reflection;

namespace backend.repositories {

    public class SellerRepository {
        
        private static readonly SellerRepository _instance = new SellerRepository();
        
        private readonly string _jsonPath = @"models/data/sellers.json";
        
        private List<Seller> _sellersList;
        
        private SellerRepository() {
            _sellersList = new List<Seller>();
            Load();
        }

        public static SellerRepository Instance => _instance;
        
        private void Load() {
            if (!File.Exists(_jsonPath)) return;

            string json = File.ReadAllText(_jsonPath);
            
            var options = new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true,
                IncludeFields = true 
            };
            
            var loadedSellers = JsonSerializer.Deserialize<List<Seller>>(json, options);

            if (loadedSellers != null) {
                _sellersList = loadedSellers;
            }
        }

        //**@brief Guarda todos los vendedores en el archivo JSON, sobrescribiendo el contenido.
        public void Save() {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_sellersList, options);
            File.WriteAllText(_jsonPath, json);
        }

        //**@brief Agrega un nuevo vendedor al repositorio.
        public void AddSeller(Seller seller) {
            _sellersList.Add(seller);
            Save();
        }

        //**@brief Elimina un vendedor del repositorio.
        public void RemoveSeller(Seller? seller) {
            if (seller != null) {
                var sellerInList = _sellersList.FirstOrDefault(s => s.Email == seller.Email);
                if (sellerInList != null) {
                    _sellersList.Remove(sellerInList);
                    Save();
                }
            }
        }
        
        public void UpdateSeller(Seller updatedSeller) {
            int index = _sellersList.FindIndex(s => s.Email == updatedSeller.Email);
            if (index != -1) {
                _sellersList[index] = updatedSeller;
                Save();
            } else {
                _sellersList.Add(updatedSeller);
                Save();
            }
        }

        //**@brief Devuelve la lista completa de vendedores.
        public List<Seller> ReturnSellers() {
            return _sellersList;
        }
        
        public bool ExistsSeller(string attribute, object value)
        {
            foreach (var seller in _sellersList)
            {
                PropertyInfo? prop = seller.GetType().GetProperty(attribute);
                if (prop != null)
                {
                    var propValue = prop.GetValue(seller);
                    if (propValue != null && propValue.Equals(value))
                        return true;
                }
            }
            return false;
        }
        
        public Seller? ReturnSeller(string email) {
            return _sellersList.FirstOrDefault(s =>
                s.Email == email);
        }


        public Seller? ReturnSellerByBookId(int bookId)
        {
            foreach (var seller in _sellersList)
            {
                if (seller.Catalog.Any(b => b.Id == bookId))
                {
                    return seller;
                }
            }
            return null;
        }

        public void RemoveBooksBySeller(int id, String sellerEmail)
        {
            foreach (var s in _sellersList)
            {
                if(s.Email== sellerEmail)
                {   Console.WriteLine($"Encontrado vendedor: {s.Email}");
                    s.RemoveBookById(id);
                    Console.WriteLine($"Libro con Id {id} eliminado del catálogo del vendedor: {s.Email}");
                    Save();
                    break;
                }
            }
        }
        
        public void RemoveSellerByEmail(string email) {
            Seller? seller = _sellersList.FirstOrDefault(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (seller != null)
            {
                _sellersList.Remove(seller);
                Save();
            }
        }

        public void AddBookBySeller(Book book, string sellerEmail)
        {
            foreach (var s in _sellersList)
            {
                if(s.Email== sellerEmail)
                {  Console.WriteLine($"Encontrado vendedor: {s.Email}");
                    s.AddBook(book);
                    Save();
                    break;
                }
            }
        }
        public void AddSaleToSeller(BookPurchase purchase, string sellerEmail)
        {
            foreach (var s in _sellersList)
            {
                if(s.Email== sellerEmail)
                {  Console.WriteLine($"Encontrado vendedor: {s.Email}");
                    s.SalesHistory.Add(purchase);
                    Save();
                    break;
                }
            }
        }


        public void RemoveSaleFromSeller(int bookId, string sellerEmail)
        {
            foreach (var s in _sellersList)
            {
                if(s.Email== sellerEmail)
                {  
                    var saleInList = s.SalesHistory.FirstOrDefault(p => p.Book.Id == bookId);
                    if (saleInList != null) {
                        s.SalesHistory.Remove(saleInList);
                        Save();
                        Console.WriteLine("eliminado de historial de ventas del vendedor");
                    }
                    break;
                }
            }
        }


        public int GetSellerIndexByEmail(string email)
        {
            for (int i = 0; i < _sellersList.Count; i++)
            {
                if (_sellersList[i].Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                    return i + 1; // 1-based index
            }
            return 0;
        }

    }
}
