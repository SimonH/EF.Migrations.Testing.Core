using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ef.Migrations.Testing.Core.Extensions
{
    public static class MigrationExtensions
    {
        public static string GetMatchingMigrationId(this List<string> migrationIds, string migrationName)
        {
            if (string.IsNullOrWhiteSpace(migrationName) || migrationIds == null)
            {
                return null;
            }
            return migrationIds.FirstOrDefault(s => s.Equals(migrationName, StringComparison.OrdinalIgnoreCase)) 
                ?? migrationIds.FirstOrDefault(s => s.Substring(16).Equals(migrationName, StringComparison.OrdinalIgnoreCase));
        }
    }
}