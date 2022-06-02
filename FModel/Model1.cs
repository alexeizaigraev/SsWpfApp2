using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SsWpfApp2
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<T_Department> T_Department { get; set; }
        public virtual DbSet<T_Ekv> T_Ekv { get; set; }
        public virtual DbSet<T_Logi> T_Logi { get; set; }
        public virtual DbSet<T_Otbor> T_Otbor { get; set; }
        public virtual DbSet<T_Terminal> T_Terminal { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
