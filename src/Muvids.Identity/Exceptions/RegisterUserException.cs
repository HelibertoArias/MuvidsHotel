using System;

namespace Muvids.Identity.Exceptions;


public class RegisterUserException : ApplicationException
{
    public RegisterUserException(string message) : base(message)
    {

    }
}