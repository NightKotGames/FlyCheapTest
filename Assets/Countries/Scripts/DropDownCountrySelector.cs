
using TMPro;
using AirPorts;
using UnityEngine;
using System.Collections.Generic;

namespace Countries
{
    public class DropDownCountrySelector : MonoBehaviour
    {
        [Header("Dropdown Reference")]
        [SerializeField] private TMP_Dropdown _countryDropdown;

        private AirportsDataContainer _dataContainer;

        private void Start()
        {
            // Находим AirportsDataContainer
            _dataContainer = Object.FindFirstObjectByType<AirportsDataContainer>();

            if (_dataContainer == null || _dataContainer.AirPortDatas.Count <= 0)
                return;

            // Список уникальных стран
            List<string> countries = new List<string>();
            foreach (var airportData in _dataContainer.AirPortDatas)
            {
                if (!string.IsNullOrEmpty(airportData.AitportCountry.ToString()) && !countries.Contains(airportData.AitportCountry.ToString()))
                {
                    countries.Add(airportData.AitportCountry.ToString());
                }
            }

            // Заполняем выпадающий список
            PopulateDropdown(countries);
        }

        private void PopulateDropdown(List<string> options)
        {
            _countryDropdown.ClearOptions();
            _countryDropdown.AddOptions(options);

            Debug.Log("[DropDownCountrySelector] Dropdown populated with countries based on AirportsDataContainer.");
        }
    }
}
