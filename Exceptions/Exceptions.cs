using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_dotnet_maui.Exceptions
{
    public class NoKeysException : Exception
    {
        public NoKeysException(string message) : base(message) {}
    }
}
