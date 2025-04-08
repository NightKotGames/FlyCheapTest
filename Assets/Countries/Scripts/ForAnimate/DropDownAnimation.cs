
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Countries.ForAnimate
{
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
        [SerializeField] private Button _blockButton;

        [Header("Animation Settings")]
        [SerializeField] private float _elementHeight = 30f;
        [SerializeField] private float _borderOffset = 10f;
        [SerializeField] private float _animationDuration = 0.3f;

        private DropdownState _currentState = DropdownState.Closed;
        private EventTrigger _eventTrigger;

        private float _originalHeight;
        private Vector2 _originalPosition;
        private int _itemCount;

        private void Awake()
        {
            _blockButton.gameObject.SetActive(false);

            if (_selectorBackground != null)
            {
                _originalHeight = _selectorBackground.sizeDelta.y;
                _originalPosition = _selectorBackground.anchoredPosition;
            }

            _eventTrigger = GetComponent<EventTrigger>();
            ConfigureEventTrigger();
        }

        private void OnEnable() => _blockButton.onClick.AddListener(() => OnDropdownClose());
        private void OnDisable() => _blockButton.onClick.RemoveListener(() => OnDropdownClose());

        private void ConfigureEventTrigger()
        {
            _eventTrigger.triggers.Clear();
            AddEvent(EventTriggerType.PointerClick, OnDropdownOpen);
        }

        private void AddEvent(EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
        {
            var entry = new EventTrigger.Entry
            {
                eventID = eventType
            };
            entry.callback.AddListener(action);
            _eventTrigger.triggers.Add(entry);
        }

        private void OnDropdownOpen(BaseEventData data = null)
        {
            if (_currentState != DropdownState.Closed) return;

            _currentState = DropdownState.Opening;
            OpenDropdown();
        }

        private void OnDropdownClose(BaseEventData data = null)
        {
            if (_currentState == DropdownState.Opened)
            {
                _currentState = DropdownState.Closing;

                _blockButton.gameObject.SetActive(false);
                CloseDropdown();
            }
        }

        public void OpenDropdown()
        {
            if (_selectorBackground == null || _dropdown == null)
                return;

            _itemCount = _dropdown.options.Count;
            float targetHeight = (_elementHeight * _itemCount) + _borderOffset;

            _selectorBackground.DOSizeDelta(new Vector2(_selectorBackground.sizeDelta.x, targetHeight), _animationDuration)
                              .SetEase(Ease.OutQuad);

            _selectorBackground.DOAnchorPos(new Vector2(_originalPosition.x, _originalPosition.y - (targetHeight - _originalHeight) / 2), _animationDuration)
                              .SetEase(Ease.OutQuad)
                              .OnComplete(() =>
                              {
                                  _currentState = DropdownState.Opened;
                                  _blockButton.gameObject.SetActive(true);
                              });
        }

        private void CloseDropdown()
        {
            if (_selectorBackground == null)
                return;

            //_dropdown.Hide();

            _selectorBackground.DOSizeDelta(new Vector2(_selectorBackground.sizeDelta.x, _originalHeight), _animationDuration)
                              .SetEase(Ease.InQuad);

            _selectorBackground.DOAnchorPos(_originalPosition, _animationDuration)
                              .SetEase(Ease.InQuad)
                              .OnComplete(() =>
                              {
                                  _currentState = DropdownState.Closed;

                              });
        }

        public DropdownState GetCurrentState() => _currentState;
    }
}