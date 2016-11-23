using System;
using System.Collections.Generic;
using Ef.Migrations.Testing.Core.Commands;
using StructuredData.Common.Container;

namespace Ef.Migrations.Testing.Core.Testing
{
    public class MigrationRunnerBase
    {
        private static IEnumerable<Lazy<ISchemaCommand, IMigrationOperationName>> _commands;

        private static IEnumerable<Lazy<ISchemaCommand, IMigrationOperationName>> GetCommands()
        {
            return ContainerManager.CompositionContainer.GetExports<ISchemaCommand, IMigrationOperationName>();
        }

        protected static IEnumerable<Lazy<ISchemaCommand, IMigrationOperationName>> Commands => _commands ?? GetCommands();
    }
}