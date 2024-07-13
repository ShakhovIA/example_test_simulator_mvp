using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class StartWidget : MonoBehaviour
    {
        private GameController _controller;

        void Start()
        {
            _controller = FindObjectOfType<GameController>();
        }

        public void OnClicked()
        {
            _controller.newSimulationPopup.Close();
            _controller.simulationView.Open();
            _controller.Init();
        }
    }
}