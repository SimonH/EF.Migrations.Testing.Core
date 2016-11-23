using System.Data.Entity.Migrations.Model;
using Ef.Migrations.Testing.Core.SchemaDefinition;

namespace Ef.Migrations.Testing.Core.Commands
{
    public abstract class CommandBase<T> : ISchemaCommand
        where T : MigrationOperation
    {
        public void Execute(object source, ISchema target)
        {
            var operation = source as T;
            if (operation == null)
            {
                // what to do here exception or let slide (or allow choice based on strict behaviour)
            }
            ExecuteInternal(operation, target);
        }

        protected abstract void ExecuteInternal(T operation, ISchema target);
    }
}