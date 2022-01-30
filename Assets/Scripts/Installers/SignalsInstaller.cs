﻿using Signals;
using Zenject;

namespace Installers
{
    public class SignalsInstaller  : Installer<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        
            Container.DeclareSignal<ShowElementSignal>();
            Container.DeclareSignal<HideFirstElementSignal>();
            Container.DeclareSignal<ControllerWasHiddenSignal>();
        }
    }
}