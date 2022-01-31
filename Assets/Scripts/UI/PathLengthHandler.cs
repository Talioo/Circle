using System;
using Signals;
using UnityEngine;
using Zenject;
using CharacterController = Game.Character.CharacterController;

namespace UI
{
    public class PathLengthHandler : IDisposable, ITickable
    {
        private const string PathSaveKey = "PathLength";
        
        private readonly PathView _pathView;
        private readonly SignalBus _signalBus;

        private CharacterController _character;
        private Vector3 _lastCharacterPosition;

        public PathLengthHandler(PathView pathView, SignalBus signalBus)
        {
            _pathView = pathView;
            _signalBus = signalBus;
            
            Subscribe();
            UpdatePathLength(PlayerPrefs.GetInt(PathSaveKey, 0));
        }

        public void Tick()
        {
            var distance = Vector3.Distance(_character.CharacterPosition, _lastCharacterPosition);
            if (distance > 1)
            {
                var newLength = PlayerPrefs.GetInt(PathSaveKey) + distance;
                UpdatePathLength((int)newLength);
                _lastCharacterPosition = _character.CharacterPosition;
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ElementSpawnedSignal>(OnCharacterSpawned);
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<ElementSpawnedSignal>(OnCharacterSpawned);
        }

        private void OnCharacterSpawned(ElementSpawnedSignal obj)
        {
            if (obj.Controller is CharacterController character)
            {
                _character = character;
                _lastCharacterPosition = _character.CharacterPosition;
            }
        }

        private void UpdatePathLength(int value)
        {
            _pathView.Text = value.ToString();
            PlayerPrefs.SetInt(PathSaveKey, value);
        }
    }
}