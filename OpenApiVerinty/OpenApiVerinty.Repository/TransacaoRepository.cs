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
    public class TransacaoRepository
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(TransacaoRepository));
        private readonly string className = typeof(TransacaoRepository).Name;
        public List<string> Notificacoes = new List<string>();

        public Transacao Sacar(Conta conta, decimal valor) 
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ClienteRepository clienteRepository = new ClienteRepository();

                    var result = clienteRepository.RetornaClientePorId(conta.IdCliente);
                    
                    if (!(result is null))
                    {
                        TransacaoDAO transacaoDAO = new TransacaoDAO(session);

                        var value = transacaoDAO.GetValue(conta);

                        if (!(value is null))
                        {
                            if (value.Valor < valor)
                            {
                                Notificacoes.Add("Saldo insuficiente");
                                return null;
                            }
                            else 
                            {
                                Transacao transacao = new Transacao();
                                transacao.IdTransacao = Guid.NewGuid().ToString();
                                transacao.Valor = value.Valor - valor;
                                transacao.IdConta = conta.Id;
                                transacao.DataEvento = DateTime.Now;
                                transacao.Tipo = TipoOperacao.SAIDA;

                                if (transacaoDAO.Add(transacao))
                                {
                                    var ultimaTransacao = transacaoDAO.GetValue(conta);

                                    return ultimaTransacao;
                                }
                                else 
                                {
                                    Notificacoes.Add("No momento não foi possível efetuar a operação");
                                    return null;
                                }
                            }
                        }
                        else
                        {
                            Notificacoes.Add("Saldo insuficiente");
                            return null;
                        }
                    }
                    else
                    {
                        Notificacoes = clienteRepository.Notificacoes;
                        Notificacoes.Add("Não foi possível realizar a operação, cliente não encontrado");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:Sacar", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;
            }
        }

        public Transacao DepositarDinheiro(Conta conta, decimal valor)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ClienteRepository clienteRepository = new ClienteRepository();
                    var result = clienteRepository.RetornaClientePorId(conta.IdCliente);

                    if (!(result is null))
                    {
                        TransacaoDAO transacaoDAO = new TransacaoDAO(session);

                        var ultimaTransacaobanco = transacaoDAO.GetValue(conta);

                        Transacao transacao = new Transacao();
                        transacao.IdTransacao = Guid.NewGuid().ToString();
                        transacao.Valor = ultimaTransacaobanco is null? valor: ultimaTransacaobanco.Valor + valor;
                        transacao.IdConta = conta.Id;
                        transacao.DataEvento = DateTime.Now;
                        transacao.Tipo = TipoOperacao.ENTRADA;

                        if (transacaoDAO.Add(transacao))
                        {
                            var ultimaTransacao = transacaoDAO.GetValue(conta);

                            return ultimaTransacao;
                        }
                        else
                        {
                            Notificacoes.Add("No momento não foi possível efetuar a operação");
                            return null;
                        }
                    }
                    else
                    {
                        Notificacoes = clienteRepository.Notificacoes;
                        Notificacoes.Add("Não foi possível realizar a operação, cliente não encontrado");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:Depositar", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;
            }

        }

        public List<Transacao> Extrato(Conta conta, DateTime Inicio, DateTime Fim)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ClienteRepository clienteRepository = new ClienteRepository();
                    var result = clienteRepository.RetornaClientePorId(conta.IdCliente);

                    if (!(result is null))
                    {
                        TransacaoDAO transacaoDAO = new TransacaoDAO(session);

                        var extratoTransacao = transacaoDAO.GetbyInterval(Inicio, Fim, conta);

                        if (extratoTransacao is null || extratoTransacao.Count() <= 0)
                        {
                            Notificacoes.Add("Nenhuma transação foi encontrada");
                            return null;
                        }
                        else 
                        {
                            return extratoTransacao;
                        }
                    }
                    else
                    {
                        Notificacoes = clienteRepository.Notificacoes;
                        Notificacoes.Add("Não foi possível realizar a operação, cliente não encontrado");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{className}:Sacar", ex);
                Notificacoes.Add($"Ocorreu uma falha interna ao realizar a operação");
                return null;
            }
        }
    }
}
