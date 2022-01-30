﻿using System;
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
        private const int MaxBonusesCount = 6;
        
        private readonly SignalBus _signalBus;
        private readonly Queue<BonusController> _bonuses = new Queue<BonusController>();

        private int _spawnedBonusesCount = 0;

        public BonusesPool(SignalBus signalBus)
        {
            _signalBus = signalBus;

            Subscribe();
        }

        public async Task<BonusController> GetBonus(BonusModel model, CancellationToken token)
        {
            if (_bonuses.Count == 0)
            {
                if (_spawnedBonusesCount <= MaxBonusesCount)
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
            _signalBus.Unsubscribe<BonusElementSpawnedSignal>(OnBonusSpawned);
            _signalBus.Unsubscribe<BonusWasHiddenSignal>(OnBonusWasHidden);
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<BonusElementSpawnedSignal>(OnBonusSpawned);
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

        private void OnBonusSpawned(BonusElementSpawnedSignal signal)
        {
            _bonuses.Enqueue(signal.Bonus);
            _spawnedBonusesCount++;
        }

        private void OnBonusWasHidden(BonusWasHiddenSignal signal)
        {
            _bonuses.Enqueue(signal.Bonus);
        }
    }
}