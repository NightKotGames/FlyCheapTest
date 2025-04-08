
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]

public class DropDownAnimation : MonoBehaviour
{
    public enum DropdownState
    {
        Closed,
        Opening,
        Opened,
        Closing
    }

    [Header("UI Components")]
    [SerializeField] private RectTransform _selectorBackground; 
    [SerializeField] private TMP_Dropdown _dropdown;

    [Header("Animation Settings")]
    [SerializeField] private float _elementHeight = 30f; // Высота одного элемента
    [SerializeField] private float _borderOffset = 10f; // Смещение для бордюра
    [SerializeField] private float _animationDuration = 0.3f; // Длительность анимации

    [SerializeField] private DropdownState _currentState = DropdownState.Closed; // Текущее состояние
    
    private EventTrigger _eventTrigger; 

    private float _originalHeight;
    private Vector2 _originalPosition; 
    private int _itemCount; 

    private void Awake()
    {
        // Инициализация высоты и позиции
        if (_selectorBackground != null)
        {
            _originalHeight = _selectorBackground.sizeDelta.y;
            _originalPosition = _selectorBackground.anchoredPosition;
        }

        // Настройка EventTrigger
        _eventTrigger = GetComponent<EventTrigger>();
        ConfigureEventTrigger();
    }

    private void ConfigureEventTrigger()
    {
        // Очищаем существующие триггеры
        _eventTrigger.triggers.Clear();

        // Событие для открытия
        AddEvent(EventTriggerType.PointerClick, OnDropdownOpen);

        // Событие для закрытия через клик вне Dropdown
        AddEvent(EventTriggerType.PointerClick, OnDropdownClose);
    }

    private void AddEvent(EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        var entry = new EventTrigger.Entry
        {
            eventID = eventType
        };
        entry.callback.AddListener(action);
        _eventTrigger.triggers.Add(entry);

        Debug.Log($"Событие {eventType} добавлено в EventTrigger.");
    }

    private void OnDropdownOpen(BaseEventData data)
    {
        if (_currentState != DropdownState.Closed) return;

        _currentState = DropdownState.Opening;
        OpenDropdown();
    }

    private void OnDropdownClose(BaseEventData data)
    {
        // Проверяем, если меню уже открыто
        if (_currentState == DropdownState.Opened)
        {
            Debug.Log("Dropdown закрывается.");
            CloseDropdown();
        }
    }

    public void OpenDropdown()
    {
        if (_selectorBackground == null || _dropdown == null) return;

        _itemCount = _dropdown.options.Count;
        float targetHeight = (_elementHeight * _itemCount) + _borderOffset;

        // Анимация увеличения высоты вниз
        _selectorBackground.DOSizeDelta(new Vector2(_selectorBackground.sizeDelta.x, targetHeight), _animationDuration)
                          .SetEase(Ease.OutQuad);

        _selectorBackground.DOAnchorPos(new Vector2(_originalPosition.x, _originalPosition.y - (targetHeight - _originalHeight) / 2), _animationDuration)
                          .SetEase(Ease.OutQuad)
                          .OnComplete(() => _currentState = DropdownState.Opened);
    }

    private void CloseDropdown()
    {
        if (_selectorBackground == null) return;

        _currentState = DropdownState.Closing;

        // Анимация возврата высоты и позиции
        _selectorBackground.DOSizeDelta(new Vector2(_selectorBackground.sizeDelta.x, _originalHeight), _animationDuration)
                          .SetEase(Ease.InQuad);

        _selectorBackground.DOAnchorPos(_originalPosition, _animationDuration)
                          .SetEase(Ease.InQuad)
                          .OnComplete(() => _currentState = DropdownState.Closed);
    }

    public DropdownState GetCurrentState()
    {
        return _currentState;
    }
}
