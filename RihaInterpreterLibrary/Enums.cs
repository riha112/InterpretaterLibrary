using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary
{
    // Stores all enum declarations

    public enum ValueType
    {
        Number,
        Text,
        Boolean,
        Array,
        Auto,
        Reference,
        Function,
    }

    public enum ScopeType
    {
        Check,
        While
    }
}
