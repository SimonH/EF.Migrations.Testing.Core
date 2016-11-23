using System.Collections.Generic;

namespace Ef.Migrations.Testing.Core.SchemaDefinition
{
    public class Schema : ISchema
    {
        private readonly List<ITable> _tables = new List<ITable>();
        public IList<ITable> Tables => _tables;
    }
}