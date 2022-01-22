using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Room prefab;

    private Graph _graph;

    //private List<Room> Rooms = new List<Room>();
    private ObjectPool<Room> _roomObjectPool;
    private Room _currentPlayerRoom;

    private void Awake()
    {
        var roomCount = 3;
        _roomObjectPool = new ObjectPool<Room>(roomCount, prefab, transform);
        _graph = GraphFactory.CircularGraph(roomCount);

        var vertex = _graph.Root;
        var neighboursCount = _graph.Root.GetNeightourCount();

        _currentPlayerRoom = CreateRoom(Vector3.zero);
        CreateRoomNeighbours(_currentPlayerRoom);
    }

    private Room CreateRoom(Vector3 position)
    {
        var newRoom = _roomObjectPool.GetObject();

        newRoom.transform.position = position;
        newRoom.gameObject.SetActive(true);

        return newRoom;
    }

    private void DisposeRoom(Room room)
    {
        room.gameObject.SetActive(false);
    }

    private void CreateRoomNeighbours(Room room)
    {
        foreach (var doorLocation in room.DoorLocations)
        {
            var position = doorLocation.position;
            var direction = position - room.transform.position;
            var roomCenter = direction.normalized *
                             Mathf.Abs(Vector3.Dot(direction.normalized, room.GetComponent<Collider>().bounds.extents));
            CreateRoom(roomCenter + position);
        }
    }
}