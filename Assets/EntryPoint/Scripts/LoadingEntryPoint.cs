
using UnityEngine;
using Utility;

namespace EntryPoint
{    
    public sealed class LoadingEntryPoint : EntryPoint, IInitEntryPoint
    {
        [Header("Options: ")]
        [SerializeField] private SceneType _nextScene;
        [SerializeField] private bool _loadOnStart;

        public void Initialize()
        {
            ///

            var mapDataLoader = Object.FindFirstObjectByType<LoadMapData>();
            if (mapDataLoader != null)
                mapDataLoader.InitMapData();
        }

        private void Start()
        {
            // Init Services
            Initialize();

            if (_loadOnStart == true)
                Loading(_nextScene);
        }
    }
}