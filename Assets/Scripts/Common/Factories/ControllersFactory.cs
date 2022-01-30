using System;
using System.Collections.Generic;
using Zenject;

namespace Common.Factories
{
    public class ControllersFactory : IControllersFactory
    {
        private readonly DiContainer _container;

        public ControllersFactory(DiContainer container)
        {
            _container = container;
        }

        public IController GetController(Type type, IView view, IModel model)
        {
            var valuePair = new List<TypeValuePair>
            {
                new TypeValuePair(typeof(IView), view),
                new TypeValuePair(typeof(IModel), model),
            };

            return _container.InstantiateExplicit(type, valuePair) as IController;
        }
    }
}