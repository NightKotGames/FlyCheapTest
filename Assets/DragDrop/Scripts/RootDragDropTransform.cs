using UnityEngine;

namespace DragDrop
{
    internal class RootDragDropTransform : MonoBehaviour
    {
        [SerializeField] private Transform _dragDropRootTransform;

        private void OnEnable() => DraggedObject.OnReturnCanvasTransform += ReturnDragDropRootTransform;
        private void OnDisable() => DraggedObject.OnReturnCanvasTransform -= ReturnDragDropRootTransform;

        private Transform ReturnDragDropRootTransform() => _dragDropRootTransform;
    }
}
