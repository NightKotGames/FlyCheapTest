
using TMPro;
using AirPorts;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Countries
{
    public class DropDownCountrySelector : MonoBehaviour
    {
        [Header("Need Components: ")]
        [SerializeField] private TMP_Dropdown _countryDropdown;  // Ссылка на Dropdown
        [SerializeField] private TextMeshProUGUI _dropDownLabelText; // Оригинальный Label, задающий размеры        

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
            SetFlags(_dropDownLabelText);
        }

        private void SetFlags(TextMeshProUGUI countryText)
        {
            // Проверяем, что текст не пустой
            if (string.IsNullOrEmpty(countryText.text))
                return;

            // Получаем компонент CountryFlagViewer, связанный с countryText
            var flagViewer = countryText.GetComponent<CountryFlagViewer>();
            if (flagViewer == null)
            {
                Debug.LogWarning($"[DropDownCountrySelector] CountryFlagViewer не найден для элемента: {countryText.text}");
                return;
            }

            // Ищем соответствующий объект AirPortData по названию страны
            var airportData = _dataContainer.AirPortDatas.Find(data => data.AitportCountry.ToString() == countryText.text);
            if (airportData == null)
            {
                Debug.LogWarning($"[DropDownCountrySelector] Данные для страны {countryText.text} не найдены.");
                return;
            }

            // Устанавливаем изображение флага через CountryFlagViewer
            flagViewer.SetImage(airportData.CountrySprite);
            Debug.Log($"[DropDownCountrySelector] Установлен флаг для страны: {countryText.text}");
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
            StartCoroutine(WaitAndAdjustLabels());
        }

        private IEnumerator WaitAndAdjustLabels()
        {
            GameObject dropdownList = null;

            // Ждём появления Dropdown List
            while (dropdownList == null)
            {
                dropdownList = GameObject.Find(_dropdownListObjectName);
                yield return null;
            }

            // Находим все TextMeshProUGUI элементы и меняем их параметры
            var textComponents = dropdownList.GetComponentsInChildren<TextMeshProUGUI>();
            

            foreach (var text in textComponents)
            {
                text.fontSize = _dropDownLabelText.fontSize;

                // Настраиваем размеры RectTransform
                var itemRect = text.GetComponent<RectTransform>();
                if (itemRect != null)
                    itemRect.sizeDelta = _dropDownLabelText.rectTransform.sizeDelta;

                SetFlags(text);
                yield return null; // Ждём следующий кадр
            }

            
        }
    }
}