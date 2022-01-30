using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Common.Factories
{
    public class ViewFactory : IViewFactory
    {
        private readonly DiContainer _container;
        private readonly IViewsConfiguration _viewsConfiguration;

        public ViewFactory(DiContainer container, IViewsConfiguration viewsConfiguration)
        {
            _container = container;
            _viewsConfiguration = viewsConfiguration;
        }

        public IView GetView(Type controllerType)
        {
            if (!_viewsConfiguration.ViewPath.TryGetValue(controllerType, out var path))
            {
                throw new KeyNotFoundException($"There is no path for {controllerType} in views configuration!");
            }

            var prefab = Resources.Load(path) as GameObject;
            return _container.InstantiatePrefabForComponent<IView>(prefab);
        }
    }
}