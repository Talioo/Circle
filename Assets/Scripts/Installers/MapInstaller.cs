using Common.Factories;
using Game;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalsInstaller.Install(Container);

            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ViewsConfiguration>().AsSingle();
            Container.BindInterfacesAndSelfTo<ViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ControllersFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ControllersManager>().AsSingle();

            Container.Bind<GameLoadingManager>().AsSingle().NonLazy();
        }
    }
}