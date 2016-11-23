using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Migrations.Model;

namespace Ef.Migrations.Testing.Core.SchemaDefinition
{
    public class Table : ITable
    {
        private readonly List<ColumnModel> _columns = new List<ColumnModel>();

        public Table(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        public string Name { get; }
        public IList<ColumnModel> Columns => _columns;
    }
}