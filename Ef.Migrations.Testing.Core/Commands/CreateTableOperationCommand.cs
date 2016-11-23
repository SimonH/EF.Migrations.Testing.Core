using System.ComponentModel.Composition;
using System.Data.Entity.Migrations.Model;
using Ef.Migrations.Testing.Core.SchemaDefinition;

namespace Ef.Migrations.Testing.Core.Commands
{
    [Export(typeof(ISchemaCommand))]
    [ExportMetadata("Name", "CreateTableOperation")]
    public class CreateTableOperationCommand : CommandBase<CreateTableOperation>
    {
        protected override void ExecuteInternal(CreateTableOperation operation, ISchema target)
        {
            var table = new Table(operation.Name);
            foreach (var col in operation.Columns)
            {
                table.Columns.Add(col);
            }
            target.Tables.Add(table);
        }
    }
}