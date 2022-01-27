using System;
using Characters.Player;
using UnityEngine;

namespace Characters.Harnold
{
    public class HarnoldController : MonoBehaviour
    {
        private HarnoldState _currentState = new DoNothingHarnoldState(null, null);

        private Transform _playerTransform;
        [NonSerialized] public Rigidbody Rg;
        
        [NonSerialized] public AudioSource TeleportSource;
        [NonSerialized] public AudioSource EscalateSource;
        public float harnoldSpeed = 0.04f;

        private void Awake()
        {
            Rg = GetComponent<Rigidbody>();
            TeleportSource = transform.Find("Teleport audiosource").GetComponent<AudioSource>();
            EscalateSource = transform.Find("Escalate audiosource").GetComponent<AudioSource>();
        }

        private void Start()
        {
            _playerTransform = FindObjectOfType<PlayerController>().transform;
            SetState(new FollowPlayerHarnoldState(_playerTransform, this));
        }

        private void SetState(HarnoldState state)
        {
            _currentState.OnStateEnd();
            _currentState = state;
            _currentState.OnStateStart();
        }

        private void Update()
        {
            var state = _currentState.Update();

            if (state != _currentState)
                SetState(state);
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        public void MoveForward()
        {
            transform.Translate(harnoldSpeed * transform.forward, Space.World);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag("Player"))
                return;

            collision.collider.gameObject.GetComponent<PlayerController>().PassOut();
        }
    }
}