using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiVerinty.Common.Model
{
    public class Log
    {

        public virtual Int64 Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Thread { get; set; }
        public virtual string Level { get; set; }
        public virtual string Logger { get; set; }

        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
    }
}
