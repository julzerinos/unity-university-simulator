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
        
        public override void OnStateStart()
        {
            
            
            Controller.harnoldSource.Play();
        }

        public override void OnStateEnd()
        {
        }

        public override HarnoldState Update()
        {
            return this;
        }

        private IEnumerator ActionDelay(float time)
        {
            yield return new WaitForSeconds(2f);
        }
    }
}