using System;
using System.Collections.Generic;
using System.Text;

namespace RihaInterpreterLibrary.Processor
{
    public interface IProcessor
    {
        public void Process(List<string> lines);
    }
}
