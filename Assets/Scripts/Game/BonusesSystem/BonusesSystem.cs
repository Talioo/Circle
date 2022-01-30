using System;
using System.Threading;
using System.Threading.Tasks;
using Game.BonusesSystem.Bonus;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.BonusesSystem
{
    public class BonusesSystem : IDisposable
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly BonusConfiguration _bonusConfiguration;
        private readonly IBonusesPool _bonusesPool;
        private readonly Camera _camera;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public BonusesSystem(GameConfiguration gameConfiguration, IBonusesPool bonusesPool, Camera camera)
        {
            _gameConfiguration = gameConfiguration;
            _bonusesPool = bonusesPool;
            _camera = camera;
            _bonusConfiguration = gameConfiguration.BonusConfiguration;

            var token = _cancellationTokenSource.Token;
            ShowBonus(token);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        private async void ShowBonus(CancellationToken token)
        {
            var lifetime = Random.Range(_bonusConfiguration.MinLifetime, _bonusConfiguration.MaxLifetime);
            var points = Random.Range(_bonusConfiguration.MinPoints, _bonusConfiguration.MaxPoints);
            var x = Random.Range(0, _camera.pixelWidth);
            var y =  Random.Range(0, _camera.pixelHeight);

            var position = _camera.ScreenToWorldPoint(new Vector2(x, y));
            
            var model = new BonusModel()
            {
                Lifetime = lifetime,
                Points = points,
                Position = new Vector3(position.x, 0.1f, position.z)
            };

            var bonus = await _bonusesPool.GetBonus(model, token);
            bonus?.Show();

            await Task.Delay(TimeSpan.FromSeconds(_gameConfiguration.BonusAppearanceTime), token);
            
            if (!token.IsCancellationRequested)
            {
                ShowBonus(token);
            }
        }
    }
}