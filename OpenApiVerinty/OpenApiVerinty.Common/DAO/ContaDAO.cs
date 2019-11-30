using log4net;
using NHibernate;
using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.DAO
{
    public class ContaDAO
    {
        private readonly ISession _session;
        private readonly ILog logger = LogManager.GetLogger(typeof(ContaDAO));
        private readonly string className = typeof(ContaDAO).Name;

        public ContaDAO(ISession session)
        {
            _session = session;
        }
        public bool Add(Conta conta) 
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(conta);
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error($"{className}:Add", ex);
                    tx.Rollback();
                    return false;

                }
            }
        }

        public Conta GetByIdClient(string Idcliente) 
        {
            return _session.Query<Conta>().Where(x => x.IdCliente == Idcliente).FirstOrDefault();
        }
    }
}
