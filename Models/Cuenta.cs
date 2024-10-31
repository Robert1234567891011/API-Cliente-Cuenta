namespace BCP.Models
{
    //autor Richard Robertopoma/github.com/Robert1234567891011
    public class Cuenta
    {
        public int Id { get; set; }
        public string TipoProducto { get; set; }
        public string NumeroCuenta { get; set; }
        public string Moneda { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Sucursal { get; set; }
        public int ClienteId { get; set; } // Relación con Cliente
    }
}
