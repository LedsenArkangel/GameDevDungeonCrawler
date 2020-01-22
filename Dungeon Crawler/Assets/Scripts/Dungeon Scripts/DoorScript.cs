using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    DungeonRoomGenerator roomGenerator;

    // Start is called before the first frame update
    void Start()
    {
        roomGenerator= GameObject.FindGameObjectWithTag("RoomGenerator").GetComponent<DungeonRoomGenerator>();
    }

    public bool TryOpenDoor()
    {
        //if no enemies present, door can be opened
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            roomGenerator.GenerateNextRoom();
            Destroy(gameObject, 0.05f);
            return true;
        }
        return false;
    }
}
