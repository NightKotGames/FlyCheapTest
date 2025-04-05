
using UnityEngine;
using UnityEngine.UI;

namespace Countries
{
    public class CountryFlagViewer : MonoBehaviour
    {
        [SerializeField] private Image _countryImage;

        public void SetImage(Sprite sprite) => _countryImage.sprite = sprite;
    }
}