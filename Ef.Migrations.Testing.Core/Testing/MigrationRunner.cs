using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Diagnostics;
using System.Linq;
using Ef.Migrations.Testing.Core.Commands;
using Ef.Migrations.Testing.Core.SchemaDefinition;

namespace Ef.Migrations.Testing.Core.Testing
{
    public class MigrationRunner : MigrationRunnerBase
    {
        public static ISchema Migrate(DbMigrationsConfiguration configuration, string sourceMigration, string targetMigration)
        {
            var migrator = new MigratorTestingDecorator(new DbMigrator(configuration));
            migrator.RunMigration(sourceMigration, targetMigration);
            var schema = new Schema();
            RunOperations(migrator.Operations, schema);
            return schema;
        }

        private static void RunOperations(IEnumerable<MigrationOperation> operations, ISchema target)
        {
            foreach(var operation in operations)
            {
                Commands.FirstOrDefault(c => string.Equals(c.Metadata.Name, operation.GetType().Name, StringComparison.OrdinalIgnoreCase))
                    ?.Value
                    ?.Execute(operation, target);
            }
        }
    }
}