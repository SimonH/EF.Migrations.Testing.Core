using System.Data.Entity.Migrations;
using Ef.Migrations.Testing.Core;
using EF.Migrations.Testing.Core.Data.Migrations;
using NUnit.Framework;

namespace EF.Migrations.Testing.Core.Fixtures
{
    [TestFixture]
    public class MigratorTestingDecoratorFixture
    {
        private MigratorTestingDecorator testMigrator;

        [SetUp]
        public void SetUp()
        {
            testMigrator = new MigratorTestingDecorator(new DbMigrator(new Configuration()));
        }

        [Test]
        public void InitialCreate()
        {
            testMigrator.RunMigration(null, "InitialCreate");
        }
    }
}