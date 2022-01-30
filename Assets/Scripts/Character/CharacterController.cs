using System;
using System.Threading.Tasks;
using Common;
using DG.Tweening;
using Map.InputSystem;
using UnityEngine;

namespace Character
{
    public class CharacterController : BaseController<CharacterView, CharacterModel>, IDisposable
    {
        private const float Speed = 60f;
        
        private readonly IInputSystem _inputSystem;

        private int _pathPointer = 0;
        private Tween _moveTween;

        public CharacterController(IView view, IModel model, IInputSystem inputSystem)
            : base(view, model)
        {
            _inputSystem = inputSystem;
            Subscribe();
        }

        public void Dispose()
        {
            _inputSystem.PathStartedEvent -= StartMoving;
            View.ClickEvent -= OnViewClicked;
        }

        private void Subscribe()
        {
            _inputSystem.PathStartedEvent += StartMoving;
            View.ClickEvent += OnViewClicked;
        }

        private void StartMoving()
        {
            Reset();
            Move();
        }

        private async void OnViewClicked()
        {
            await Task.Yield();//to make sure that this event would be after PathStartedEvent
            Reset();
        }

        private void Reset()
        {
            _moveTween?.Kill();
            _pathPointer = 0;
        }
        
        private void Move()
        {
            if (_inputSystem.Path.Count > _pathPointer)
            {
                var point = _inputSystem.Path[_pathPointer];
                point.y = View.transform.position.y;
                var distance = Vector3.Distance(point, View.transform.position);
                _moveTween = View.transform.DOMove(point, distance/Speed).SetEase(Ease.Linear).OnComplete(Move);
                _pathPointer++;
            }
            else
            {
                _pathPointer = 0;
            }
        }
    }
}