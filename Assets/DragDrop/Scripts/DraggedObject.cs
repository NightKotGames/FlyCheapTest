using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragDrop
{
    [RequireComponent(typeof(CanvasGroup))]

    internal class DraggedObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [HideInInspector] public Transform DefaultParent;
        
        [SerializeField] private bool _isDragging;
        public bool IsDraggind => _isDragging;

        public static event Func<Transform> OnReturnCanvasTransform;
        public static event Action<int> OnDaggedObjectWitchID = delegate { };


                
        private Camera _camera;
        private Vector3 _offset;
        

        private void Awake() => _camera = Camera.allCameras[0];

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _offset = transform.position - _camera.ScreenToWorldPoint(eventData.position);
            DefaultParent = OnReturnCanvasTransform.Invoke();
            transform.SetParent(DefaultParent.parent);
            SetActiveBlockingRaycast(false);
            OnDaggedObjectWitchID.Invoke(gameObject.GetInstanceID());   
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPos = _camera.ScreenToWorldPoint(eventData.position);
            transform.position = newPos + _offset;    
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(DefaultParent);
            SetActiveBlockingRaycast(true);
            _isDragging = false;
        }

        public void SetActiveBlockingRaycast(bool state) => _canvasGroup.blocksRaycasts = state;
    }
}