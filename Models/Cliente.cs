namespace Web_Api.Models;

public class Cliente : User
{
    // Propiedades específicas de Cliente
    public string? DireccionEnvio { get; set; }
    public DateTime FechaRegistro { get; set; } =  DateTime.UtcNow;
}
