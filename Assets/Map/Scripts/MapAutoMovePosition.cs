
using System;
using AirPorts;
using Countries;
using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class MapAutoMovePosition : MonoBehaviour
    {
        [SerializeField] private RectTransform _mapRectTransform;
        [SerializeField] private float _moveTime = 5f;

        // Экшен для управления вводом
        public static event Action<bool> OnMapDragToggle;

        private AirportsDataContainer _dataContainer;

        private void Start()
        {
            _dataContainer = UnityEngine.Object.FindFirstObjectByType<AirportsDataContainer>();

            if (_dataContainer == null)
            {
                Debug.LogWarning("AirportsDataContainer отсутствует в сцене.");
                return;
            }

            var selectedCountry = CurrentSelectedCountry.Instance.SelectedCountry;
            var airport = FindAirportForCountry(selectedCountry);
            if (airport != null)
                MoveMapToPosition(airport.Position);
            else
                Debug.LogWarning($"Аэропорт для страны {selectedCountry} не найден.");
        }

        private AirPortData FindAirportForCountry(string country)
        {
            foreach (var airport in _dataContainer.AirPortDatas)
            {
                if (airport.AitportCountry.ToString() == country)
                    return airport;
            }
            return null;
        }

        private void MoveMapToPosition(Vector2 position)
        {
            if (_mapRectTransform != null)
            {
                OnMapDragToggle?.Invoke(false);

                _mapRectTransform.DOAnchorPos(-position, _moveTime)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() => OnMapDragToggle?.Invoke(true));
            }
            else
                Debug.LogWarning("RectTransform карты не найден.");
        }
    }
}
