using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class LoadWidget : MonoBehaviour
    {
        private GameController _controller;

        void Start()
        {
            _controller = FindObjectOfType<GameController>();
        }

        public void OnClicked()
        {
            if (_controller.IsSavedSimulation())
            {
                _controller.newSimulationPopup.Open();
                _controller.simulationView.Close();
                _controller.LoadSimulation();
            }
        }
    }
}