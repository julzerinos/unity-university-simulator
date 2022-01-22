using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    public event Action<Room> RoomChanged;
    
    private int _doorsCount;
    public List<Transform> DoorLocations { get; private set; } = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in transform.Find("Doors"))
        {
            DoorLocations.Add(child);
        }
    }


    public void Init(int doorsCount)
    {
        _doorsCount = doorsCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        RoomChanged?.Invoke(this);
    }
}

//public class PipeRoom: Room
//{

//}

//public class CornerRoom: Room
//{

//}

//public class TeeRoom: Room
//{

//}

//public class IntersectionRoom: Room
//{

//}