using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Middleware.Exceptions
{
    public class ForbiddenOperationException: Exception
    {
        public ForbiddenOperationException(string message) : base(message)
        {
        }
    }
}
