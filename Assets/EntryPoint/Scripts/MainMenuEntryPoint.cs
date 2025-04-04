
using AirPorts;
using UnityEngine;

namespace EntryPoint
{
    public sealed class MainMenuEntryPoint : EntryPoint
    {
        [Header("Options: ")]
        [SerializeField] private SceneType _nextScene;
        [SerializeField] private bool _loadOnStart;

        private bool _initIsSuccessFully;

        public void Initialize()
        {
            ///

            if (Object.FindFirstObjectByType<AirportsDataContainer>() == null)
            {
                Debug.LogWarning("[MainMenuEntryPoint] AirportsDataContainer not found! Loading default scene...");
                _initIsSuccessFully = false;
                LoadingDefault();
            }
            else
                _initIsSuccessFully = true;
        }

        private void Start()
        {
            // Init Services
            Initialize();

            if (_loadOnStart == true)
                if(_initIsSuccessFully == true)
                    Loading(_nextScene);
        }
    }
}