using UnityEngine;

namespace Characters.Harnold
{
    public abstract class HarnoldState
    {
        protected Transform PlayerTransform;
        protected HarnoldController Controller;
        
        public HarnoldState(Transform playerTransform, HarnoldController controller)
        {
            PlayerTransform = playerTransform;
            Controller = controller;
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