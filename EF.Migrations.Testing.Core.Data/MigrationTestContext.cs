using System.Data.Entity;
using EF.Migrations.Testing.Core.Data.Model;

namespace EF.Migrations.Testing.Core.Data
{
    public class MigrationTestContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
    }
}