using UnityEngine;

namespace Characters.Harnold
{
    public class DoNothingHarnoldState : HarnoldState
    {
        public DoNothingHarnoldState(Transform playerTransform, HarnoldController controller) : base(playerTransform, controller)
        {
        }
    }
}