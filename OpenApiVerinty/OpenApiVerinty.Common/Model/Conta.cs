using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Model
{
    public enum TipoConta
    {
        FISICA = 0,
        JURIDICA = 1
    }
    public class Conta
    {
        public virtual string Id { get; set; }
        public virtual string IdCliente { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual TipoConta Tipo { get; set; }

    }
}
