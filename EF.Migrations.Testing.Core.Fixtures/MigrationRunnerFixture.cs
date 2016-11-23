using System.Runtime.InteropServices;
using Ef.Migrations.Testing.Core.Testing;
using EF.Migrations.Testing.Core.Data.Migrations;
using NUnit.Framework;

namespace EF.Migrations.Testing.Core.Fixtures
{
    [TestFixture]
    public class MigrationRunnerFixture
    {
        [Test]
        public void Migrate()
        {
            var schema = MigrationRunner.Migrate(new Configuration(), null, "InitialCreate");
            Assert.That(schema.Tables.Count, Is.EqualTo(1));
            Assert.That(schema.Tables[0].Columns.Count, Is.EqualTo(2));
        }
    }
}