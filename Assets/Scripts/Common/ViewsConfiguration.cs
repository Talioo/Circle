using System;
using System.Collections.Generic;
using Game.BonusesSystem.Bonus;
using Game.Character;

namespace Common.Factories
{
    public class ViewsConfiguration : IViewsConfiguration
    {
        private readonly Dictionary<Type, string> _viewPath = new Dictionary<Type, string>()
        {
            {typeof(CharacterController), "Character"},
            {typeof(BonusController), "BonusView"},
        };

        public Dictionary<Type, string> ViewPath => _viewPath;
    }
}