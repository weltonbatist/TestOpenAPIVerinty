using log4net;
using NHibernate;
using OpenApiVerinty.Common.DAO;
using OpenApiVerinty.Common.Helper;
using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Repository
{
    public class ClienteRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(ClienteRepository));
        private readonly string className = typeof(ClienteRepository).Name;
        public List<string> Notificacoes = new List<string>();
        public Cliente ClienteExiste(string documento, ISession session)
        {
            try
            {
                ClienteDAO dao = new ClienteDAO(session);

                return dao.Get(documento);

            }
            catch (Exception ex)
            {
                logger.Error($"{className}:ClienteExiste", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;

            }
        }

        public bool SalvarCliente(Cliente cliente)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ClienteDAO dao = new ClienteDAO(session);

                    if (ClienteExiste(cliente.Documento, session) is null)
                    {
                        cliente.Identificador = Guid.NewGuid().ToString();

                        if (dao.Add(cliente))
                        {
                            return true;
                        }
                        else
                        {
                            Notificacoes.Add("Não foi possível realizar a operação");
                            return false;
                        }
                    }
                    else 
                    {
                        Notificacoes.Add("Já existe um cadastro identificado em nossa base de dados");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:SalvarCliente", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return false;

            }
        }

        public Cliente AtualizarCliente(Cliente cliente)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ClienteDAO dao = new ClienteDAO(session);

                    if (!(ClienteExiste(cliente.Documento, session) is null))
                    {
                        var clienteatualizado = dao.Update(cliente);

                        if (clienteatualizado is null)
                        {
                            Notificacoes.Add("Não foi possível realizar a operação de atualização");
                            return null;
                        }
                        else
                        {
                            
                            return clienteatualizado;
                        }
                    }
                    else
                    {
                        Notificacoes.Add("Cliente não identificado em nossa base de dados");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:AtualizarCliente", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;

            }
        }

        public Cliente RetornaCliente(string documento)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ClienteDAO dao = new ClienteDAO(session);
                    var result = dao.Get(documento);

                    if (result is null) 
                    {
                        Notificacoes.Add($"Cliente não encontrado");
                        return null;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:AtualizarCliente", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;

            }
        }

        public Cliente RetornaClientePorId(string id)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ClienteDAO dao = new ClienteDAO(session);
                    var result = dao.GetById(id);

                    if (result is null)
                    {
                        Notificacoes.Add($"Cliente não encontrado");
                        return null;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:AtualizarCliente", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;

            }
        }
    }
}
