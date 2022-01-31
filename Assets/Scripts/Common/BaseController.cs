using Signals;
using Zenject;

namespace Common
{
    public class BaseController <TView, TModel> : IController
        where TView : IView 
        where TModel : IModel
    {
        protected SignalBus SignalBus { get; }
        protected TModel Model { get; private set; }
        protected TView View { get; private set; }

        public BaseController(IView view, IModel model, SignalBus signalBus)
        {
            Model = (TModel)model;
            View = (TView)view;
            SignalBus = signalBus;
        }

        public virtual void OnSpawn()
        {
            SignalBus.Fire(new ElementSpawnedSignal {Controller = this});
        }
    }
}