namespace Components
{
    public abstract class BaseComponent : BaseMonoBehaviour
    {
        private void Awake()
        {
            Initialize();

            Subscribe();
            SubscribeAsync();
        }
        private void OnDestroy()
        {
            UnSubscribe();

            UnInitialize();
        }
    }
}
