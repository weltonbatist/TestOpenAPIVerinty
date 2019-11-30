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
    public class GerenciadorCliente
    {

        public TemplateLayoutClienteResponse ConsultarCliente(string documento)
        {
            ClienteRepository clienteRepository = new ClienteRepository();

            var result = clienteRepository.RetornaCliente(documento);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = HelpSerialize<Cliente>.SerializeObject(result),
                Notificacoes = string.Join(";", clienteRepository.Notificacoes),
                Status = result == null ? false:true
            };

        }

        public TemplateLayoutClienteResponse CadastrarCliente(Cliente cliente) 
        {
            ClienteRepository clienteRepository = new ClienteRepository();

            var result = clienteRepository.SalvarCliente(cliente);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = null,
                Notificacoes = string.Join(";", clienteRepository.Notificacoes),
                Status = result
            }; 

        }

        public TemplateLayoutClienteResponse AtualizarrCliente(Cliente cliente)
        {
            ClienteRepository clienteRepository = new ClienteRepository();

            var result = clienteRepository.AtualizarCliente(cliente);

            return new TemplateLayoutClienteResponse
            {
                DataHora = DateTime.Now,
                Dados = HelpSerialize<Cliente>.SerializeObject(result),
                Notificacoes = string.Join(";", clienteRepository.Notificacoes),
                Status = result is null ? false : true
            };

        }


    }
}
