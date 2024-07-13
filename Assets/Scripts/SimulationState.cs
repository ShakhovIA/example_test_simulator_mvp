using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    [System.Serializable]
    public class SimulationState {
        public List<Vector3> animalPositions;
        public List<Vector3> foodPositions;
    }
}