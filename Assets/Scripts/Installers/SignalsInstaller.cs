using Signals;
using Zenject;

namespace Installers
{
    public class SignalsInstaller  : Installer<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        
            Container.DeclareSignal<SpawnElementSignal>();
            Container.DeclareSignal<ElementSpawnedSignal>();
            Container.DeclareSignal<BonusWasHiddenSignal>();
            Container.DeclareSignal<AddPointsSignal>();
        }
    }
}
