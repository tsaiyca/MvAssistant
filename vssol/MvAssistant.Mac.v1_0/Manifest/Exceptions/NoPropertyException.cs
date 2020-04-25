﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class NoPropertyException : Exception
    {
        public NoPropertyException()
        {
        }

        public NoPropertyException(string message)
            : base(message)
        {
        }

        public NoPropertyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}