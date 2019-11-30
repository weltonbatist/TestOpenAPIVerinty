using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Model
{
    public class Cliente
    {
        public virtual string Identificador { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Sobrenome { get; set; }
        public virtual string Documento { get; set; }
        public virtual DateTime DataNascimento { get; set; }
        public virtual DateTime DataCadastro { get; set; }
    }
}
