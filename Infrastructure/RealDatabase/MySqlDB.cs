using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RealDatabase
{
    public class MySqlDB : DbContext
    {
        public MySqlDB() { }

        public MySqlDB(DbContextOptions<MySqlDB>options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database = animals; Uid = root; Pwd = 1234;");
        //    //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Animals;Uid=root;Pwd=1234;");
        //}
    }
}
