using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject zombie;
    public GameObject wall;
    public RoomType type = RoomType.BASIC;

    private int roomDifficulty;

    public void GenerateObjectsAndEnemies(int difficultyLevel)
    {
        roomDifficulty = difficultyLevel;
        
        int amountOfWalls = Random.Range(0,5);
        Vector2 roomCenter = transform.position;
        float distanceToNorthWall = 7.5f;
        float distanceToWestWall = 7.5f;
        //TODO: generate objects

        //generate walls
        for(int j = 0; j<amountOfWalls; j++)
        {
            //split room into 3x3
            //each row and column must have at least 1 wall-less block
            int rngWallPlacementBlock = Random.Range(0, 9);
            

            /*
            float rngX = roomCenter.x + Random.Range(-roomCorner.x, roomCorner.x);
            float rngY = roomCenter.y + Random.Range(-roomCorner.y, roomCorner.y);
            int rngRotation = Random.Range(0,8);
            */

            /*
            switch (rng)
            {
                //walls on left (25%)
                case 0:
                    break;
                //walls on right (25%)
                case 1:
                    break;
                //walls on middle horizontally(25%)
                case 2:
                    break;
                //walls on middle vertically(25%)
                case 3:
                    break;
                default:
                    break;
            }
            */
            
            //check that wall positions can't trap anything (always path up from mid, left or right and give at least room of 1.5f for player, enter door and exit door)
        }


        //TODO: generate enemies

        //generate zombies if basic room
        int amountOfZombies = Random.Range(1, roomDifficulty * 2);
        for (int i = 0; i<amountOfZombies; i++)
        {
            Instantiate(zombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
        }
    }

    public enum RoomType
    {
        BASIC
    }
}
