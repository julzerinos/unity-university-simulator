using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        public AudioClip[] randomAmbiances;
        public AudioClip escalate;
        public float secondBetweenAmbiance = 10f;

        private int _currentAmbiance;
        private AudioSource _musicSource;

        private void Awake()
        {
            _musicSource = GetComponent<AudioSource>();
            StartCoroutine(SongTimer(0));
        }

        private IEnumerator SongTimer(float sec)
        {
            yield return new WaitForSeconds(sec + secondBetweenAmbiance);

            PlayNextRandomAmbiance();
        }

        private IEnumerator FadeAudio(bool fadingIn, float time, float delay = 0)
        {
            yield return new WaitForSeconds(delay);

            float currentTime = 0;
            var start = fadingIn ? 0 : 1;

            while (currentTime < time)
            {
                currentTime += Time.deltaTime;
                _musicSource.volume = Mathf.Lerp(start, fadingIn ? 1 : 0, currentTime / time);
                yield return null;
            }
        }

        private void PlayNextRandomAmbiance()
        {
            _currentAmbiance = (_currentAmbiance + Random.Range(0, randomAmbiances.Length - 1)) %
                               randomAmbiances.Length;

            _musicSource.clip = randomAmbiances[_currentAmbiance];

            StartCoroutine(FadeAudio(true, 5f));
            StartCoroutine(FadeAudio(false, 5f, _musicSource.clip.length - 5f));

            _musicSource.Play();

            StartCoroutine(SongTimer(_musicSource.clip.length));
        }

        public void Escalate()
        {
            _musicSource.Stop();

            StopAllCoroutines();
            _musicSource.clip = escalate;
            _musicSource.Play();
        }

        public void Deescalate()
        {
            _musicSource.Stop();

            StartCoroutine(SongTimer(0));
        }
    }
}