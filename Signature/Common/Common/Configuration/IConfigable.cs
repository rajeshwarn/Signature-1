﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration
{
    public interface IConfigable
    {
        void SetProperty(string name, string value);
    }
}
