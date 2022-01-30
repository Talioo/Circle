using System;
using Common;

namespace Signals
{
    public class ShowElementSignal
    {
        public Type Type { get; set; }
        public IModel Model { get; set; }
    }
}