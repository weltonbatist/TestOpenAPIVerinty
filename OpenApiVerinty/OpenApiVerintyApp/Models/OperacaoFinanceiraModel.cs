using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenApiVerintyApp.Models
{
    public class OperacaoFinanceiraModel
    {
        public Conta conta { get; set; }
        public decimal valor { get; set; }
    }
}