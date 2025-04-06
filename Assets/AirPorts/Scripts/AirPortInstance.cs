
using UnityEngine;

namespace AirPorts
{
    public class AirPortInstance : MonoBehaviour
    {
        [SerializeField] private AirPortData _portData;
        public AirPortData PortData => _portData;

        public Vector2 Position { get; private set; }

        private void Awake() => Position = _portData.Position;
    }
}