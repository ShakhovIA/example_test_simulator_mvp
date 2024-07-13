using UnityEngine;

namespace Game.Scripts
{

    public interface IAnimal
    {
        Vector3 Position { get; set; }
        Vector3 TargetPosition { get; set; }
        float Speed { get; set; }
    }

}