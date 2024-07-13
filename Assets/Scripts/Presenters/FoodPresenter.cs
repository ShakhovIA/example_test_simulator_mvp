namespace Game.Scripts
{
    public class FoodPresenter {
        public IFood model;
        public FoodView view;

        public FoodPresenter(IFood _model, FoodView _view) {
            model = _model;
            view = _view;
            view.SetModel(model);
        }
    }
}