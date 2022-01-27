using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    private GameObject _parallax;

    private Transform _leftDoor;
    private Transform _rightDoor;

    private GameObject _warning;

    private bool _isOpen;

    private GameObject _finalRoom;

    private void Awake()
    {
        _parallax = transform.Find("Parallax window view").gameObject;
        _leftDoor = transform.Find("Left door");
        _rightDoor = transform.Find("Right door");
        _warning = transform.Find("ECTS warning").gameObject;

        _finalRoom = transform.parent.Find("Final room").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde))
            ChangeToFinalState();
    }

    public void ChangeToFinalState()
    {
        _parallax.SetActive(false);
        OpenDoors();
        _isOpen = true;
        _finalRoom.SetActive(true);
    }

    private void OpenDoors()
    {
        _rightDoor.Rotate(Vector3.up, 90, Space.World);
        _leftDoor.Rotate(Vector3.up, -90, Space.World);
    }

    public void DisplayWarning()
    {
        if (_isOpen)
            return;


        if (!_warning.activeSelf)
            StartCoroutine(DisplayWarningCoroutine());
    }

    private IEnumerator DisplayWarningCoroutine()
    {
        _warning.SetActive(true);
        yield return new WaitForSeconds(3f);
        _warning.SetActive(false);
    }
}