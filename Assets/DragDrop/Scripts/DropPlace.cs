
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragDrop
{
    internal class DropPlace : MonoBehaviour, IDropHandler
    {
        [SerializeField] private bool _resizeDropObject;
        [SerializeField] private Transform _targetDropTransform;

        public void OnDrop(PointerEventData eventData)
        {
            DraggedObject currentDragObject = null;
            if (eventData.pointerDrag.TryGetComponent(out DraggedObject @object) == true)
                @object.DefaultParent = _targetDropTransform; currentDragObject = @object;
            if(_resizeDropObject == true)
                currentDragObject.gameObject.GetComponent<RectTransform>().sizeDelta = _targetDropTransform.gameObject.GetComponent<RectTransform>().sizeDelta;
        }
    }
}
