using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Domain.ObjectValue
{
    public class TemplateLayoutClienteResponse
    {
        public bool Status { get; set; }
        public string Notificacoes { get; set; }
        public DateTime DataHora { get; set; }
        public string Dados { get; set; }
    }
}
