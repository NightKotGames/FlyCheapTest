
using Utility;
using UnityEngine;

namespace Countries
{
    public class CurrentSelectedCountry : Singleton<CurrentSelectedCountry>
    {
        [SerializeField] private string _selectedCountry;
        public string SelectedCountry => _selectedCountry;

        private void OnEnable() => SelectCountryPusher.OnSelectCountry += SetCountry;
        private void OnDestroy() => SelectCountryPusher.OnSelectCountry -= SetCountry;

        private void SetCountry(string selectedCountry) => _selectedCountry = selectedCountry;

    }
}