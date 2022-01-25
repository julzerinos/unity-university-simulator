using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms
{
    public class Door : MonoBehaviour
    {
        private AudioSource _doorSource;

        private bool _wasRotated;

        private void Awake()
        {
            _doorSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            // var direction = Random.value > .5f;
            // _minRotationDegree = direction ? 0 : -90;
            // _maxRotationDegree = direction ? 90 : 0;
        }

        private void Update()
        {
            if (_doorSource.isPlaying && !_wasRotated)
                _doorSource.Pause();

            _wasRotated = false;
        }

        public void RotateDoor(float by, Vector3 agentDirection)
        {
            _wasRotated = Mathf.Abs(by) > .1f;

            if (!_doorSource.isPlaying && _wasRotated)
                _doorSource.Play();
            
            by *= Mathf.Sign(Vector3.Dot(transform.right, agentDirection));
            transform.Rotate(Vector3.up, by, Space.World);
        }
    }
}