using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Reflection;
using Ef.Migrations.Testing.Core.Extensions;

namespace Ef.Migrations.Testing.Core
{
    public class MigratorTestingDecorator : MigratorBase
    {
        private DbMigrator innerMigrator;
        public MigratorTestingDecorator(DbMigrator innerMigrator) : base(innerMigrator)
        {
            if (innerMigrator == null)
            {
                throw new ArgumentNullException(nameof(innerMigrator));
            }
            this.innerMigrator = innerMigrator;
        }

        private IEnumerable<MigrationOperation> ApplyMigration(string migrationId, bool isUp)
        {
            var types = innerMigrator.Configuration.GetType().Assembly.GetTypes();
            var type = innerMigrator.Configuration.GetType().Assembly.GetTypes().FirstOrDefault(t => string.Equals(t.Name, migrationId.Substring(16), StringComparison.OrdinalIgnoreCase));
            if (type == null)
            {
               throw new InvalidOperationException();
            } 
            var migration = Activator.CreateInstance(type);
            var method = type.GetMethod(isUp ? "Up": "Down");
            method.Invoke(migration, null);
            var property = type.GetProperty("Operations", BindingFlags.NonPublic | BindingFlags.Instance);
            return property.GetValue(migration) as IEnumerable<MigrationOperation>;

        }

        public void RunMigration(string sourceMigration, string targetMigration)
        {
            var migrationIds = GetLocalMigrations().ToList();
            if (migrationIds.Count == 0)
            {
                return;
            }
            var sourceMigrationId = migrationIds.GetMatchingMigrationId(sourceMigration);
            var targetMigrationId = migrationIds.GetMatchingMigrationId(targetMigration);
            if (targetMigrationId == null)
            {
                throw new ArgumentOutOfRangeException("You must supply a valid target migration");
            }

            if (sourceMigrationId != null && string.Equals(sourceMigrationId, targetMigrationId)) // nothing to see here 
            {
                return;
            }
            var isUp = !(sourceMigrationId != null && string.Compare(sourceMigrationId, targetMigrationId, StringComparison.OrdinalIgnoreCase) > 0);
            if (!isUp)
            {
                migrationIds.Reverse();
            }
            var migrationsToApply = new List<string>();
            migrationIds.ForEach(id =>
            {
                if (isUp 
                    && (sourceMigrationId == null || string.Compare(id, sourceMigrationId, StringComparison.OrdinalIgnoreCase) > 0)
                    && string.Compare(id, targetMigrationId, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    migrationsToApply.Add(id);
                }
                if (!isUp 
                    && string.Compare(id, sourceMigrationId, StringComparison.OrdinalIgnoreCase) <= 0
                    && string.Compare(id, targetMigrationId, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    migrationsToApply.Add(id);
                }
            });
            var operations = new List<MigrationOperation>();
            migrationsToApply.ForEach(id => operations.AddRange(ApplyMigration(id, isUp)));
        }
    }
}