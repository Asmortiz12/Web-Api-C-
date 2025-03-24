namespace Web_Api.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        
        // Claves foráneas
        public Guid FacturaId { get; set; }
        public int ProductoId { get; set; }
        
        // Propiedades adicionales
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        
        // Propiedades de navegación
        public required Factura Factura { get; set; }
        public required Producto Producto { get; set; }
    }
}