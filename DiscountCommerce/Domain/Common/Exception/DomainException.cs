using System;
namespace Domain.Common.Exception
{
    public class DomainException: System.Exception
    {
        public DomainException(string msg) : base(msg){}
    }
}