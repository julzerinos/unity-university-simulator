using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    int DoorsCount;
    public List<Transform> DoorLocations { get; private set; } = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in transform.Find("Doors"))
        {
            DoorLocations.Add(child);
        }
    }


    public void Init(int DoorsCount)
    {
        this.DoorsCount = DoorsCount;



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