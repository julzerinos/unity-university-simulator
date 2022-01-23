using System;
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
            _currentState.Update();
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        public void MoveForward()
        {
            transform.Translate(.04f * transform.forward, Space.World);
        }
        
        public void CalculateWeights()
        {
            MoveForward();
            transform.LookAt(_playerTransform, Vector3.up);
        }
    }
}