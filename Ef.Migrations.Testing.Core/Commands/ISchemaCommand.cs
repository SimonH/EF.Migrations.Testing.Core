using Ef.Migrations.Testing.Core.SchemaDefinition;

namespace Ef.Migrations.Testing.Core.Commands
{
    public interface ISchemaCommand
    {
        void Execute(object source, ISchema target);
    }
}