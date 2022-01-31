using Common;
using Common.Factories;
using Game;
using Game.BonusesSystem;
using InputSystem;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MapInstaller : MonoInstaller
    {
        [SerializeField]
        private PathView _pathView;
        [SerializeField]
        private PointsView _pointsView;
        
        public override void InstallBindings()
        {
            SignalsInstaller.Install(Container);

            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ViewsConfiguration>().AsSingle();
            Container.BindInterfacesAndSelfTo<ViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ControllersFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ElementsFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BonusesPool>().AsSingle();

            Container.BindInstance(_pathView).AsSingle();
            Container.BindInstance(_pointsView).AsSingle();
            Container.BindInterfacesAndSelfTo<PointsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathLengthHandler>().AsSingle();

            Container.Bind<GameLoadingManager>().AsSingle().NonLazy();
            Container.Bind<BonusesSystem>().AsSingle().NonLazy();
        }
    }
}