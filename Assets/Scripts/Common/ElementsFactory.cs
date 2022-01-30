using System;
using Common.Factories;
using Signals;
using Zenject;

namespace Common
{
    public class ElementsFactory : IDisposable
    {
        private readonly IViewFactory _viewFactory;
        private readonly IControllersFactory _controllersFactory;
        private readonly SignalBus _signalBus;

        public ElementsFactory(IControllersFactory controllersFactory, IViewFactory viewFactory, SignalBus signalBus)
        {
            _controllersFactory = controllersFactory;
            _viewFactory = viewFactory;
            _signalBus = signalBus;

            Subscribe();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SpawnElementSignal>(SpawnController);
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<SpawnElementSignal>(SpawnController);
        }

        private void SpawnController(SpawnElementSignal signal)
        {
            var view = _viewFactory.GetView(signal.Type);
            var controller = _controllersFactory.GetController(signal.Type, view, signal.Model);
            controller.OnSpawn();
        }
    }
}