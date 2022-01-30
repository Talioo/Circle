using Game.Character;
using Signals;
using Zenject;
using CharacterController = Game.Character.CharacterController;

namespace Game
{
    public class GameLoadingManager
    {
        private readonly SignalBus _signalBus;

        public GameLoadingManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            InitCharacter();
        }

        private void InitCharacter()
        {
            _signalBus.Fire(new SpawnElementSignal { Type = typeof(CharacterController), Model = new CharacterModel()});
        }
    }
}