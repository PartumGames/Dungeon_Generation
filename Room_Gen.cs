using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Gen : MonoBehaviour
{
    public int totalRooms;//how many rooms do you want

    public float roomSize;//how big is your room -> used a single number here because all rooms are a square

    public GameObject startingRoomPrefab;//the room your player will spawn into

    public List<Vector3> roomsLocations = new List<Vector3>();//list of all the room locations/so we can spawn in room prefabs later

    private Vector3 currentPos = Vector3.zero;//current location for our walker

    public List<GameObject> rooms = new List<GameObject>();//all room prefabs



    private void Start()
    {
        AddStartRoom();
        GenRooms();
        SpawnInRooms();
    }

    private void AddStartRoom()
    {
        roomsLocations.Add(currentPos);//just add the current positon to the roomLocations list
    }

    private void GenRooms()
    {
        for (int i = 0; i < totalRooms; i++)//loop through how many rooms we want
        {
            Vector3 pos = NextPosition(PickDirection());//pos = the returned value of NextPosition, and we pass it the returnd value of PickDirection as an argument

            if (!IsOverlap(pos))//if this room does not overlap another room[share the save vector3 cordinates]
            {
                roomsLocations.Add(pos);//then add it to the roomLocations list
            }
        }
    }

    private Vector3 NextPosition(Vector2 _direction)
    {
        // take in a vector2 direction, and multily it by the room size
        Vector3 newPos = new Vector3(_direction.x * roomSize, 0f, _direction.y * roomSize);
        currentPos += newPos;//add that to the current position
        return currentPos;
    }

    private Vector2 PickDirection()
    {
        int dir = Random.Range(1, 5);//pick a direction

        Vector2 directionVector = Vector2.zero;//initialize a vector2

        if (dir == 1)//left
        {
            directionVector = Vector2.left;//set the vector2 to vector2.left
        }

        if (dir == 2)//right
        {
            directionVector = Vector2.right;
        }

        if (dir == 3)//up
        {
            directionVector = Vector2.up;
        }

        if (dir == 4)//down
        {
            directionVector = Vector2.down;
        }

        return directionVector;//return the vector 2
    }

    private bool IsOverlap(Vector3 _pos)//check if room is already at this location
    {
        if (roomsLocations.Contains(_pos))
        {
            return true;
        }

        return false;
    }


    private void SpawnInRooms()
    {
        Instantiate(startingRoomPrefab, roomsLocations[0], Quaternion.identity);//spawn in our staring room

        for (int i = 1; i < roomsLocations.Count; i++)//starting at 1 loop till the end of roomLocations list
        {
            int randRoom = Random.Range(0, rooms.Count);//pick a random room from the room prefabs list
            Instantiate(rooms[randRoom], roomsLocations[i], Quaternion.identity);//spawn it at roomLocations[i]'s position
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < roomsLocations.Count; i++)//loop through all room locations
        {
            Gizmos.DrawWireCube(roomsLocations[i], new Vector3(roomSize, 1f, roomSize));//draw a wire cube at roomLocations[i], and a vector3 size of (roomsize, 1f, roomsize);
        }
    }
}
