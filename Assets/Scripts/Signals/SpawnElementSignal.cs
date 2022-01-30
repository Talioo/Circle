using System;
using Common;

namespace Signals
{
    public class SpawnElementSignal
    {
        public Type Type { get; set; }
        public IModel Model { get; set; }
    }
}