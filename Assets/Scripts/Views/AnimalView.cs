using UnityEngine;

namespace Game.Scripts
{
    public class AnimalView : MonoBehaviour {
        private IAnimal animalModel;

        public void SetModel(IAnimal model) {
            animalModel = model;
        }
    }
}