using Microsoft.EntityFrameworkCore;
using projeto.Model;

namespace projeto.Database
{
    public class ClienteDbContext : DbContext
    {
       public ClienteDbContext(DbContextOptions<ClienteDbContext> options
       ) : base(options){

       }

       public DbSet<Cliente> Clientes {get; set;}

       protected override void OnModelCreating(ModelBuilder modelBuilder){
        var cliente = modelBuilder.Entity<Cliente>();
        cliente.ToTable("");
        cliente.HasKey(x => x.Id);
        cliente.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        cliente.Property(x => x.Nome).HasColumnName("nome").IsRequired();
        cliente.Property(x => x.Nascimento).HasColumnName("nascimento");
        cliente.Property(x => x.Email).HasColumnName("email").IsRequired();
        cliente.Property(x => x.Sexo).HasColumnName("sexo").IsRequired();
        cliente.Property(x => x.Telefone).HasColumnName("telefone").IsRequired();

           }
    }
}