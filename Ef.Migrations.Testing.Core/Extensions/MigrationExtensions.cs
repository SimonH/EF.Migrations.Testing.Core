using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations.Model;
using System.Linq;

namespace Ef.Migrations.Testing.Core.Extensions
{
    public static class MigrationExtensions
    {
        public static string GetMatchingMigrationId(this IEnumerable<string> migrationIds, string migrationName)
        {
            if (string.IsNullOrWhiteSpace(migrationName) || migrationIds == null)
            {
                return null;
            }
            return migrationIds.FirstOrDefault(s => s.Equals(migrationName, StringComparison.OrdinalIgnoreCase)) 
                ?? migrationIds.FirstOrDefault(s => s.Substring(16).Equals(migrationName, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsApplicable(this string migrationId, bool isUp, string sourceMigrationId, string targetMigrationId)
        {
            return (isUp
                    && (sourceMigrationId == null || string.Compare(migrationId, sourceMigrationId, StringComparison.OrdinalIgnoreCase) > 0)
                    && string.Compare(migrationId, targetMigrationId, StringComparison.OrdinalIgnoreCase) <= 0) 
                || (!isUp
                    && string.Compare(migrationId, sourceMigrationId, StringComparison.OrdinalIgnoreCase) <= 0
                    && string.Compare(migrationId, targetMigrationId, StringComparison.OrdinalIgnoreCase) > 0);
        } 
    }
}