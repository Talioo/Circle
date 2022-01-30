using System;
using System.Threading.Tasks;
using Common;
using DefaultNamespace;
using DG.Tweening;
using Map.InputSystem;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterController : BaseController<CharacterView, CharacterModel>, IDisposable
    {
        private readonly IInputSystem _inputSystem;

        private int _pathPointer = 0;

        public CharacterController(IView view, IModel model, IInputSystem inputSystem)
            : base(view, model)
        {
            _inputSystem = inputSystem;
            Subscribe();
        }

        public void Dispose()
        {
            _inputSystem.PathStartedEvent -= Move;
        }

        private void Subscribe()
        {
            _inputSystem.PathStartedEvent += Move;
        }
        
        private void Move()
        {
            if (_inputSystem.Path.Count > _pathPointer)
            {
                var point = _inputSystem.Path[_pathPointer];
                point.y = View.transform.position.y;
                View.transform.DOMove(point, 1f).OnComplete(Move);
                _pathPointer++;
            }
            else
            {
                _pathPointer = 0;
            }
        }
    }
}