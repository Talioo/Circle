using System;
using Common;
using UnityEngine;

namespace Game.BonusesSystem.Bonus
{
    public class BonusView : BaseView
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        public event Action TouchEvent;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        private void OnCollisionEnter(Collision collision)
        {
            TouchEvent?.Invoke();
        }
    }
}