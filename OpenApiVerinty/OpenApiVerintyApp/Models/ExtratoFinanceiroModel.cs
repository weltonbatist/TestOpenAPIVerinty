using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenApiVerintyApp.Models
{
    public class ExtratoFinanceiroModel
    {
        public Conta conta { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }
    }
}