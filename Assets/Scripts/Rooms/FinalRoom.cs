using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class FinalRoom : MonoBehaviour
{
    private AudioSource _finalSource;
    private GameObject _harnold;
    private GameObject _diploma;

    private void Awake()
    {
        _finalSource = GetComponent<AudioSource>();
        _harnold = transform.Find("Harnolds").gameObject;
        _diploma = transform.Find("Diploma").gameObject;
    }

    private void Trigger(PlayerController player)
    {
        RenderSettings.fogDensity = 0f;
        
        _finalSource.Play();
        _harnold.SetActive(true);
        _diploma.SetActive(false);

        StartCoroutine(HarnoldActivates(player));
    }

    private IEnumerator HarnoldActivates(PlayerController player)
    {
        foreach (Transform harnold in _harnold.transform.GetChild(1))
        {
            harnold.gameObject.SetActive(true);
            yield return new WaitForSeconds(.2f);
        }

        player.PassOut();
    }

    private void OnTriggerEnter(Collider other)
    {
        Trigger(other.GetComponent<PlayerController>());
    }
}