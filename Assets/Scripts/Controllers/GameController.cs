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

        private Transform _rooms;
        private int _ectsCollected = 0;
        private readonly Random _random = new Random();
        private Ects _ects;
        private Calculator _calculator;
        private Text _ectsText;
        private List<int> _usedIdxs = new List<int>();
        private AudioSource _collectAudioSource;
        private Light _flashlight;

        private void Awake()
        {
            _rooms = transform.Find("Rooms");
            var playerTransform = transform.Find("Player");
            _ectsText = playerTransform.Find("Canvas").Find("ECTS counter").GetComponent<Text>();
            _calculator = playerTransform.Find("Main Camera").Find("CalculatorRotator").Find("Calculator")
                .GetComponent<Calculator>();
            _collectAudioSource = GetComponent<AudioSource>();
            _flashlight = playerTransform.Find("Main Camera").Find("Spot Light").GetComponent<Light>();
           SpawnEctsInRandomRoom();
        }

        private void SpawnEctsInRandomRoom()
        {
            var pos = _rooms.GetChild(RandomRoomIndex()).position;
            SpawnEctsAtPosition(pos);
        }

        private void SpawnEctsAtPosition(Vector3 pos)
        {
            _ects = Instantiate(ectsPrefab, transform);
            _ects.gameObject.SetActive(true);
            _ects.transform.position = pos;
            _ects.EctsCollected += OnEctsCollectedEvent;
            _calculator.EctsPosition = _ects.transform.position;
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
            EctsCollectionEffects();
        }

        private void EctsCollectionEffects()
        {
            _collectAudioSource.Play();
            IncrementEctsCollected();
            switch (_ectsCollected)
            {
                case 1:
                {
                    RenderSettings.fogDensity = 0.2f;
                    SpawnEctsInRandomRoom();
                    break;
                }
                case 2:
                {
                    RenderSettings.fogDensity = 0.3f;
                    SpawnEctsInRandomRoom();
                    _flashlight.intensity = 5;
                    break;
                }
                case 3:
                {
                    RenderSettings.fogDensity = 0.35f;
                    SpawnEctsInRandomRoom();
                    _flashlight.enabled = false;
                    break;
                }
                case 4:
                {
                    // open exit door
                    SpawnEctsBehindSpawn();
                    break;
                }
            }
        }
        private void IncrementEctsCollected()
        {
            _ectsCollected += 1;
            _ectsText.text = $"{_ectsCollected} of 4 ECTS collected";
            
        }
        private void SpawnEctsBehindSpawn()
        {
            SpawnEctsAtPosition(_rooms.Find("SpawnRoom").position - new Vector3(0,0,8));
        }
    }
}