using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Characters.Harnold
{
    public class PlayerStareHarnoldState : HarnoldState
    {
        public PlayerStareHarnoldState(Transform playerTransform, HarnoldController controller) : base(playerTransform,
            controller)
        {
        }

        private float _secondsLeft = 5f;

        public override void FixedUpdate()
        {
            Controller.transform.LookAt(PlayerTransform, Vector3.up);
        }

        public override HarnoldState Update()
        {
            if ((_secondsLeft -= Time.deltaTime) < 0)
                return new FollowPlayerHarnoldState(PlayerTransform, Controller);
            
            return this;
        }
    }
}