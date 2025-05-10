using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Models;

public class Mensaje
{
    [Key]
    public int id_mensaje { get; set; }

    [ForeignKey(nameof(Id_Conversacion))]
    public int Id_Conversacion { get; set; }

    [ForeignKey(nameof(Id_user))]

    public int Id_user { get; set; }

    public string contenido { get; set; }

    public Conversacion conversacion { get; set; }
    public User user { get; set; }


}
public class MensajeDTO
{
    public int? id_remitente { get; set; }
    public int id_Emisor { get; set; }
    public string contenido { get; set; }

}