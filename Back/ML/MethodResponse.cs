using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class MethodResponse <T>
    {
        public int Code { get; set; }
        public string Messagge { get; set; }
        public T Result { get; set; }
    }
}
