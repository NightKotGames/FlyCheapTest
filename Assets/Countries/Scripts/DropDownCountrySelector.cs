
using TMPro;
using AirPorts;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Countries
{
    public class DropDownCountrySelector : MonoBehaviour
    {
        [Header("Need Components:")]
        [SerializeField] private TMP_Dropdown _countryDropdown;
        [SerializeField] private Image _countryImage; // Ссылка на Image для отображения флага
        [SerializeField] private TextMeshProUGUI _dropDownLabelText; // Оригинальный Label для размера шрифта

        private AirportsDataContainer _dataContainer;
        private List<TMP_Dropdown.OptionData> _dropDownMenuDatas = new();

        private void Start()
        {
            // Находим AirportsDataContainer
            _dataContainer = Object.FindFirstObjectByType<AirportsDataContainer>();

            if (_dataContainer == null)
            {
                Debug.LogWarning("AirportsDataContainer не найден!");
                return;
            }
            _countryDropdown.onValueChanged.AddListener(UpdateFlag);
            PopulateDropdown(GetUniqueCountries(_dataContainer));
        }

        private List<TMP_Dropdown.OptionData> GetUniqueCountries(AirportsDataContainer dataContainer)
        {            
            foreach (var airportData in dataContainer.AirPortDatas)
            {
                if (string.IsNullOrEmpty(airportData.AitportCountry.ToString()) == false)
                    _dropDownMenuDatas.Add(new TMP_Dropdown.OptionData(airportData.AitportCountry.ToString(),                        airportData.CountrySprite, Color.white));
            }
            return _dropDownMenuDatas;
        }

        private void PopulateDropdown(List<TMP_Dropdown.OptionData> options)
        {
            _countryDropdown.ClearOptions();
            _countryDropdown.AddOptions(options);

            // Убедимся, что флаг обновляется для первоначально выбранного варианта
            UpdateFlag(_countryDropdown.value);
            AdjustDropdownFont();
        }


        private void UpdateFlag(int index)
        {
            string selectedCountry = _countryDropdown.options[index].text;
            var airportData = _dataContainer.AirPortDatas.Find(data => data.AitportCountry.ToString() == selectedCountry);

            if (airportData != null)
            {
                _countryImage.sprite = airportData.CountrySprite;
                Debug.Log($"Флаг обновлен для страны: {selectedCountry}");
            }
            else
            {
                Debug.LogWarning($"Не удалось найти данные для страны: {selectedCountry}");
                _countryImage.sprite = null;
            }
        }

        private void AdjustDropdownFont()
        {
            var dropdownTemplate = _countryDropdown.template;
            if (dropdownTemplate == null)
            {
                Debug.LogWarning("Шаблон Dropdown не найден!");
                return;
            }

            var dropdownItems = dropdownTemplate.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (var text in dropdownItems)
            {
                text.fontSize = _dropDownLabelText.fontSize;
                var itemRect = text.GetComponent<RectTransform>();
                if (itemRect != null)
                    itemRect.sizeDelta = _dropDownLabelText.rectTransform.sizeDelta;
            }
        }
    }
}