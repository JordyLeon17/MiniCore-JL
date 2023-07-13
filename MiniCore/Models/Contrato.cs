using System.ComponentModel.DataAnnotations;

namespace MiniCore.Models
{
    public class Contrato
    {
        public int ContratoId { get; set; }

        public int ClienteID { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public string? Nombre { get; set; }
        public float Monto { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}
