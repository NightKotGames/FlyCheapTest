
using TMPro;
using UnityEngine;

namespace Countries
{
    public class SelectCountryPusher : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdownMenu;
        public static event System.Action<string> OnSelectCountry;

        private void OnEnable()
        {
            if(_dropdownMenu ==  null)
                return;
            _dropdownMenu.onValueChanged.AddListener(delegate { PushCountry(_dropdownMenu.options[_dropdownMenu.value].text); });
        }

        private void OnDisable()
        {
            if (_dropdownMenu == null)
                return;
            _dropdownMenu.onValueChanged.RemoveListener(delegate { PushCountry(_dropdownMenu.options[_dropdownMenu.value].text); });
        }

        private void PushCountry(string selectedCountry) => OnSelectCountry?.Invoke(selectedCountry);
    }
}