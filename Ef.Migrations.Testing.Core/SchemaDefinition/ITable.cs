using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;

namespace Ef.Migrations.Testing.Core.SchemaDefinition
{
    public interface ITable
    {
        string Name { get; }
        IList<ColumnModel> Columns { get; }
    }
}