
using Countryes;
using UnityEngine;

namespace AirPorts
{
    [CreateAssetMenu(menuName = "World/Airports/NewAirPort", fileName = "NewAirPort")]

    public class AirPortData : ScriptableObject
    {
        [SerializeField] private CountriesType _aitportCountry;
        public CountriesType AitportCountry => _aitportCountry;

        public Vector2 Position;
    }
}