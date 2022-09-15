using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControl : MonoBehaviour
{
    public List<GameObject> rooms;
    public int totalRooms;

    GameObject lastRoom;
    List<GameObject> savedRooms;
    DoorControl door;
    public List<RandomizeRooms> randomRooms;

    void Start()
    {
        door = GetComponent<DoorControl>();
        MapGenerator();
    }
    public void MapGenerator()
    {
        savedRooms = new List<GameObject>();

        Vector2 finalPos = transform.position;
        GameObject initRoom = Instantiate(rooms[0], finalPos, Quaternion.identity);
        lastRoom = initRoom;
        savedRooms.Add(initRoom);

        for (int i = 0; i < totalRooms; i++)
        {
            Vector2 finalPosR = transform.position;
            int randomRoom = Random.Range(4, rooms.Count); ///CAMBIAR EL NUMERO SEGUN LAS ROOMS QUE META
            int checkRoom = GetConstantRoom(i);
            if (checkRoom != -1)
            {
                randomRoom = checkRoom;
            }
            finalPosR = lastRoom.transform.Find("END").position;
            GameObject newRoom = Instantiate(rooms[randomRoom], finalPosR, Quaternion.identity);
            newRoom.transform.Find("PuertaL").GetComponent<DoorControl>().teleporPoint = lastRoom.transform.Find("PuertaR/ExitPoint");
            lastRoom.transform.Find("PuertaR").GetComponent<DoorControl>().teleporPoint = newRoom.transform.Find("PuertaL/EnterPoint");

            savedRooms.Add(newRoom);
            lastRoom = newRoom;
        }
    }
    private int GetConstantRoom(int _index)
    {
        for (int i = 0; i < randomRooms.Count; i++)
        {
            if(randomRooms[i].index == _index)
            {
                int roomRandom = Random.Range(0, randomRooms[i].room.Count);

                return randomRooms[i].room[roomRandom];
            }
        }
        return -1;
    }

    void Update()
    {

    }
}

[System.Serializable]
public class RandomizeRooms
{
    public int index;
    public List<int> room;
}