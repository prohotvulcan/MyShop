﻿using System;

namespace MyShop.Common.Exceptions
{
    public class NameDuplicatedException : Exception
    {
        public NameDuplicatedException()
        {

        }

        public NameDuplicatedException(string message) : base(message)
        {

        }

        public NameDuplicatedException(string message, Exception innerEx) : base(message, innerEx)
        {

        }
    }
}
