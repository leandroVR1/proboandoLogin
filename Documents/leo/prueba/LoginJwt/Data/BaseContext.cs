using Microsoft.EntityFrameworkCore;
using LoginJwt.Models;

namespace LoginJwt.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options): base(options)
        { }
        //public DbSet<Cupon> Cupones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}

