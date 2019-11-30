using log4net;
using OpenApiVerinty.Common.Model;
using OpenApiVerinty.Common.Util;
using OpenApiVerinty.Domain.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenApiVerintyApp.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(ClienteController));
        private readonly string className = typeof(ClienteController).Name;

        [HttpGet]
        [Route("api/cliente/{documento}")]
        public HttpResponseMessage ConsultaCliente(string documento)
        {
            try
            {
                if (documento is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo documento não pode ser nulo");
                }
                else
                {
                    GerenciadorCliente gerenciadorCliente = new GerenciadorCliente();

                    var result = gerenciadorCliente.ConsultarCliente(documento);

                    if (result.Status)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, result);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error($"{className}:Cadastrar", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }


        [Route("api/cliente/cadastrar")]
        [HttpPost]
        public HttpResponseMessage Cadastrar(Cliente cliente) 
        {
            try
            {
                if (cliente is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo cliente não pode ser nulo");
                }
                else 
                {
                    GerenciadorCliente gerenciadorCliente = new GerenciadorCliente();

                    var result = gerenciadorCliente.CadastrarCliente(cliente);

                    if (result.Status)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, result);
                    }
                    else 
                    {
                        return Request.CreateResponse(HttpStatusCode.Conflict, result);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error($"{className}:Cadastrar", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }

        [Route("api/cliente/atualizar")]
        [HttpPut]
        public HttpResponseMessage Atualizar(Cliente cliente)
        {
            try
            {
                if (cliente is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo cliente não pode ser nulo");
                }
                else
                {
                    GerenciadorCliente gerenciadorCliente = new GerenciadorCliente();

                    var result = gerenciadorCliente.AtualizarrCliente(cliente);

                    if (result.Status)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Conflict, result);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error($"{className}:Atualizar", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }


        [Route("api/cliente/conta/cadastrar")]
        [HttpPost]
        public HttpResponseMessage CadastrarConta(Cliente cliente)
        {
            try
            {
                if (cliente is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo cliente não pode ser nulo");
                }
                else
                {
                    GerenciadorConta gerenciadorConta = new GerenciadorConta();

                    var result = gerenciadorConta.CadastrarConta(cliente);

                    if (result.Status)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Conflict, result);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error($"{className}:Cadastrar", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }

        [Route("api/cliente/conta/consultar")]
        [HttpPost]
        public HttpResponseMessage ConsultarConta(Cliente cliente)
        {
            try
            {
                if (cliente is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo cliente não pode ser nulo");
                }
                else
                {
                    GerenciadorConta gerenciadorConta = new GerenciadorConta();

                    var result = gerenciadorConta.Consultar(cliente);

                    if (result.Status)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Conflict, result);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error($"{className}:ConsultarConta", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }
    }
}
