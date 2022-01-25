using UnityEngine;

namespace Characters.Harnold
{
    public class OpenDoorHarnoldState : HarnoldState
    {
        public OpenDoorHarnoldState(Transform playerTransform, HarnoldController controller) : base(playerTransform,
            controller)
        {
        }

        private readonly LayerMask _mask = LayerMask.GetMask("Door");

        private float _startTime;
        private float _timeLeft;

        public override void OnStateStart()
        {
            Debug.Log("Enter door state");
            _timeLeft = 3f;
        }

        public override void OnStateEnd()
        {
            Debug.Log("Leave door state");
        }

        public override HarnoldState Update()
        {
            Debug.DrawRay(Controller.transform.position, Controller.transform.forward, Color.blue, 0f, false);

            // if (!Physics.Raycast(
            //     Controller.transform.position, Controller.transform.forward, out var check,
            //     2f, _mask))
            //     return new FollowPlayerHarnoldState(PlayerTransform, Controller);

//             return this;

            if ((_timeLeft -= Time.deltaTime) < 0)
                return new FollowPlayerHarnoldState(PlayerTransform, Controller);

            return this;
        }
    }
}