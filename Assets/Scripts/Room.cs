using System;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    public event Action<Room> RoomChanged;

    // private int id;
    //private int _doorsCount;
    //public bool NeighboursSpawned { get; set; }
    public List<Transform> DoorLocations { get; } = new List<Transform>();

    public List<Room> Neighbours { get; } = new List<Room>();


    private void Awake()
    {
        foreach (Transform child in transform.Find("Doors"))
        {
            DoorLocations.Add(child);
        }
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