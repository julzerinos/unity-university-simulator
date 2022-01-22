using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
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
        this._doorsCount = doorsCount;



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