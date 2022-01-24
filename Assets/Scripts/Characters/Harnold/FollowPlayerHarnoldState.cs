﻿using UnityEngine;
using Utils;

namespace Characters.Harnold
{
    public class FollowPlayerHarnoldState : HarnoldState
    {
        public FollowPlayerHarnoldState(Transform playerTransform, HarnoldController controller) : base(playerTransform,
            controller)
        {
        }

        private Vector3 _lastDirection = Vector3.zero;

        private LayerMask _mask = LayerMask.GetMask("Door");

        private readonly float _checkDistance = 1f;

        private void DirectionalCheck(ref Vector3 updateDirection, Vector3 direction, Vector3 counter)
        {
            Debug.DrawRay(_controller.transform.position, direction, Color.blue, 0f, false);

            if (Physics.Raycast(_controller.transform.position, direction, out var check, _checkDistance, ~_mask))
                updateDirection += (_checkDistance - check.distance) * counter;
        }

        public override void FixedUpdate()
        {
            // var momentum = _controller.transform.forward;
            // DirectionalCheck(ref momentum, _controller.transform.position + _controller.transform.forward,
            //     _controller.transform.position - _controller.transform.forward);
            //
            // if (Physics.Raycast(_controller.transform.position,
            //     (_controller.transform.position + _controller.transform.forward).normalized, out var forwardCheck,
            //     3))
            // {
            //     Debug.Log("hit");
            //     momentum = -(checkDistance - forwardCheck.distance) * _controller.transform.forward;
            // }

            var right45 = (_controller.transform.forward + _controller.transform.right).normalized;
            var right30 = (2 * _controller.transform.forward + _controller.transform.right).normalized;
            var right60 = (_controller.transform.forward + 2 * _controller.transform.right).normalized;
            var right90 = _controller.transform.right;

            var left45 = (_controller.transform.forward - _controller.transform.right).normalized;
            var left30 = (2 * _controller.transform.forward - _controller.transform.right).normalized;
            var left60 = (_controller.transform.forward - 2 * _controller.transform.right).normalized;
            var left90 = -_controller.transform.right;

            var rightPull = Vector3.zero;
            DirectionalCheck(ref rightPull, right45, left45);
            DirectionalCheck(ref rightPull, right30, left60);
            DirectionalCheck(ref rightPull, right90, _controller.transform.forward);

            var leftPull = Vector3.zero;
            DirectionalCheck(ref leftPull, left45, right45);
            DirectionalCheck(ref leftPull, left30, right60);
            DirectionalCheck(ref leftPull, left90, _controller.transform.forward);

            var playerDirection =
                (_playerTransform.position.ToX0Z() - _controller.transform.position.ToX0Z()).normalized;

            var momentum = _controller.transform.forward;

            Debug.DrawLine(_controller.transform.position, _controller.transform.position + playerDirection,
                Color.magenta, .0f, false);
            Debug.DrawLine(_controller.transform.position, _controller.transform.position + leftPull, Color.red, .0f,
                false);
            Debug.DrawLine(_controller.transform.position, _controller.transform.position + rightPull, Color.green, .0f,
                false);

            var weightedDirection = playerDirection + rightPull.ToX0Z() + leftPull.ToX0Z() + momentum.ToX0Z();

            Debug.DrawLine(_controller.transform.position.ToX0Z(), _controller.transform.position + weightedDirection,
                Color.black, .0f,
                false);

            _lastDirection = Vector3.Lerp(_lastDirection, weightedDirection, .05f);
            _controller.transform.LookAt(_controller.transform.position + _lastDirection, Vector3.up);
            _controller.MoveForward();
        }
    }
}