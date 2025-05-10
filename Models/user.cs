using System.ComponentModel.DataAnnotations;

namespace Chat.Models;

public class User
{
    [Key]
    public int Id_user { get; set; }

    public string nombre { get; set; }
    public string contra { get; set; }

    // Navegación inversa (opcional)
    public ICollection<Conversacion> ConversacionesEnviadas { get; set; }
    public ICollection<Conversacion> ConversacionesRecibidas { get; set; }

    public ICollection<Mensaje> Mensajes { get; set; }
}