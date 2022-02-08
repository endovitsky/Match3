using UnityEngine;

namespace Components
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        public void ReInitialize()
        {
            UnInitialize();
            Initialize();
        }

        public abstract void Initialize();
        public abstract void UnInitialize();
        public abstract void Subscribe();
        protected virtual async void SubscribeAsync()
        {
        }
        public abstract void UnSubscribe();
    }
}
