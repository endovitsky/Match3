using System;
using Components;
using Extensions;
using UnityEngine;

namespace Managers
{
    public abstract class BaseManager<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
    {
        public event Action<bool> IsInitializedChanged = delegate { };

        public bool IsInitialized
        {
            get => _isInitialized;
            internal set
            {
                if (_isInitialized == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionExtensions.GetCallerMemberName()}" +
                          $"\n{_isInitialized}->{value}");
                _isInitialized = value;

                IsInitializedChanged.Invoke(_isInitialized);
            }
        }

        public static T Instance { get; private set; }

        private bool _isInitialized;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this.gameObject.GetComponent<T>();

                DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
                    Destroy(gameObject);
                }
            }

            Initialize();

            Subscribe();
            SubscribeAsync();
        }
        private void OnDestroy()
        {
            Instance = null;

            UnSubscribe();

            UnInitialize();
        }
    }
}
