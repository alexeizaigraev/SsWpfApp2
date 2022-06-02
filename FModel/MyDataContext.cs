using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    class MyDataContext : DbContext
    {
        //internal static string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DataBaseExpress\DrmDb.mdf;Integrated Security=True;Connect Timeout=30; MultipleActiveResultSets=True;App=EntityFramework";
        internal static string connectionString = File.ReadAllLines("Config/ConnectionString.txt")[0];
        public MyDataContext() : base(connectionString)
        { }
        public DbSet<T_Otbor> otbors { get; set; }
        public DbSet<T_Department> departments { get; set; }

        public DbSet<T_Terminal> terminals { get; set; }

        public DbSet<T_Logi> logis { get; set; }

        public DbSet<T_Ekv> ekvs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
