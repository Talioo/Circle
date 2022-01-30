using System;

namespace Common.Factories
{
    public interface IControllersFactory
    {
        IController GetController(Type type, IView view, IModel model);
    }
}