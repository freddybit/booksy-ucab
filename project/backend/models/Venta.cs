using System;
namespace backend.models
{
    public class Venta
    {
        private int _codigoDeCompra;
        private DateTime _fechaDeVenta;
        private double _montoTotal;
        private int _libroId;
        private int _compradorId;
        private int _vendedorId;
        private bool _isDeleted;
        private string _sellerEmail;
        private string _buyerEmail;

        public Venta(int codigoDeCompra, DateTime fechaDeVenta, double montoTotal, int libroId, int compradorId, int vendedorId, bool isDeleted, string sellerEmail, string buyerEmail)
        {
            _codigoDeCompra = codigoDeCompra;
            _fechaDeVenta = fechaDeVenta;
            _montoTotal = montoTotal;
            _libroId = libroId;
            _sellerEmail = sellerEmail;
            _buyerEmail = buyerEmail;
            _compradorId = compradorId;
            _vendedorId = vendedorId;
            _isDeleted = isDeleted;
        }

        public int CodigoDeCompra
        {
            get => _codigoDeCompra;
            set => _codigoDeCompra = value;
        }
        public DateTime FechaDeVenta
        {
            get => _fechaDeVenta;
            set => _fechaDeVenta = value;
        }
        public double MontoTotal
        {
            get => _montoTotal;
            set => _montoTotal = value;
        }
        public int LibroId
        {
            get => _libroId;
            set => _libroId = value;
        }
        public int CompradorId
        {
            get => _compradorId;
            set => _compradorId = value;
        }
        public int VendedorId{
            get => _vendedorId;
            set => _vendedorId = value;
        }
        public bool IsDeleted{
            get => _isDeleted;
            set => _isDeleted = value;
        }

        public string SellerEmail{
            get => _sellerEmail;
            set => _sellerEmail = value;
        }
        public string BuyerEmail{
            get => _buyerEmail;
            set => _buyerEmail = value;
        }
    }
}