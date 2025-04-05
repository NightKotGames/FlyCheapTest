
using TMPro;
using AirPorts;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Countries
{
    public class DropDownCountrySelector : MonoBehaviour
    {
        [Header("Need Components: ")]
        [SerializeField] private TMP_Dropdown _countryDropdown;  // Ссылка на Dropdown
        [SerializeField] private TextMeshProUGUI _dropDownLabelText; // Оригинальный Label, задающий размеры
        [Header("Opyions: ")]
        [SerializeField] private bool _resizeSelect;


        private const string _dropdownListObjectName = "Dropdown List"; // Имя объекта Dropdown List
        private AirportsDataContainer _dataContainer;

        private void Start()
        {
            // Находим AirportsDataContainer
            _dataContainer = Object.FindFirstObjectByType<AirportsDataContainer>();

            if (_dataContainer == null)
                return;

            // Добавляем EventTrigger к Dropdown для отслеживания нажатий
            AddDropdownClickListener();

            // Генерируем уникальные страны и заполняем Dropdown
            List<string> countries = GetUniqueCountries(_dataContainer);
            PopulateDropdown(countries);
        }

        private List<string> GetUniqueCountries(AirportsDataContainer dataContainer)
        {
            HashSet<string> countries = new HashSet<string>();
            foreach (var airportData in dataContainer.AirPortDatas)
            {
                if (string.IsNullOrEmpty(airportData.AitportCountry.ToString()) == false)
                    countries.Add(airportData.AitportCountry.ToString());
            }
            return new List<string>(countries);
        }

        private void PopulateDropdown(List<string> options)
        {
            _countryDropdown.ClearOptions();
            _countryDropdown.AddOptions(options);

            Debug.Log($"[DropDownCountrySelector] Dropdown populated with {options.Count} countries.");
        }

        private void AddDropdownClickListener()
        {
            var trigger = _countryDropdown.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = _countryDropdown.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };

            entry.callback.AddListener((eventData) => { OnDropdownClicked(); });
            trigger.triggers.Add(entry);
        }

        private void OnDropdownClicked()
        {
            Debug.Log("[DropDownCountrySelector] Dropdown clicked, waiting for the list to appear...");
            StartCoroutine(WaitAndAdjustLabels());
        }

        private IEnumerator WaitAndAdjustLabels()
        {
            if(_resizeSelect == false)
                yield return null;

            GameObject dropdownList = null;

            // Ждём появления Dropdown List
            while (dropdownList == null)
            {
                dropdownList = GameObject.Find(_dropdownListObjectName);
                yield return null; // Ждём следующий кадр
            }

            Debug.Log("[DropDownCountrySelector] Dropdown List appeared.");

            // Находим все TextMeshProUGUI элементы и меняем их параметры
            var textComponents = dropdownList.GetComponentsInChildren<TextMeshProUGUI>();
            
            foreach (var text in textComponents)
            {
                // Применяем размер шрифта
                text.fontSize = _dropDownLabelText.fontSize;

                // Настраиваем размеры RectTransform
                var itemRect = text.GetComponent<RectTransform>();
                if (itemRect != null)
                {
                    itemRect.sizeDelta = _dropDownLabelText.rectTransform.sizeDelta;
                    Debug.Log($"[DropDownCountrySelector] Applied size {itemRect.sizeDelta} to: {text.gameObject.name}");
                }

                Debug.Log($"[DropDownCountrySelector] Applied font size {text.fontSize} to: {text.gameObject.name}");
            }
        }
    }
}