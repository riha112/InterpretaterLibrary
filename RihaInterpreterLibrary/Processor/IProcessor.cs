using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Processor
{
    /// <summary>
    /// Does work after all translations are done with code,
    /// Doesn't modify any codes data.
    /// </summary>
    public interface IProcessor
    {
        public void Process(List<string> lines);
    }
}
