using InputSystem;
using Map;
using Map.InputSystem;
using Zenject;

namespace Installers
{
    public class MapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
}