using System.Collections.Generic;
using Rooms;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public Ects ectsPrefab;

        // Start is called before the first frame update
        private Transform _rooms;
        private int _ectsCollected = 0;
        private Random _random = new Random();
        private Ects _ects;
        private Transform _player;
        private Text _ectsText;
        private List<int> _usedIdxs = new List<int>();
        private AudioSource _collectAudioSource;

        private void Awake()
        {
            _rooms = transform.Find("Rooms");
            _player = transform.Find("Player");
            _ectsText = _player.Find("Canvas").Find("ECTS counter").GetComponent<Text>();
            _collectAudioSource = GetComponent<AudioSource>();
            SpawnEctsInRandomRoom();
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void SpawnEctsInRandomRoom()
        {
            var pos = _rooms.GetChild(RandomRoomIndex()).position;
            pos.y += 1;
            _ects = Instantiate(ectsPrefab, transform);
            _ects.gameObject.SetActive(true);
            _ects.transform.position = pos;
            _ects.EctsCollected += OnEctsCollectedEvent;
        }

        private int RandomRoomIndex()
        {
            int newRandom;
            do
            {
                newRandom = _random.Next(_rooms.childCount - 1);
            } while (_usedIdxs.Contains(newRandom));
            _usedIdxs.Add(newRandom);
            return newRandom;
        }

        private void OnEctsCollectedEvent(Ects ects)
        {
            ects.gameObject.SetActive(false);
            _collectAudioSource.Play();
            IncrementEctsCollected();
            SpawnEctsInRandomRoom();
        }

        private void IncrementEctsCollected()
        {
            _ectsCollected += 1;
            _ectsText.text = $"{_ectsCollected} of 4 ECTS collected";
        }
    }
}