using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    public enum ValueType
    {
        Number,
        Text,
        Boolean,
        Array,
        Auto,
        Function,
    }

    public enum ScopeType
    {
        Check,
        While
    }
}
