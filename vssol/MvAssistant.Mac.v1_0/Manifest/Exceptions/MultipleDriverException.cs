﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class MultipleDriverException : Exception
    {
        public MultipleDriverException()
        {
        }

        public MultipleDriverException(string message)
            : base(message)
        {
        }

        public MultipleDriverException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}