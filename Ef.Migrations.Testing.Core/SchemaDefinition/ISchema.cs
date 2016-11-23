using System.Collections;
using System.Collections.Generic;

namespace Ef.Migrations.Testing.Core.SchemaDefinition
{
    public interface ISchema
    {
        IList<ITable> Tables { get; }
    }
}