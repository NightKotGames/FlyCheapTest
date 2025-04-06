using System;
using UnityEngine;

namespace Map
{
    public class HandMapScroll : MonoBehaviour
    {
        [SerializeField] private RectTransform _mapRectTransform; 
        [SerializeField] private RectTransform _maskRectTransform;

        private bool _dragPemission;

        private Vector2 _lastTouchPosition;
        private bool _isDragging = false;

        private void OnEnable() => MapAutoMovePosition.OnMapDragToggle += DragPermission;
        private void OnDisable() => MapAutoMovePosition.OnMapDragToggle -= DragPermission;

        private void DragPermission(bool dragPemission) => _dragPemission = dragPemission;

        private void Update()
        {
            if(_dragPemission == false)
                return;

            if (Input.touchCount == 1) // Одно касание
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _isDragging = true;
                        _lastTouchPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        if (_isDragging)
                        {
                            Vector2 delta = touch.position - _lastTouchPosition;
                            Vector2 newPosition = _mapRectTransform.anchoredPosition + delta;

                            // Размеры карты и маски
                            float mapWidth = _mapRectTransform.rect.width;
                            float mapHeight = _mapRectTransform.rect.height;
                            float maskWidth = _maskRectTransform.rect.width;
                            float maskHeight = _maskRectTransform.rect.height;

                            // Рассчитываем границы
                            float minX = -(mapWidth - maskWidth) / 2; // Левая граница
                            float maxX = (mapWidth - maskWidth) / 2;  // Правая граница
                            float minY = -(mapHeight - maskHeight) / 2; // Нижняя граница
                            float maxY = (mapHeight - maskHeight) / 2;  // Верхняя граница

                            // Применяем ограничения
                            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                            _mapRectTransform.anchoredPosition = newPosition;
                            _lastTouchPosition = touch.position;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isDragging = false;
                        break;
                }
            }
        }
    }
}