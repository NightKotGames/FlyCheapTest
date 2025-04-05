
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
            // Проверяем, что метод вызывается только в редакторе
#if UNITY_EDITOR
            Debug.Log("[AirportConfigurator] Scanning Map...");

            // Находим все объекты AirPortInstance с использованием нового API
            var airportInstances = Object.FindObjectsByType<AirPortInstance>(FindObjectsSortMode.None);
            int processedCount = 0;

            foreach (var airportInstance in airportInstances)
            {
                // Проверяем, есть ли у объекта AirPortData
                if (airportInstance.PortData != null)
                {
                    // Переводим мировые координаты в локальные координаты RectTransform
                    Vector2 localPosition;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        _mapRectTransform,
                        airportInstance.transform.position,
                        null,
                        out localPosition
                    );

                    // Устанавливаем координаты в AirPortData
                    airportInstance.PortData.Position = localPosition;
                    processedCount++;

                    Debug.Log($"[AirportConfigurator] Updated position for {airportInstance.PortData.AitportCountry}: {localPosition}");
                }
                else
                {
                    Debug.LogWarning($"[AirportConfigurator] AirPortInstance {airportInstance.name} has no AirPortData assigned!");
                }
            }

            Debug.Log($"[AirportConfigurator] Scanning complete. Total airports processed: {processedCount}");
#else
            Debug.LogWarning("[AirportConfigurator] ConfigureAirports can only be executed in the Editor!");
#endif
        }
    }
}