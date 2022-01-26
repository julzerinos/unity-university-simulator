using System;
using Characters.Player;
using UnityEngine;

namespace Characters.Harnold
{
    public class HarnoldController : MonoBehaviour
    {
        private HarnoldState _currentState = new DoNothingHarnoldState(null, null);

        private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = FindObjectOfType<PlayerController>().transform;
            SetState(new FollowPlayerHarnoldState(_playerTransform, this));
        }

        private void SetState(HarnoldState state)
        {
            _currentState.OnStateEnd();
            _currentState = state;
            _currentState.OnStateStart();
        }

        private void Update()
        {
            var state = _currentState.Update();

            if (state != _currentState)
                SetState(state);
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        public void MoveForward()
        {
            transform.Translate(.04f * transform.forward, Space.World);
        }
        

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag("Player"))
                return;

            collision.collider.gameObject.GetComponent<PlayerController>().PassOut();

        }
    }
}