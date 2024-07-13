using UnityEngine;

namespace Game.Scripts
{
    public class AnimalModel: IAnimal {
        public Vector3 Position { get; set; }
        public Vector3 TargetPosition { get; set; }
        public float Speed { get; set; }
    }
}