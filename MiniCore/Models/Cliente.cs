namespace MiniCore.Models
{
    public class Cliente
    {

        public int ClienteId { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Contrato>? Contratos { get; set; }
    }
}
