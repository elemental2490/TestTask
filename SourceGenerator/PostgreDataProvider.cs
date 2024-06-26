using System.Collections.Generic;
using System.Linq;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace SourceGenerator
{
    public class PostgreDataProvider
    {
        public IEnumerable<string> GetData()
        {
            using var con =
                   new NpgsqlConnection("Host=localhost:5432;Username=postgres;Password=1234;Database=db");
            var pc = new PostgresCompiler();
            var db = new QueryFactory(con, pc);

            return db.Query("Classes")
                .Select("name")
                .Get<string>()
                .Select(x => x.Trim());
        }
    }
}