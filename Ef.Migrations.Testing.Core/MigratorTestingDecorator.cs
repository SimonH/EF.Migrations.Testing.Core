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
        private readonly DbMigrator _innerMigrator;
        private readonly List<MigrationOperation> _operations = new List<MigrationOperation>();
        public MigratorTestingDecorator(DbMigrator innerMigrator) : base(innerMigrator)
        {
            if (innerMigrator == null)
            {
                throw new ArgumentNullException(nameof(innerMigrator));
            }
            this._innerMigrator = innerMigrator;
        }

        private IEnumerable<MigrationOperation> ApplyMigration(string migrationId, bool isUp)
        {
            var types = _innerMigrator.Configuration.GetType().Assembly.GetTypes();
            var type = _innerMigrator.Configuration.GetType().Assembly.GetTypes().FirstOrDefault(t => string.Equals(t.Name, migrationId.Substring(16), StringComparison.OrdinalIgnoreCase));
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
            _operations.Clear();
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

            if (!string.IsNullOrWhiteSpace(sourceMigration) && sourceMigrationId == null)
            {
                throw new ArgumentOutOfRangeException("You must supply a valid source migration or null");
            }

            if (sourceMigrationId != null && string.Equals(sourceMigrationId, targetMigrationId)) // nothing to see or do here 
            {
                return;
            }
            var isUp = !(sourceMigrationId != null && string.Compare(sourceMigrationId, targetMigrationId, StringComparison.OrdinalIgnoreCase) > 0);
            if (!isUp)
            {
                migrationIds.Reverse();
            }
            _operations.AddRange(migrationIds.Where(s => s.IsApplicable(isUp, sourceMigrationId, targetMigrationId)).SelectMany(s => ApplyMigration(s, isUp)));
        }

        public IEnumerable<MigrationOperation> Operations => _operations;
    }
}