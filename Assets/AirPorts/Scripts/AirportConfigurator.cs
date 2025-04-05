
using UnityEngine;
using NaughtyAttributes;

namespace AirPorts
{
    public class AirportConfigurator : MonoBehaviour
    {
        [Header("Map Settings")]
        [SerializeField] private RectTransform _mapRectTransform;
        
        [Tooltip("Editor Only!")]
        [Button("Configure Airports")]
        public void ConfigureAirports()
        {
#if UNITY_EDITOR
            Debug.Log("[AirportConfigurator] Scanning Map...");
            var airportInstances = Object.FindObjectsByType<AirPortInstance>(FindObjectsSortMode.None);
            int processedCount = 0;

            foreach (var airportInstance in airportInstances)
            {
                if (airportInstance.PortData != null)
                {
                    Vector2 localPosition;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        _mapRectTransform,
                        airportInstance.transform.position,
                        null,
                        out localPosition
                    );

                    airportInstance.PortData.Position = localPosition;
                    processedCount++;
                    UnityEditor.EditorUtility.SetDirty(airportInstance.PortData);

                    Debug.Log($"[AirportConfigurator] Updated position for {airportInstance.PortData.AitportCountry}: {localPosition}");
                }
                else
                {
                    Debug.LogWarning($"[AirportConfigurator] AirPortInstance {airportInstance.name} has no AirPortData assigned!");
                }
            }

            UnityEditor.AssetDatabase.SaveAssets();
            Debug.Log($"[AirportConfigurator] Scanning complete. Total airports processed: {processedCount}");
#else
Debug.LogWarning("[AirportConfigurator] ConfigureAirports can only be executed in the Editor!");
#endif

        }
    }
}