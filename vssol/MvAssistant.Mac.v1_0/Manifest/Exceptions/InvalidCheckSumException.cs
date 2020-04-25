﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest.Exceptions
{
    public class InvalidCheckSumException : Exception
    {
        public InvalidCheckSumException()
        {
        }

        public InvalidCheckSumException(string message)
            : base(message)
        {
        }

        public InvalidCheckSumException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}