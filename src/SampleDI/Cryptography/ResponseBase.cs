﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Cryptography
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
