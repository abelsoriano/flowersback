using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FlowersBack.Models
{
    public class DataContext: DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }
        //evitando el borrado en cascada 
        //(si borro las flores que no se lleve el historico de pedidos)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        //public System.Data.Entity.DbSet<FlowersBack.Models.Flower> Flowers { get; set; }
        public DbSet<Flower> Flowers { get; set; }

    }
}