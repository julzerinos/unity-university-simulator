using UnityEngine;

namespace Characters.Harnold
{
    public abstract class HarnoldState
    {
        protected Transform _playerTransform;
        protected HarnoldController _controller;
        
        public HarnoldState(Transform playerTransform, HarnoldController controller)
        {
            _playerTransform = playerTransform;
            _controller = controller;
        }
        
        public virtual void OnStateStart()
        {
        }

        public virtual void OnStateEnd()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }
    }
}