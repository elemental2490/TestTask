using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DAL
{
    public class SessionFactory
    {
        private static ISessionFactory? _sessionFactory;

        public static ISession OpenSession()
        {
            return _sessionFactory?.OpenSession() ?? throw new InvalidOperationException();
        }

        public static void Init(string connectionString)
        {
            _sessionFactory = BuildSessionFactory(connectionString);
        }

        private static ISessionFactory? BuildSessionFactory(string connectionString)
        {
            return Fluently
                .Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    .ConnectionString(c => c.Is(connectionString))
                    .ShowSql())
                .Mappings(m =>
                {
                    foreach (var classType in GetClasses())
                    {
                        m.FluentMappings.Add(classType);
                    }
                })
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
        }

        private static IEnumerable<Type> GetClasses()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(
                    type => type.Namespace == "TestTaskSolution.Models.Mappings"
                    && type.FullName != null && type.FullName.EndsWith("Map"));
        }
    }
}