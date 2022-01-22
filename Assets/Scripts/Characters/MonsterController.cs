using UnityEngine;

namespace Characters
{
    public class MonsterController : MonoBehaviour
    {
        private MonsterState _currentState = new DoNothingMonsterState();
        
        
    }

    public abstract class MonsterState
    {
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

    public class DoNothingMonsterState : MonsterState
    {
    }
}