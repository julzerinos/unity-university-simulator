using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rooms
{
    public class Door : MonoBehaviour
    {
        private AudioSource _doorSource;

        private bool _isOpening;

        private bool _wasRotated;

        private void Awake()
        {
            _doorSource = GetComponent<AudioSource>();
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

        public void Open(Vector3 agentDirection)
        {
            if (_isOpening) return;
            StartCoroutine(OpenDoor(agentDirection));
        }

        private IEnumerator OpenDoor(Vector3 agentDirection)
        {
            _isOpening = true;

            for (float i = 0; i <= 1f; i += .01f)
            {
                RotateDoor(1, agentDirection);
                yield return null;
            }

            _isOpening = false;
        }
    }
}