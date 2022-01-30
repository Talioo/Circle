using System;
using System.Collections.Generic;

namespace Common.Factories
{
    public interface IViewsConfiguration
    {
        Dictionary<Type, string> ViewPath { get; }
    }
}