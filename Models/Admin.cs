namespace Web_Api.Models;

public class Admin : User
{
    // Propiedades específicas de Admin
    public string? Departamento { get; set; }
    public string? NivelAcceso { get; set; } = "Total";
}
