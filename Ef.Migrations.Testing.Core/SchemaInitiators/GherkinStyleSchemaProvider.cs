using System.IO;
using Ef.Migrations.Testing.Core.Interfaces;

namespace Ef.Migrations.Testing.Core.SchemaInitiators
{
    public class GherkinStyleSchemaProvider : ISchemaProvider
    {
        public void Load(FileInfo fileInfo)
        {
        }

        public void Load(Stream stream)
        {
        }

        public void Load(string text)
        {
        }

        public void Load(string[] lines)
        {
        }
    }
}