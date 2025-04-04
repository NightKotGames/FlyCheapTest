
using Utility;
using UnityEngine;

namespace EntryPoint
{
    [RequireComponent(typeof(SceneLoader))]

    public class EntryPoint : MonoBehaviour, IEntryPoint
    {
        [Header("Need Components: ")]
        [SerializeField] private SceneLoader _loader;

        public void Loading(SceneType nextScene)
        {
            if (_loader != null)
            {
                if (nextScene != SceneType.None)
                    _loader.LoadSceneAsync(nextScene.ToString());
                else
                    throw new System.Exception("Loading Error!");
            }
        }

        public void LoadingDefault() => Loading(SceneType.Root);
    }
}