using Common;
using DefaultNamespace;
using Map.InputSystem;
using Zenject;

namespace Character
{
    public class CharacterController : BaseController<CharacterView, CharacterModel>, ITickable
    {
        private IInputSystem _inputSystem;

        private int _pathPointer = 0;

        public CharacterController(IView view, IModel model, IInputSystem inputSystem)
            : base(view, model)
        {
            _inputSystem = inputSystem;
        }

        public void Tick()
        {

        }
    }
}