using log4net;
using OpenApiVerinty.Domain.Kernel;
using OpenApiVerintyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenApiVerintyApp.Controllers
{
    public class OperacaoFinanceiraController : ApiController
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(OperacaoFinanceiraController));
        private readonly string className = typeof(OperacaoFinanceiraController).Name;


        [Route("api/operacao/sacar")]
        [HttpPost]
        public HttpResponseMessage Sacar(OperacaoFinanceiraModel operacao)
        {
            try
            {
                if (operacao is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo operacao não pode ser nulo");
                }
                else if (operacao.valor <= 0) 
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Não é possivel sacar valores negativos");
                }
                else
                {
                    GerenciadorConta gerenciadorTransacao = new GerenciadorConta();


                    var result = gerenciadorTransacao.Sacar(operacao.conta, operacao.valor);

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
                logger.Error($"{className}:Sacar", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }

        [Route("api/operacao/depositaremconta")]
        [HttpPost]
        public HttpResponseMessage DepositarNaConta(OperacaoFinanceiraModel operacao)
        {
            try
            {
                if (operacao is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo operacao não pode ser nulo");
                }
                else if (operacao.valor <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Não é possivel depositar valores negativos");
                }
                else
                {
                    GerenciadorConta gerenciadorTransacao = new GerenciadorConta();


                    var result = gerenciadorTransacao.Depositar(operacao.conta, operacao.valor);

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
                logger.Error($"{className}:Depositar", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }

        [Route("api/operacao/extratodaconta")]
        [HttpPost]
        public HttpResponseMessage ExtratoFinanceiro(ExtratoFinanceiroModel extratoFinanceiro)
        {
            try
            {
                if (extratoFinanceiro is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo extratoFinanceiro não pode ser nulo");
                }
                else if (extratoFinanceiro.conta is null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Tipo extratoFinanceiro.conta não pode ser nulo");
                }
                else if (extratoFinanceiro.inicio > extratoFinanceiro.fim)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Data inicio não pode ser maior que a data fim");
                }
                else
                {
                    GerenciadorConta gerenciadorTransacao = new GerenciadorConta();

                    var result = gerenciadorTransacao.Extrato(extratoFinanceiro.conta, extratoFinanceiro.inicio, extratoFinanceiro.fim);

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
                logger.Error($"{className}:Depositar", ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ops! Nossos servidores estão com problemas, tente mais tarde!");
            }
        }
    }
}
