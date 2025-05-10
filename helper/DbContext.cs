using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Conversacion> Conversaciones { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relaciones múltiples a la misma tabla: User como Emisor y Receptor
        modelBuilder.Entity<Conversacion>()
            .HasOne(c => c.Emisor)
            .WithMany(u => u.ConversacionesEnviadas)
            .HasForeignKey(c => c.Id_Emisor)
            .OnDelete(DeleteBehavior.Restrict); // evitar cascada circular

        modelBuilder.Entity<Conversacion>()
            .HasOne(c => c.Receptor)
            .WithMany(u => u.ConversacionesRecibidas)
            .HasForeignKey(c => c.id_remitente)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación Mensaje -> Conversacion
        modelBuilder.Entity<Mensaje>()
            .HasOne(m => m.conversacion)
            .WithMany(c => c.Mensajes)
            .HasForeignKey(m => m.Id_Conversacion);

        modelBuilder.Entity<Mensaje>()
            .HasOne(m => m.user)
            .WithMany(u => u.Mensajes)
            .HasForeignKey(m => m.Id_user);


    }
}
