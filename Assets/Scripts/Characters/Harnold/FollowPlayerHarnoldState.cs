using System.Collections;
using Rooms;
using UnityEngine;
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

        private Vector3 _lastDistanceCheckPoint;
        private bool _didNotMove;
        private readonly WaitForSeconds _distanceCheckWait = new WaitForSeconds(10f);

        public override void OnStateStart()
        {
            Controller.StartCoroutine(DistanceCheckLoop());

            Controller.EscalateSource.Play();
        }

        public override void OnStateEnd()
        {
            Controller.EscalateSource.Stop();
        }

        private IEnumerator DistanceCheckLoop()
        {
            while (!_didNotMove)
            {
                yield return _distanceCheckWait;

                if ((_lastDistanceCheckPoint - Controller.transform.position).sqrMagnitude >= 4f)
                {
                    _lastDistanceCheckPoint = Controller.transform.position;
                    continue;
                }

                _didNotMove = true;
                break;
            }
        }

        public override HarnoldState Update()
        {
            if (_didNotMove)
                return new TeleportHarnoldState(PlayerTransform, Controller);

            if (Physics.Raycast(
                Controller.transform.position, Controller.transform.forward, out var hit,
                2f, _mask) && hit.collider.CompareTag("Doot"))
                hit.collider.GetComponent<Door>().Open(Controller.transform.forward);

            return this;
        }

        private void DirectionalCheck(ref Vector3 updateDirection, Vector3 direction, Vector3 counter)
        {
            if (Physics.Raycast(Controller.transform.position, direction, out var check, _checkDistance, ~_mask))
                updateDirection += (_checkDistance - check.distance) * counter;
        }

        public override void FixedUpdate()
        {
            var transform = Controller.transform;

            var forward = transform.forward;
            var right = transform.right;

            var right45 = (forward + right).normalized;
            var right30 = (2 * forward + right).normalized;
            var right60 = (forward + 2 * right).normalized;
            var right90 = right;

            var left45 = (forward - right).normalized;
            var left30 = (2 * forward - right).normalized;
            var left60 = (forward - 2 * right).normalized;
            var left90 = -right;

            var rightPull = Vector3.zero;
            DirectionalCheck(ref rightPull, right45, left45);
            DirectionalCheck(ref rightPull, right30, left60);
            DirectionalCheck(ref rightPull, right90, forward);

            var leftPull = Vector3.zero;
            DirectionalCheck(ref leftPull, left45, right45);
            DirectionalCheck(ref leftPull, left30, right60);
            DirectionalCheck(ref leftPull, left90, forward);

            var position = Controller.transform.position;
            var playerDirection =
                (PlayerTransform.position.ToX0Z() - position.ToX0Z()).normalized;

            var momentum = forward;

            var weightedDirection = playerDirection + rightPull.ToX0Z() + leftPull.ToX0Z() + momentum.ToX0Z();

            _lastDirection = Vector3.Lerp(_lastDirection, weightedDirection, .05f);
            Controller.transform.LookAt(position + _lastDirection, Vector3.up);
            Controller.MoveForward();
        }
    }
}