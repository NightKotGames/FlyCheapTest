
using Utility;
using UnityEngine;

namespace EntryPoint
{
    [RequireComponent(typeof(SceneLoader))]
    
    public class LoadingEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneType _nextScene;
        [SerializeField] private SceneLoader _loader;

        private void Start()
        {
            // Init Services


            Loading();
        }

        private void Loading()
        {
            if (_loader != null)
                if (_nextScene != SceneType.None)
                    _loader.LoadSceneAsync(_nextScene.ToString());
                else
                    throw new System.Exception("Loading Error !");
        }
    }
}