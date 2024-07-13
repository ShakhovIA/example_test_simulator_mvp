using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class SimulationSpeedWidget : MonoBehaviour
    {
        private GameController _controller;

        private Slider _slider;

        private Text _text;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _text = GetComponentInChildren<Text>();
        }

        void Start()
        {
            _controller = FindObjectOfType<GameController>();
            // _slider.value = 0;
            // ChangeState();
        }

        public void ChangeStateSimulationSpeed()
        {
            _controller.simulationSpeed = _slider.value;
            _text.text = $"Simulation Speed [Current = {_slider.value}] [MAX = {_slider.maxValue}]";
        }
        
        public void ChangeStateMapSize()
        {
            _controller.N = Mathf.RoundToInt(_slider.value);
            _text.text = $"Map Size [Current = {_slider.value}x{_slider.value}] [MAX = {_slider.maxValue}]";
        }
        
        public void ChangeStateAnimalsCount()
        {
            _controller.M = Mathf.RoundToInt(_slider.value);
            _text.text = $"Animal Count [Current = {_slider.value}] [MAX = {_slider.maxValue}]";
        }
    }
}
