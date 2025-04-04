
using AirPorts;
using UnityEngine;

namespace Utility
{
    public class LoadMapData : MonoBehaviour
    {
        public void InitMapData()
        {
            Debug.Log("[LoadMapData] Loading airport data...");
            var airportDatas = Resources.LoadAll<AirPortData>("AirPortsData");

            if (airportDatas.Length > 0)
            {
                Debug.Log($"[LoadMapData] Loaded {airportDatas.Length} airport data files.");

                foreach (var airportData in airportDatas)
                {
                    if (AirportsDataContainer.Instance.AirPortDatas.Contains(airportData) == false)
                    {
                        AirportsDataContainer.Instance.AirPortDatas.Add(airportData);
                        Debug.Log($"[LoadMapData] Added airport: {airportData.name} in {airportData.AitportCountry}");
                    }
                    else
                    {
                        Debug.LogWarning($"[LoadMapData] Duplicate data detected: {airportData.name}");
                    }
                }
            }
            else
            {
                Debug.LogError("[LoadMapData] No airport data found in Resources/AirPortsData.");
            }
        }
    }
}