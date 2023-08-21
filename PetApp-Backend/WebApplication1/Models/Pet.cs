using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string  Raza { get; set; }
        public string Color { get; set; }
        public decimal Peso{ get; set; }
        public DateTime FechaAlta { get; set; }

    }
}
