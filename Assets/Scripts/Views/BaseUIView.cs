using UnityEngine;

namespace Game.Scripts
{
    public class BaseUIView:MonoBehaviour, IView
    {
        public GameObject container;

        public virtual void Open()
        {
            container.SetActive(true);
        }

        public virtual void Close()
        {
            container.SetActive(false);
        }
    }
}