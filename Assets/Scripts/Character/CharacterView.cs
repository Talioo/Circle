using System;
using Common;

namespace Character
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