using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms
{
    public class Door : MonoBehaviour
    {
        private float _maxRotationDegree = 90;
        private float _minRotationDegree = 0;
        public float minDistanceToDoor = 100;

        private float _currentRotation = 0;
        private Collider _doorCollider;
        private Vector3 _lastMousePosition = Vector3.zero;

        private bool _shouldReset;

        private void Awake()
        {
            _doorCollider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            var direction = Random.value > .5f;
            _minRotationDegree = direction ? 0 : -90;
            _maxRotationDegree = direction ? 90 : 0;
        }

        private void Update()
        {
            if (!Input.GetMouseButton(0))
            {
                _shouldReset = true;
                return;
            }

            var mousePosition = Input.mousePosition;

            if (_shouldReset)
            {
                _lastMousePosition = mousePosition;
                _shouldReset = false;
                return;
            }

            var ray = Camera.main.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(ray, out var hit, minDistanceToDoor)) return;

            var diff = mousePosition.x - _lastMousePosition.x;

            RotateDoor(diff);

            _lastMousePosition = mousePosition;
        }

        private void RotateDoor(float by)
        {
            if (by > 0 && _currentRotation > _maxRotationDegree)
                return;

            if (by < 0 && _currentRotation < _minRotationDegree)
                return;

            _currentRotation += by;
            transform.Rotate(Vector3.up, by, Space.World);
        }
    }
}