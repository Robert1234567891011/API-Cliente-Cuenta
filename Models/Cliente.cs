namespace BCP.Models
{
    //autor Richard Robertopoma/github.com/Robert1234567891011
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string TipoDocumento { get; set; }
        public string DocumentoIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>(); // Inicializa la colección
    }

}
