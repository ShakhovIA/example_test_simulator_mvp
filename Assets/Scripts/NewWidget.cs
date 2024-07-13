using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class NewWidget : MonoBehaviour
    {
        private GameController _controller;

        void Start()
        {
            _controller = FindObjectOfType<GameController>();
        }

        public void OnClicked()
        {
            _controller.newSimulationPopup.Open();
            _controller.simulationView.Close();
            _controller.simulationSpeed = 0;
        }
    }
}