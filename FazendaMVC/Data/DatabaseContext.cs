using FazendaMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FazendaMVC.Data
{
    public class DatabaseContext: DbContext
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Fazenda");
        }

        public DbSet<FazendaViewModel> Fazendas { get; set; }
        public DbSet<AnimalViewModel> Animais { get; set; }
    }
}
