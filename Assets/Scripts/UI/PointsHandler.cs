using System;
using Signals;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PointsHandler : IDisposable
    {
        private const string PointsSaveKey = "Points";
        
        private readonly PointsView _pointsView;
        private readonly SignalBus _signalBus;

        public PointsHandler(PointsView pointsView, SignalBus signalBus)
        {
            _pointsView = pointsView;
            _signalBus = signalBus;
            
            Subscribe();
            SetNewPointsCount(PlayerPrefs.GetInt(PointsSaveKey, 0));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<AddPointsSignal>(OnPointsAdd);
        }
        
        private void Subscribe()
        {
            _signalBus.Subscribe<AddPointsSignal>(OnPointsAdd);
        }

        private void OnPointsAdd(AddPointsSignal signal)
        {
            var currentPoints = PlayerPrefs.GetInt(PointsSaveKey);
            SetNewPointsCount(currentPoints + signal.Count);
        }

        private void SetNewPointsCount(int count)
        {
            _pointsView.Text = count.ToString();
            PlayerPrefs.SetInt(PointsSaveKey, count);
        }
    }
}
