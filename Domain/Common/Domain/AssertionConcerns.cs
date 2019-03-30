using System;
using System.Collections.Generic;
using Domain.Common.Exception;

namespace Domain.Common.Domain
{
    public class AssertionConcerns
    {
        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }

        public static void AssertStringNotEmpty(string str, string message)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new DomainException(message);
            }
        }
        public static void AssertArugmentNotNull<T>(T arg, string message)
        {
            if (arg == null)
            {
                throw new DomainException(message);
            }
        }

        public static void AssertArgumentNotIn<T>(T arg, IList<T> list, string message)
        {
            if (list.Contains(arg))
            {
                throw new DomainException(message);
            }
        }
        
        public static void AssertArgumentRange(Decimal arg, Decimal minimum, Decimal maximum, string message)
        {
            if (arg < minimum || arg > maximum)
            {
                throw new DomainException(message);
            }
        }
    }
}