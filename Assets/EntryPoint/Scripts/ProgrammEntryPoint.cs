
using Utility;
using UnityEngine;

namespace EntryPoint
{
    [RequireComponent(typeof(SceneLoader))]

    public class ProgrammEntryPoint : MonoBehaviour
    {
        [Header("Options: ")]
        [SerializeField] private SceneType _nextScene;
        [SerializeField] private bool _loadOnStart;
        [Space(20)]
        [Header("Need Components: ")]
        [SerializeField] private SceneLoader _loader;


        private void Start()
        {
            // Init Services

            if (_loadOnStart == true)
                Loading();
        }

        public void Loading()
        {
            if (_loader != null)
                if (_nextScene != SceneType.None)
                    _loader.LoadSceneAsync(_nextScene.ToString());
                else
                    throw new System.Exception("Loading Error !");
        }
    }
}