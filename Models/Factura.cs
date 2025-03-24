namespace Web_Api.Models
{
    public class Factura
    {
        public Guid Id { get; set; }

        public required ICollection<DetalleFactura> Detalles { get; set; }
    // other properties

        public int ClienteId { get; set; }
    
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        
        // Navegación
        public required Cliente Cliente { get; set; }
        public ICollection<DetalleFactura> DetalleFactura { get; set; } = new List<DetalleFactura>();
    }
}