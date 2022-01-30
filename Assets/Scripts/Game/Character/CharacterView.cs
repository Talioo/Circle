using System;
using Common;

namespace Game.Character
{
    public class CharacterView : BaseView
    {
        public event Action ClickEvent;

        private void OnMouseDown()
        {
            ClickEvent?.Invoke();
        }
    }
}