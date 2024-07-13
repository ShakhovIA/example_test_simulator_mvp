using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class GameController : MonoBehaviour
    {
        public GameObject animalPrefab;
        public GameObject foodPrefab;
        public GameObject eatEffectPrefab;
        public Transform groundParent;
        public Transform particleParent;
        public int N = 10;
        public int M = 5;
        public float V = 1.0f;
        public float simulationSpeed = 0f;

        private List<AnimalPresenter> _animalPresenters = new List<AnimalPresenter>();
        private List<FoodPresenter> _foodPresenters = new List<FoodPresenter>();
        public SimulationView simulationView;
        public NewSimulationPopup newSimulationPopup;

        private void Start()
        {
            simulationView.Close();
            newSimulationPopup.Open();
        }

        void Update()
        {
            if (simulationSpeed > 0)
            {
                float deltaTime = Time.deltaTime * simulationSpeed;
                foreach (var animalPresenter in _animalPresenters)
                {
                    animalPresenter.Update(deltaTime);
                }
            }
            else
            {
                foreach (var animalPresenter in _animalPresenters)
                {
                    animalPresenter.StayHere();
                }
            }
        }

        public void Init()
        {
            ClearSimulation();
            for (int i = 0; i < M; i++)
            {
                Vector3 animalPosition = new Vector3(Random.Range(0, N), 0, Random.Range(0, N));
                Vector3 foodPosition = GetRandomFoodPosition(animalPosition);

                GameObject animalObject = Instantiate(animalPrefab, animalPosition, Quaternion.identity, groundParent);
                GameObject foodObject = Instantiate(foodPrefab, foodPosition, Quaternion.identity,groundParent);

                IAnimal animalModel = new AnimalModel
                    { Position = animalPosition, TargetPosition = foodPosition, Speed = V };
                IFood foodModel = new FoodModel { Position = foodPosition };

                AnimalView animalView = animalObject.GetComponent<AnimalView>();
                FoodView foodView = foodObject.GetComponent<FoodView>();

                AnimalPresenter animalPresenter = new AnimalPresenter(animalModel, animalView);
                FoodPresenter foodPresenter = new FoodPresenter(foodModel, foodView);

                animalPresenter.OnFoodReached += HandleFoodReached;

                _animalPresenters.Add(animalPresenter);
                _foodPresenters.Add(foodPresenter);
            }

            simulationSpeed = 1.0f;
        }
        
        public void SaveSimulation() {
            SimulationState state = new SimulationState {
                animalPositions = new List<Vector3>(),
                foodPositions = new List<Vector3>()
            };

            foreach (var animalPresenter in _animalPresenters) {
                state.animalPositions.Add(animalPresenter.model.Position);
            }

            foreach (var foodPresenter in _foodPresenters) {
                state.foodPositions.Add(foodPresenter.model.Position);
            }

            SaveLoadManager.SaveSimulation(state);
        }

        public void LoadSimulation() {
            SimulationState state = SaveLoadManager.LoadSimulation();
            if (state != null) {
                
                ClearSimulation();
                
                for (int i = 0; i < state.animalPositions.Count; i++) {
                    Vector3 animalPosition = state.animalPositions[i];
                    Vector3 foodPosition = state.foodPositions[i];

                    GameObject animalObject = Instantiate(animalPrefab, animalPosition, Quaternion.identity, groundParent);
                    GameObject foodObject = Instantiate(foodPrefab, foodPosition, Quaternion.identity, groundParent);

                    IAnimal animalModel = new AnimalModel { Position = animalPosition, TargetPosition = foodPosition, Speed = V };
                    IFood foodModel = new FoodModel { Position = foodPosition };

                    AnimalView animalView = animalObject.GetComponent<AnimalView>();
                    FoodView foodView = foodObject.GetComponent<FoodView>();

                    AnimalPresenter animalPresenter = new AnimalPresenter(animalModel, animalView);
                    FoodPresenter foodPresenter = new FoodPresenter(foodModel, foodView);
                    
                    animalPresenter.OnFoodReached += HandleFoodReached;

                    _animalPresenters.Add(animalPresenter);
                    _foodPresenters.Add(foodPresenter);
                }
            }

            simulationSpeed = 1.0f;
        }

        public bool IsSavedSimulation()
        {
            SimulationState state = SaveLoadManager.LoadSimulation();
            if (state != null)
            {
                return false;
            }
            return true;
        }

        private void HandleFoodReached(AnimalPresenter animalPresenter)
        {
            for (int i = 0; i < _foodPresenters.Count; i++)
            {
                if (_foodPresenters[i].model.Position == animalPresenter.model.TargetPosition)
                {
                    GameObject effect = Instantiate(eatEffectPrefab, animalPresenter.model.Position, Quaternion.identity, particleParent);
                    Destroy(effect, 1.0f);
                    _foodPresenters[i].model.Position = GetRandomFoodPosition(animalPresenter.model.Position);
                    _foodPresenters[i].view.gameObject.transform.position = _foodPresenters[i].model.Position;
                    animalPresenter.model.TargetPosition = _foodPresenters[i].model.Position;
                    break;
                }
            }
        }

        public void ClearSimulation()
        {
            foreach (var animalPresenter in _animalPresenters) {
                animalPresenter.UnsubscribeEvents();
                Destroy(animalPresenter.view.gameObject);
            }
            foreach (var foodPresenter in _foodPresenters) {
                Destroy(foodPresenter.view.gameObject);
            }
            _animalPresenters.Clear();
            _foodPresenters.Clear();
        }

        private Vector3 GetRandomFoodPosition(Vector3 animalPosition)
        {
            Vector3 foodPosition;
            float maxDistance = V * 5.0f;

            do
            {
                foodPosition = new Vector3(Random.Range(0, N), 0, Random.Range(0, N));
            } while (Vector3.Distance(animalPosition, foodPosition) > maxDistance ||
                     IsFoodPositionOccupied(foodPosition));

            return foodPosition;
        }

        private bool IsFoodPositionOccupied(Vector3 position)
        {
            foreach (var foodPresenter in _foodPresenters)
            {
                if (Vector3.Distance(foodPresenter.model.Position, position) < 0.1f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
