using FluentNHibernate.Mapping;
using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Mapping
{
    public class ClienteMapping : ClassMap<Cliente>
    {
        public ClienteMapping()
        {
            Id(x => x.Identificador).CustomSqlType("varchar(40)");
            Map(x => x.Nome).Length(80);
            Map(x => x.Sobrenome).Length(150);
            Map(x => x.Documento).Not.Nullable();
            Map(x => x.DataNascimento);
            Map(x => x.DataCadastro);
        }
    }
}
