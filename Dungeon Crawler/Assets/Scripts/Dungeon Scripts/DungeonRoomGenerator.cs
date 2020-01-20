using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DungeonRoomGenerator : MonoBehaviour
{
    public GameObject BasicRoomTemplate;
    public GameObject BossRoomTemplate;
    public GameObject pathFindingMesh;
    public int level=0;
    public int bossRoom = 5;
    float roomOffSetPerLevel=15.25f;

    public void GenerateNextRoom()
    {
        level++;
        GameObject room;
        if(level != bossRoom)room = Instantiate(BasicRoomTemplate, new Vector2(0, roomOffSetPerLevel*level), Quaternion.identity);
        else room = Instantiate(BossRoomTemplate, new Vector2(0, roomOffSetPerLevel * level), Quaternion.identity);

        if (room.GetComponent<RoomGenerator>() != null) room.GetComponent<RoomGenerator>().GenerateObjectsAndEnemies(level);

        //updating pathfinding mesh
        /*
        GridGraph gg = pathFindingMesh.GetComponent<AstarPath>().data.AddGraph(typeof(GridGraph)) as GridGraph;
        gg.rotation = new Vector3(90f,0f,0f);
        gg.collision.mask.value = 13;
        gg.collision.heightCheck = false;
        gg.collision.use2D = true;
        int width = 32;
        int depth = 35;
        float nodeSize = 0.5f;
        gg.center = new Vector2(0, roomOffSetPerLevel * level);
        gg.SetDimensions(width, depth, nodeSize);
        Invoke("scanGraphs", 0.3f);
        */

        GridGraph gg = pathFindingMesh.GetComponent<AstarPath>().data.gridGraph;
        Debug.Log("0" + gg.width);
        int width = gg.width;
        int depth = gg.depth + 60;
        float nodeSize = gg.nodeSize;
        gg.SetDimensions(width, depth, nodeSize);
        //TODO move graph to map center for efficiency
        Invoke("scanGraphs", 0.1f);
    }

    public void scanGraphs()
    {
        // Scans all graphs
        AstarPath.active.Scan();
    }
}
