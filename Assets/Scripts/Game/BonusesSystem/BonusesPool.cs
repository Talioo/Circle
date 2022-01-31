using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.BonusesSystem.Bonus;
using Signals;
using UnityEngine;
using Zenject;

namespace Game.BonusesSystem
{
    public class BonusesPool : IBonusesPool, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly GameConfiguration _gameConfiguration;
        private readonly Queue<BonusController> _bonuses = new Queue<BonusController>();

        private int _spawnedBonusesCount = 0;

        public BonusesPool(SignalBus signalBus, GameConfiguration gameConfiguration)
        {
            _signalBus = signalBus;
            _gameConfiguration = gameConfiguration;

            Subscribe();
        }

        public async Task<BonusController> GetBonus(BonusModel model, CancellationToken token)
        {
            if (_bonuses.Count == 0)
            {
                if (_spawnedBonusesCount <= _gameConfiguration.MaxBonusesCount)
                {
                    await SpawnBonus(model, token);
                    if (token.IsCancellationRequested)
                    {
                        return null;
                    }
                    return _bonuses.Dequeue();
                }
                
                while (_bonuses.Count == 0)
                {
                    Debug.Log("Waiting for new bonus...");
                    await Task.Yield();
                    if (token.IsCancellationRequested)
                    {
                        return null;
                    }
                }
            }
            
            var bonus = _bonuses.Dequeue();
            bonus.SetData(model.Points, model.Lifetime, model.Position);
            return bonus;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ElementSpawnedSignal>(OnBonusSpawned);
            _signalBus.Unsubscribe<BonusWasHiddenSignal>(OnBonusWasHidden);
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<ElementSpawnedSignal>(OnBonusSpawned);
            _signalBus.Subscribe<BonusWasHiddenSignal>(OnBonusWasHidden);
        }

        private async Task SpawnBonus(BonusModel model, CancellationToken token)
        {
            _signalBus.Fire(new SpawnElementSignal {Type = typeof(BonusController), Model = model});

            while (_bonuses.Count == 0)
            {
                Debug.Log("Waiting for new bonus...");
                await Task.Yield();
                
                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        private void OnBonusSpawned(ElementSpawnedSignal signal)
        {
            if (signal.Controller is BonusController bonus)
            {
                _bonuses.Enqueue(bonus);
                _spawnedBonusesCount++;
            }
        }

        private void OnBonusWasHidden(BonusWasHiddenSignal signal)
        {
            _bonuses.Enqueue(signal.Bonus);
        }
    }
}