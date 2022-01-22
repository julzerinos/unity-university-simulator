using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Room prefab;
    private Graph Graph;
    //private List<Room> Rooms = new List<Room>();
    private ObjectPool<Room> RoomObjectPool;
    private Room CurrentPlayerRoom;

    private void Awake()
    {
        int roomCount = 3;
        RoomObjectPool = new ObjectPool<Room>(roomCount, prefab);
        Graph = GraphFactory.circularGraph(roomCount);

        var vertex = Graph.Root;
        int neighboursCount = Graph.Root.GetNeightourCount();

        CurrentPlayerRoom = createRoom(Vector3.zero);
        createRoomNeighbours(CurrentPlayerRoom);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void setRoom(Room room, Room relativeRoom)
    //{

    //}

    private Room createRoom(Vector3 position)
    {
        var newRoom = RoomObjectPool.GetObject();
        newRoom.transform.position = position;
        newRoom.gameObject.SetActive(true);
        return newRoom;

    }

    private void disposeRoom(Room room)
    {
        room.gameObject.SetActive(false);
    }

    private void createRoomNeighbours(Room room)
    {
        foreach (Transform doorLocation in room.DoorLocations)
        {
            var direction = room.transform.position - doorLocation.position;
            var roomCenter = direction.normalized * Mathf.Abs(Vector3.Dot(direction.normalized, room.GetComponent<Collider>().bounds.extents));
            createRoom(roomCenter + doorLocation.position);
        }
    }

}
