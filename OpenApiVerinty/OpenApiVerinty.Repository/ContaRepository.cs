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
    public class ContaRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(ContaRepository));
        private readonly string className = typeof(ContaRepository).Name;
        public List<string> Notificacoes = new List<string>();

        public bool CriarConta(Cliente cliente) 
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession()) 
                {
                    ClienteRepository clienteRepository = new ClienteRepository();
                    var result = clienteRepository.ClienteExiste(cliente.Documento, session);

                    if (!(result is null))
                    {
                        ContaDAO contaDAO = new ContaDAO(session);

                        var contaverificacao = contaDAO.GetByIdClient(result.Identificador);

                        if (contaverificacao is null)
                        {
                            var EhValido = contaDAO.Add(new Conta()
                            {
                                Id = Guid.NewGuid().ToString(),
                                IdCliente = result.Identificador,
                                DataCadastro = DateTime.Now,
                                Tipo = TipoConta.FISICA
                            });

                            if (!EhValido)
                            {
                                Notificacoes.Add("Não foi possível realizar a operação");
                            }
                            return EhValido;
                        }
                        else 
                        {
                            Notificacoes.Add("Já existe uma conta atralada ao cliente");
                            return false;
                        }
                    }
                    else 
                    {
                        Notificacoes = clienteRepository.Notificacoes;
                        Notificacoes.Add("Cliente não encontrado");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:CriarConta", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return false;
            }
        }

        public Conta ConsultarContar(Cliente cliente) 
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession()) 
                {
                    ContaDAO contaDAO = new ContaDAO(session);

                    var conta = contaDAO.GetByIdClient(cliente.Identificador);

                    if (conta is null) 
                    {
                        Notificacoes.Add($"Não existe uma conta atrelada a esse cliente");
                        return null;
                    }
                    return conta;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:ConsultarContar", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;
            }
        }
    }
}
