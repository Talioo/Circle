using System;

namespace Common
{
    public class BaseController <TView, TModel> : IController
        where TView : IView 
        where TModel : IModel
    {
        protected TModel Model { get; private set; }
        protected TView View { get; private set; }

        public BaseController(IView view, IModel model)
        {
            Model = (TModel)model;
            View = (TView)view;
        }

        public void Show()
        {
            
        }

        public void Hide()
        {
            
        }
    }
}