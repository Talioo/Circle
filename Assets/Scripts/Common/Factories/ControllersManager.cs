using System;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;

namespace Common.Factories
{
    public class ControllersManager : IDisposable
    {
        private readonly IViewFactory _viewFactory;
        private readonly IControllersFactory _controllersFactory;
        private readonly SignalBus _signalBus;
        
        private readonly List<IController> _controllers = new List<IController>();

        public ControllersManager(IControllersFactory controllersFactory, IViewFactory viewFactory, SignalBus signalBus)
        {
            _controllersFactory = controllersFactory;
            _viewFactory = viewFactory;
            _signalBus = signalBus;

            Subscribe();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ShowElementSignal>(SpawnController);
            _signalBus.Unsubscribe<HideFirstElementSignal>(OnHideElement);
            _signalBus.Unsubscribe<ControllerWasHiddenSignal>(OnControllerWasHidden);
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<ShowElementSignal>(SpawnController);
            _signalBus.Subscribe<HideFirstElementSignal>(OnHideElement);
            _signalBus.Subscribe<ControllerWasHiddenSignal>(OnControllerWasHidden);
        }

        private void SpawnController(ShowElementSignal signal)
        {
            var view = _viewFactory.GetView(signal.Type);
            var controller = _controllersFactory.GetController(signal.Type, view, signal.Model);
            _controllers.Add(controller);
            controller.Show();
        }

        private void OnHideElement(HideFirstElementSignal signal)
        {
            IController controllerToHide = null;
            foreach (var controller in _controllers)
            {
                if (controller.GetType() == signal.Type)
                {
                    controllerToHide = controller;
                    break;
                }
            }

            if (controllerToHide == null)
            {
                Debug.LogWarning($"There is no controllers with type {signal.Type} to hide!");
                return;
            }
            controllerToHide.Hide();
        }

        private void OnControllerWasHidden(ControllerWasHiddenSignal signal)
        {
            _controllers.Remove(signal.Controller);
        }
    }
}