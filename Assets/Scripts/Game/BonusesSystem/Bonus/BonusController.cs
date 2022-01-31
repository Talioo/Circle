using Common;
using DG.Tweening;
using Signals;
using UnityEngine;
using Zenject;

namespace Game.BonusesSystem.Bonus
{
    public class BonusController : BaseController<BonusView, BonusModel>
    {
        private readonly GameConfiguration _gameConfiguration;
        private Sequence _colorSequence;
        
        public BonusController(IView view, IModel model, SignalBus signalBus, GameConfiguration gameConfiguration)
            : base(view, model, signalBus)
        {
            _gameConfiguration = gameConfiguration;
        }

        public override void OnSpawn()
        {
            base.OnSpawn();
            View.SpriteRenderer.color = Color.clear;
        }

        public void SetData(int points, float lifetime, Vector3 position)
        {
            Model.Lifetime = lifetime;
            Model.Points = points;
            Model.Position = position;
        }

        public void Show()
        {
            View.TouchEvent += Collect;
            View.transform.position = Model.Position;
            View.gameObject.SetActive(true);
            _colorSequence = DOTween.Sequence();
            var color = GetColor();
            
            _colorSequence.Append(View.SpriteRenderer.DOColor(color, Model.Lifetime / 3))
                          .AppendInterval(Model.Lifetime / 3)
                          .Append(View.SpriteRenderer.DOColor(Color.clear, Model.Lifetime / 3))
                          .AppendCallback(Clear);
        }

        private Color GetColor()
        {
            float diffPoints = _gameConfiguration.BonusConfiguration.MaxPoints -
                               _gameConfiguration.BonusConfiguration.MinPoints;
            float currentPointPosition = Model.Points - _gameConfiguration.BonusConfiguration.MinPoints;
            float time = currentPointPosition / diffPoints;

            return _gameConfiguration.BonusConfiguration.Gradient.Evaluate(time);
        }

        private void Collect()
        {
            SignalBus.Fire(new AddPointsSignal {Count = Model.Points});
            Clear();
        }

        private void Clear()
        {
            View.TouchEvent -= Collect;
            View.gameObject.SetActive(false);
            View.SpriteRenderer.color = Color.clear;
            SignalBus.Fire(new BonusWasHiddenSignal {Bonus = this});
            
            _colorSequence?.Kill();
        }
    }
}