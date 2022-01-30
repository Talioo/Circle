using System;

namespace Common.Factories
{
    public interface IViewFactory
    {
        IView GetView(Type controllerType);
    }
}