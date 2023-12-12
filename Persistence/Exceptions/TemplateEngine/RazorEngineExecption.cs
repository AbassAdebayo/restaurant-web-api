using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Exceptions.TemplateEngine
{
    public class RazorEngineExecption : Exception
    {
        public string Message { get; set; }

        public RazorEngineExecption(string message)
            : base(message) { }

        public RazorEngineExecption(string message, Exception innerException)
            : base(message, innerException) { }
        
        
    }
}
