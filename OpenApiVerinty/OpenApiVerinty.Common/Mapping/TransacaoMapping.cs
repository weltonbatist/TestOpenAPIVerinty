using FluentNHibernate.Mapping;
using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Mapping
{
    public class TransacaoMapping: ClassMap<Transacao>
    {
        public TransacaoMapping()
        {
            Id(x => x.IdTransacao).CustomSqlType("varchar(40)");
            Map(x => x.IdConta).Not.Nullable();
            Map(x => x.Tipo).Not.Nullable();
            Map(x => x.Valor).Not.Nullable();
            Map(x => x.DataEvento).Default("getdate()").Not.Nullable();
        }
    }
}
