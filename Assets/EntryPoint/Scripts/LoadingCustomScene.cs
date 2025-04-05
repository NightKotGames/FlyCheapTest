
using UnityEngine;

namespace EntryPoint
{
    public class LoadingCustomScene : EntryPoint
    {
        [Header("Options: ")]
        [SerializeField] private SceneType _nextScene;

        public void LoadScene() => Loading(_nextScene);
    }
}