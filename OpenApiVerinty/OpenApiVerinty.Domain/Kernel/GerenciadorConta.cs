using OpenApiVerinty.Common.Model;
using OpenApiVerinty.Common.Util;
using OpenApiVerinty.Domain.ObjectValue;
using OpenApiVerinty.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Domain.Kernel
{
    public class GerenciadorConta
    {
        public TemplateLayoutClienteResponse CadastrarConta(Cliente cliente)
        {
            ContaRepository contaRepository = new ContaRepository();

            var result = contaRepository.CriarConta(cliente);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = null,
                Notificacoes = string.Join(";", contaRepository.Notificacoes),
                Status = result
            };

        }

        public TemplateLayoutClienteResponse Consultar(Cliente cliente)
        {
            ContaRepository contaRepository = new ContaRepository();

            var result = contaRepository.ConsultarContar(cliente);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = HelpSerialize<Conta>.SerializeObject(result),
                Notificacoes = string.Join(";", contaRepository.Notificacoes),
                Status = result is null ? false : true
            };

        }

        public TemplateLayoutClienteResponse Sacar(Conta conta, decimal valor)
        {
            TransacaoRepository transacaoRepository = new TransacaoRepository();

            var result = transacaoRepository.Sacar(conta, valor);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = HelpSerialize<Transacao>.SerializeObject(result),
                Notificacoes = string.Join(";", transacaoRepository.Notificacoes),
                Status = result is null ? false : true
            };

        }

        public TemplateLayoutClienteResponse Depositar(Conta conta, decimal valor)
        {
            TransacaoRepository transacaoRepository = new TransacaoRepository();

            var result = transacaoRepository.DepositarDinheiro(conta, valor);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = HelpSerialize<Transacao>.SerializeObject(result),
                Notificacoes = string.Join(";", transacaoRepository.Notificacoes),
                Status = result is null ? false : true
            };

        }

        public TemplateLayoutClienteResponse Extrato(Conta conta, DateTime inicio, DateTime fim)
        {
            TransacaoRepository transacaoRepository = new TransacaoRepository();

            var result = transacaoRepository.Extrato(conta,inicio,fim);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = HelpSerialize<List<Transacao>>.SerializeObject(result),
                Notificacoes = string.Join(";", transacaoRepository.Notificacoes),
                Status = result is null ? false : true
            };

        }
    }
}
