using FluentNHibernate.Mapping;
using OpenApiVerinty.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Mapping
{
    public class LogMapping: ClassMap<Log>
    {
        public LogMapping()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Date);
            Map(x => x.Level);
            Map(x => x.Logger);
            Map(x => x.Thread);
            Map(x => x.Message).Length(10000);
            Map(x => x.Exception).Length(10000);
        }
    }
}
