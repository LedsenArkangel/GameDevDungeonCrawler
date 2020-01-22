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
    public int firstBossRoom = 10;
    float roomOffSetPerLevel=15.25f;

    public void GenerateNextRoom()
    {
        level++;
        GameObject room;
        if(level != firstBossRoom)room = Instantiate(BasicRoomTemplate, new Vector2(0, roomOffSetPerLevel*level), Quaternion.identity);
        else room = Instantiate(BossRoomTemplate, new Vector2(0, roomOffSetPerLevel * level), Quaternion.identity);

        if (room.GetComponent<RoomGenerator>() != null) room.GetComponent<RoomGenerator>().GenerateObjectsAndEnemies(level);

        GridGraph gg = pathFindingMesh.GetComponent<AstarPath>().data.gridGraph;
        Debug.Log("0" + gg.width);
        int width = gg.width;
        int depth = gg.depth + 60;
        float nodeSize = gg.nodeSize;
        gg.SetDimensions(width, depth, nodeSize);
        //TODO move graph to map center for pathfinding efficiency
        Invoke("scanGraphs", 0.1f);
    }

    public void scanGraphs()
    {
        // Scans all graphs
        AstarPath.active.Scan();
    }
}
