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
    public class TransacaoDAO
    {
        private readonly ISession _session;
        private readonly ILog logger = LogManager.GetLogger(typeof(TransacaoDAO));
        private readonly string className = typeof(TransacaoDAO).Name;

        public TransacaoDAO(ISession session)
        {
            _session = session;
        }
        public bool Add(Transacao transacao) 
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(transacao);
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

        public List<Transacao> GetbyInterval(DateTime startdate, DateTime enddate, Conta conta) 
        {
            return _session.Query<Transacao>().Where(x => x.DataEvento >= startdate && x.DataEvento <= enddate && x.IdConta == conta.Id).ToList();
        }

        public Transacao GetValue(Conta conta) 
        {
            var result =  _session.Query<Transacao>().Where(x => x.IdConta == conta.Id).ToList();

            if (result != null) 
            {
                return result.OrderByDescending(x => x.DataEvento).FirstOrDefault();
            }

            return null;
        }
    }
}
