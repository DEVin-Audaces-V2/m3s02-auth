using m3s02_auth.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace m3s02_auth.DataBase
{
    public class DbContexto : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Db-auth");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(x => x.Login);
            modelBuilder.Entity<Usuario>().Property(x => x.Login)
                                           .HasMaxLength(50);

            modelBuilder.Entity<Usuario>().Property(x => x.Senha)
                                          .HasMaxLength(50);
        }
    }
}
