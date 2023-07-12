using System;

namespace m3s02_auth.Exceptions
{
    public class NotFoundException:  Exception
    {
        public NotFoundException(string message): base (message)
        {
                
        }
    }
}
