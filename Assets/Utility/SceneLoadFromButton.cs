
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class SceneLoadFromButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SceneLoader _sceneLoader;
        [Space(20)]
        [SerializeField] private string _sceneToLoadName;

        private void OnEnable() => _button.onClick.AddListener(() => _sceneLoader.LoadSceneAsync(_sceneToLoadName));

        private void OnDisable() => _button.onClick.RemoveListener(_button.onClick.Invoke);
    }
}