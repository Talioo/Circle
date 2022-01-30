using System;
using System.Collections.Generic;
using Character;

namespace Common.Factories
{
    public class ViewsConfiguration : IViewsConfiguration
    {
        private readonly Dictionary<Type, string> _viewPath = new Dictionary<Type, string>()
        {
            {typeof(CharacterController), "Character"}
        };

        public Dictionary<Type, string> ViewPath => _viewPath;
    }
}