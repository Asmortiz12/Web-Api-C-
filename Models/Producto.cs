namespace Web_Api.Models;

public class Producto
{       
    public int Id { get; set; }

    public ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
    public string? Nombre { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }
}
