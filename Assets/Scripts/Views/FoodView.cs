using UnityEngine;

namespace Game.Scripts
{
    public class FoodView : MonoBehaviour {
        private IFood foodModel;

        public void SetModel(IFood model) {
            foodModel = model;
        }
    }
}