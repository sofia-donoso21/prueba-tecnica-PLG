namespace ClientesAPI.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }
        public string Pais { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
