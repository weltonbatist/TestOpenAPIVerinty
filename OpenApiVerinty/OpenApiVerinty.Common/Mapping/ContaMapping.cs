using FluentNHibernate.Mapping;
using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Mapping
{
    public class ContaMapping: ClassMap<Conta>
    {
        public ContaMapping()
        {
            Id(x => x.Id).CustomSqlType("varchar(40)");
            Map(x => x.IdCliente).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.Tipo).Not.Nullable();
        }
    }
}
