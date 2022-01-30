using Character;
using Signals;
using UnityEngine;
using Zenject;
using CharacterController = Character.CharacterController;

namespace Game
{
    public class GameLoadingManager
    {
        private readonly SignalBus _signalBus;

        public GameLoadingManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            InitCharacter();
            InitBonuses();
        }

        private void InitCharacter()
        {
            _signalBus.Fire(new ShowElementSignal { Type = typeof(CharacterController), Model = new CharacterModel()});
        }

        private void InitBonuses()
        {
            Debug.LogError("Not implemented yet");
        }
    }
}