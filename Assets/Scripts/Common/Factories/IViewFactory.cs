using System;
using UnityEngine;

namespace Common.Factories
{
    public interface IViewFactory
    {
        IView GetView(Type controllerType);
    }
}