using UnityEngine;

namespace Game.Scripts
{
    public class AnimalPresenter {
        public IAnimal model;
        public AnimalView view;

        public delegate void FoodReachedHandler(AnimalPresenter animalPresenter);
        public event FoodReachedHandler OnFoodReached;

        public AnimalPresenter(IAnimal model, AnimalView view) {
            this.model = model;
            this.view = view;
        }

        public void Update(float deltaTime) {
            Vector3 direction = (model.TargetPosition - model.Position).normalized;
            Vector3 newPosition = model.Position + direction * model.Speed * deltaTime;
            
            if (Vector3.Distance(newPosition, model.TargetPosition) < 0.1f) {
                OnFoodReached?.Invoke(this);
            } else {
                if (!IsCollidingWithOtherAnimals(newPosition)) {
                    model.Position = newPosition;
                    view.transform.position = newPosition;
                }
                else
                {
                    Vector3 avoidanceDirection = GetAvoidanceDirection();
                    Vector3 avoidancePosition = model.Position + avoidanceDirection * model.Speed * deltaTime;
                    if (!IsCollidingWithOtherAnimals(avoidancePosition)) {
                        model.Position = avoidancePosition;
                        view.transform.position = avoidancePosition;
                    }
                }
            }
        }

        public void StayHere()
        {
            view.transform.position = view.transform.position;
        }
        
        private bool IsCollidingWithOtherAnimals(Vector3 newPosition) {
            Collider[] hitColliders = Physics.OverlapSphere(newPosition, 0.5f);
            foreach (var hitCollider in hitColliders) {
                if (hitCollider.gameObject != view.gameObject && hitCollider.gameObject.layer == LayerMask.NameToLayer("Animals")) {
                    return true;
                }
            }
            return false;
        }
        
        private Vector3 GetAvoidanceDirection() {
            Vector3 currentDirection = (model.TargetPosition - model.Position).normalized;
            Vector3 avoidanceDirection = new Vector3(-currentDirection.z, currentDirection.y, currentDirection.x);
            return avoidanceDirection;
        }
        
        public void UnsubscribeEvents() {
            OnFoodReached = null;
        }
    }
}