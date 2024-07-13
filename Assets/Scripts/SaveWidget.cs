using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class SaveWidget : MonoBehaviour
    {
        private GameController _controller;

        void Start()
        {
            _controller = FindObjectOfType<GameController>();
        }

        public void OnClicked()
        {
            _controller.SaveSimulation();
        }
    }
}