using System;
using UnityEngine;

namespace Rooms
{
    public class Ects : MonoBehaviour
    {
        public event Action<Ects> EctsCollected;

        private void OnTriggerEnter(Collider other)
        {
            EctsCollected?.Invoke(this);
        }
    }
}
