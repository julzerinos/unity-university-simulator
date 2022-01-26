using System.Collections;
using UnityEngine;

namespace Characters.Harnold
{
    public class TeleportHarnoldState : HarnoldState
    {
        public TeleportHarnoldState(Transform playerTransform, HarnoldController controller) : base(playerTransform,
            controller)
        {
        }

        private Vector3 _playerPosition;
        private readonly WaitForSeconds _teleportWait = new WaitForSeconds(2f);
        private bool _didTeleport;

        public override void OnStateStart()
        {
            _playerPosition = PlayerTransform.position;

            Controller.StartCoroutine(TeleportDelay());
        }

        public override void OnStateEnd()
        {
        }

        public override HarnoldState Update()
        {
            if (_didTeleport)
                return new PlayerStareHarnoldState(PlayerTransform, Controller);

            return this;
        }

        private IEnumerator TeleportDelay()
        {
            var tries = 3;

            while (tries-- > 0)
            {
                yield return _teleportWait;

                if (Vector3.Dot(PlayerTransform.position - _playerPosition, PlayerTransform.forward) > 0)
                    break;
            }

            Teleport();
        }

        private void Teleport()
        {
            var target = _playerPosition;
            if ((_playerPosition - PlayerTransform.position).sqrMagnitude < 4f)
                target += 2 * PlayerTransform.forward;

            Controller.transform.position = target;

            Controller.TeleportSource.Play();

            _didTeleport = true;

            Controller.Rg.velocity = Vector3.zero;
        }
    }
}