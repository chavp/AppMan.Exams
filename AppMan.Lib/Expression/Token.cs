using System;
using System.Collections.Generic;
using System.Text;

namespace AppMan.Lib.Expression
{
    public enum Token
    {
        EOF,
        Add,
        Subtract,
        Number,
        Multiply,
        Divide,
        OpenParents,
        CloseParents
    }
}
