
using Utility;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircleUIView : MonoBehaviour
{
    [SerializeField] private float _turnImageAmount;
    [SerializeField] private Image _loadingImage;

    private void OnEnable() => SceneLoader.OnLoadingTeek += TurnImage;
    private void OnDisable() => SceneLoader.OnLoadingTeek -= TurnImage;

    private void TurnImage() => _loadingImage.transform.Rotate(0, 0, _turnImageAmount);

}
