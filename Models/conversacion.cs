using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Models;

public class Conversacion
{
    [Key]
    public int id_Conversacion { get; set; }

    [ForeignKey(nameof(Id_Emisor))]
    public int Id_Emisor { get; set; }

    [ForeignKey(nameof(id_remitente))]
    public int id_remitente { get; set; }

    public User Emisor { get; set; }
    public User Receptor { get; set; }

    public ICollection<Mensaje> Mensajes { get; set; }

}

public class ConversacionDTO
{
    public int id_Emisor { get; set; }
    public int id_remitente { get; set; }

}