using System;

namespace Muvids.Identity.Exceptions;


public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message) : base(message)
    {

    }
}