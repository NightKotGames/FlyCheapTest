
using UnityEngine;

namespace EntryPoint
{
    public class MainMenuEntryPoint : EntryPoint
    {
        [Header("Options: ")]
        [SerializeField] private SceneType _nextScene;
        [SerializeField] private bool _loadOnStart;

        public void Initialize()
        {
            ///
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