using UnityEngine;

namespace Characters.Harnold
{
    public class FollowPlayerHarnoldState : HarnoldState
    {
        public FollowPlayerHarnoldState(Transform playerTransform, HarnoldController controller) : base(playerTransform,
            controller)
        {
        }

        private Vector3 _lastDirection = Vector3.zero;

        private float checkDistance = 4f;

        public override void FixedUpdate()
        {
            // var brakePull = Vector3.zero;
            // if (Physics.Raycast(_controller.transform.position,
            //     (_controller.transform.position + _controller.transform.forward).normalized, out var forwardCheck,
            //     checkDistance))
            //     brakePull += (checkDistance - forwardCheck.distance) * -_controller.transform.forward;

            var right45 = (_controller.transform.forward + _controller.transform.right).normalized;
            var right30 = (2 * _controller.transform.forward + _controller.transform.right).normalized;
            var right90 = _controller.transform.right;
            var rightPull = Vector3.zero;
            if (Physics.Raycast(_controller.transform.position, right45, out var leftCheck45, checkDistance))
                rightPull += (checkDistance - leftCheck45.distance) * -_controller.transform.right;
            if (Physics.Raycast(_controller.transform.position, right30, out var leftCheck30, checkDistance))
                rightPull += (checkDistance - leftCheck30.distance) * -_controller.transform.right;
            if (Physics.Raycast(_controller.transform.position, right90, out var leftCheck90, checkDistance))
                rightPull += (checkDistance - leftCheck90.distance) * -_controller.transform.right;

            var left45 = (_controller.transform.forward - _controller.transform.right).normalized;
            var left30 = (2 * _controller.transform.forward - _controller.transform.right).normalized;
            var left90 = -_controller.transform.right;
            var leftPull = Vector3.zero;
            if (Physics.Raycast(_controller.transform.position, left45, out var rightCheck45, checkDistance))
                leftPull += (checkDistance - rightCheck45.distance) * _controller.transform.right;
            if (Physics.Raycast(_controller.transform.position, left30, out var rightCheck30, checkDistance))
                leftPull += (checkDistance - rightCheck30.distance) * _controller.transform.right;
            if (Physics.Raycast(_controller.transform.position, left90, out var rightCheck90, checkDistance))
                leftPull += (checkDistance - rightCheck90.distance) * _controller.transform.right;

            var momentum = _controller.transform.forward;
            var playerDirection = (_playerTransform.position - _controller.transform.position).normalized;
            var weightedDirection = playerDirection + rightPull + leftPull + momentum;// + brakePull;
            _lastDirection = Vector3.Lerp(_lastDirection, weightedDirection, .05f);
            _controller.transform.LookAt(_controller.transform.position + _lastDirection, Vector3.up);
            _controller.MoveForward();

            // Debug.DrawLine(_controller.transform.position, _controller.transform.position + brakePull, Color.red, .0f,
                // false);
            Debug.DrawLine(_controller.transform.position, _controller.transform.position + leftPull, Color.blue, .0f,
                false);
            Debug.DrawLine(_controller.transform.position, _controller.transform.position + rightPull, Color.green, .0f,
                false);
            Debug.DrawLine(_controller.transform.position, _controller.transform.position + playerDirection,
                Color.magenta, .0f, false);
        }
    }
}