
using UnityEngine;
using UnityEngine.UI;

namespace EntryPoint
{
    public sealed class SceneLoadFromButton : EntryPoint
    {
        [SerializeField] private Button _button;
        [Space(20)]
        [SerializeField] private SceneType _nextScene;

        private void OnEnable() => _button.onClick.AddListener(()=> Loading(_nextScene));
        private void OnDisable() => _button.onClick.RemoveListener(()=> Loading(_nextScene));
    }
}