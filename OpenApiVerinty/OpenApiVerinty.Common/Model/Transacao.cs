using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Model
{
    public enum TipoOperacao
    {
        ENTRADA = 0,
        SAIDA = 1
    }
    public class Transacao
    {
        public virtual string IdTransacao { get; set; }
        public virtual string IdConta { get; set; }
        public virtual TipoOperacao Tipo { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual DateTime DataEvento { get; set; }
    }
}
