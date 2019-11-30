using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Helper
{
    public class NHibernateHelper
    {
        private static ISessionFactory factory = CreateSessionFactory();

        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                var t = Assembly.GetExecutingAssembly();

                Configuration cfg = new Configuration();
                cfg.Configure();
                NHibernate.Tool.hbm2ddl.SchemaUpdate s = new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg);
                ISessionFactory factory = Fluently.Configure(cfg)
                    .Mappings(x => {
                        x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
                    }).BuildSessionFactory();
                return factory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("CreateSessionFactory error",ex);
            }

        }

        public static ISession OpenSession()
        {
            return factory.OpenSession();
        }
    }
}
