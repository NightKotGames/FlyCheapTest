
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace Countries.ForAnimate
{
    public class TMP_CustomDropdown : TMP_Dropdown
    {
        [Header("Animation Settings")]
        [SerializeField] private RectTransform _selectorBackground;

        [SerializeField] private float _elementHeight = 30f; 

        [SerializeField] private float _borderOffset = 10f; 

        [SerializeField] private float _animationDuration = 0.3f; 
               
        private float _originalHeight;
        private Vector2 _originalPosition;
        private int _itemCount;


        public RectTransform SelectorBackground
        {
            get => _selectorBackground;
            set => _selectorBackground = value;
        }
        public float ElementHeight
        {
            get => _elementHeight;
            set => _elementHeight = value;
        }
        public float BorderOffset
        {
            get => _borderOffset;
            set => _borderOffset = value;
        }
        public float AnimationDuration
        {
            get => _animationDuration;
            set => _animationDuration = value;
        }



        protected override void Awake()
        {
            base.Awake();

            // Инициализация высоты и позиции фона
            if (_selectorBackground != null)
            {
                _originalHeight = _selectorBackground.sizeDelta.y;
                _originalPosition = _selectorBackground.anchoredPosition;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.Show();

            // Состояние и анимация открытия
            _itemCount = options.Count;
            float targetHeight = (_elementHeight * _itemCount) + _borderOffset;

            if (_selectorBackground != null)
            {
                _selectorBackground.DOSizeDelta(new Vector2(_selectorBackground.sizeDelta.x, targetHeight), _animationDuration)
                                   .SetEase(Ease.OutQuad);

                _selectorBackground.DOAnchorPos(new Vector2(_originalPosition.x, _originalPosition.y - (targetHeight - _originalHeight) / 2), _animationDuration)
                                   .SetEase(Ease.OutQuad);
            }
        }

        public override void OnCancel(BaseEventData eventData)
        {
            base.Hide();

            // Анимация возврата высоты и позиции
            if (_selectorBackground != null)
            {
                _selectorBackground.DOSizeDelta(new Vector2(_selectorBackground.sizeDelta.x, _originalHeight), _animationDuration)
                                   .SetEase(Ease.InQuad);

                _selectorBackground.DOAnchorPos(_originalPosition, _animationDuration)
                                   .SetEase(Ease.InQuad);
            }
        }
    }
}