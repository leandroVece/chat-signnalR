using System.Threading.Tasks;
using Chat.Context;
using Chat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Chat.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{


    private readonly ILogger<ChatController> _logger;

    private readonly AppDbContext _database;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(ILogger<ChatController> logger, AppDbContext database, IHubContext<ChatHub> hubContext)
    {
        _logger = logger;
        _database = database;
        _hubContext = hubContext;
    }


    [HttpPost("login")]
    public async Task<ActionResult> login([FromBody] Login login)
    {
        var res = await _database.Users.Where(x => x.nombre == login.nombre && x.contra == login.contra).FirstOrDefaultAsync();
        return Ok(res);
    }
    [HttpGet("all/{id}")]
    public async Task<ActionResult> GetAllUser(int id)
    {
        var res = await _database.Users
            .Where(x => x.Id_user != id) // Filtrar primero
            .Select(x => new { Id_user = x.Id_user, name = x.nombre })
            .ToListAsync();

        return Ok(res);
    }

    [HttpPost("AllMesaje")]
    public async Task<ActionResult> GetAllMensaje([FromBody] ConversacionDTO dTO)
    {
        try
        {
            var conversacion = await _database.Conversaciones.FirstOrDefaultAsync(c =>
            (c.Id_Emisor == dTO.id_Emisor && c.id_remitente == dTO.id_remitente) ||
            (c.Id_Emisor == dTO.id_remitente && c.id_remitente == dTO.id_Emisor)
        );
            if (conversacion is null)
            {
                return Ok();
            }
            var mensajes = await _database.Mensajes
                .Where(m => m.Id_Conversacion == conversacion.id_Conversacion)
                .Select(m => new
                {
                    id_mensaje = m.id_mensaje,
                    contenido = m.contenido,
                    id_emisor = m.Id_user,
                })
                .ToListAsync();

            return Ok(mensajes);
        }
        catch (System.Exception)
        {

            return Unauthorized();
        }

    }

    [HttpPost("newMensaje")]
    public async Task<IActionResult> SendMensaje([FromBody] MensajeDTO dto)
    {
        try
        {
            // Verificar si ya existe la conversación
            var conversacion = await _database.Conversaciones
                .FirstOrDefaultAsync(c =>
                    (c.Id_Emisor == dto.id_Emisor && c.id_remitente == dto.id_remitente) ||
                    (c.Id_Emisor == dto.id_remitente && c.id_remitente == dto.id_Emisor));

            if (conversacion == null)
            {
                conversacion = new Conversacion
                {
                    Id_Emisor = dto.id_Emisor,
                    id_remitente = (int)dto.id_remitente
                };
                _database.Conversaciones.Add(conversacion);
                await _database.SaveChangesAsync();
            }

            var mensaje = new Mensaje
            {
                contenido = dto.contenido,
                Id_user = dto.id_Emisor,
                Id_Conversacion = conversacion.id_Conversacion
            };

            _database.Mensajes.Add(mensaje);
            await _database.SaveChangesAsync();

            // Aquí está lo importante: enviar ambos IDs a SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", new
            {
                contenido = mensaje.contenido,
                id_emisor = mensaje.Id_user,
                id_remitente = dto.id_remitente
            });

            return Ok();
        }
        catch
        {
            return BadRequest("Error al enviar el mensaje");
        }
    }




}
