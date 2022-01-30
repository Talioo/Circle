using Game;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Game settings/Settings Installer")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField]
        private GameConfiguration _gameConfiguration;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_gameConfiguration).AsSingle();
        }
    }
}