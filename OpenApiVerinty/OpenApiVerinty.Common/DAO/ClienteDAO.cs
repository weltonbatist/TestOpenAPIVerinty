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
    public class ClienteDAO
    {
        private readonly ISession _session;
        private readonly ILog logger = LogManager.GetLogger(typeof(ClienteDAO));
        private readonly string className = typeof(ClienteDAO).Name;

        public ClienteDAO(ISession session)
        {
            this._session = session;
        }

        public bool Add(Cliente cliente)
        {

            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    _session.Save(cliente);
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error($"{className}:Add",ex);
                    tx.Rollback();
                    return false;

                }
            }

        }
        public Cliente Get(string documento)
        {
            return _session.Query<Cliente>().Where(x => x.Documento == documento).FirstOrDefault();
        }

        public Cliente GetById(string id)
        {
            return _session.Query<Cliente>().Where(x => x.Identificador == id).FirstOrDefault();
        }

        public Cliente Update(Cliente cliente) 
        {
            using (ITransaction tx = _session.BeginTransaction())
            {
                try
                {
                    var cliente_atualizado = _session.Merge(cliente);
                    tx.Commit();
                    return cliente_atualizado;
                }
                catch (Exception ex)
                {
                    logger.Error($"{className}:Update", ex);
                    tx.Rollback();
                    return null;

                }
            }
        }
    }
}
