using System;
using System.Collections.Generic;
using System.Text;

namespace MorningFM.Logic.DTOs
{
    public class HttpHandlerException: Exception
    {
        public HttpHandlerException()
        {

        }

        public HttpHandlerException(string message)
            :base(message)
        {

        }
        public HttpHandlerException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
