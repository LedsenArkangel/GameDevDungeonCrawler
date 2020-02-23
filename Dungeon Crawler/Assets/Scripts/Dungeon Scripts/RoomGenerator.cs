using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject zombie;
    public GameObject spider;
    public GameObject fireZombie;
    public GameObject oilZombie;
    public GameObject wraith;
    public GameObject boss;
    public GameObject wall;

    public GameObject[] objects;

    public RoomType type = RoomType.BASIC;

    private int roomDifficulty;

    public void GenerateObjectsAndEnemies(int difficultyLevel)
    {
        roomDifficulty = difficultyLevel;
        
        Vector2 roomCenter = transform.position;
        if (type == RoomType.BASIC)
        {
            int amountOfWalls = Random.Range(0, 5);
            float distanceToNorthWall = 7.5f;
            float distanceToWestWall = 7.5f;

            int roomRng = Random.Range(0, 4); //Random.Range(0, 8);

            switch (roomRng)
            {
                //wall in the middle
                case 0:
                    int rngRotation = Random.Range(0, 4);
                    float rotation = 0;
                    if (rngRotation == 1) rotation = 45;
                    else if (rngRotation == 2)
                    {
                        int longWallRng = Random.Range(0, 4);
                        rotation = 90;
                        if(longWallRng == 2) Instantiate(wall, new Vector2(roomCenter.x + (distanceToWestWall + 2.25f) / 2f, roomCenter.y), Quaternion.Euler(new Vector3(0, 0, rotation)));
                        if (longWallRng == 3) Instantiate(wall, new Vector2(roomCenter.x - (distanceToWestWall + 2.25f) / 2f, roomCenter.y), Quaternion.Euler(new Vector3(0, 0, rotation)));
                    }
                    else if (rngRotation == 3) rotation = 135;
                    Instantiate(wall,roomCenter, Quaternion.Euler(new Vector3(0,0, rotation)));
                    break;
                
                //two walls in middle with = shape
                case 1:
                    int wallConnectRng = Random.Range(0, 3);
                    Instantiate(wall, new Vector2(roomCenter.x , roomCenter.y + (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                    Instantiate(wall, new Vector2(roomCenter.x, roomCenter.y - (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                    if (wallConnectRng == 0)
                    {
                        Instantiate(wall, new Vector2(roomCenter.x + (distanceToWestWall + 2.25f) / 2f, roomCenter.y + (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                        Instantiate(wall, new Vector2(roomCenter.x - (distanceToWestWall + 2.25f) / 2f, roomCenter.y - (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                    else if (wallConnectRng == 1)
                    {
                        Instantiate(wall, new Vector2(roomCenter.x + (distanceToWestWall + 2.25f) / 2f, roomCenter.y - (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                        Instantiate(wall, new Vector2(roomCenter.x - (distanceToWestWall + 2.25f) / 2f, roomCenter.y + (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                    break;
                //two walls near corners
                case 2:
                    int wallCornerRng = Random.Range(0, 3);
                    if (wallCornerRng == 0)
                    {
                        Instantiate(wall, new Vector2(roomCenter.x + (distanceToWestWall + 2.25f) / 2f, roomCenter.y + (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                        Instantiate(wall, new Vector2(roomCenter.x - (distanceToWestWall + 2.25f) / 2f, roomCenter.y - (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                    else if (wallCornerRng == 1)
                    {
                        Instantiate(wall, new Vector2(roomCenter.x + (distanceToWestWall + 2.25f) / 2f, roomCenter.y - (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                        Instantiate(wall, new Vector2(roomCenter.x - (distanceToWestWall + 2.25f) / 2f, roomCenter.y + (distanceToNorthWall - 2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                    else if (wallCornerRng == 2)
                    {
                        Instantiate(wall, new Vector2(roomCenter.x + (distanceToWestWall - 2.5f) / 2f, roomCenter.y - (distanceToNorthWall + 2.2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 0)));
                        Instantiate(wall, new Vector2(roomCenter.x - (distanceToWestWall - 2.5f) / 2f, roomCenter.y + (distanceToNorthWall + 2.2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                    else if (wallCornerRng == 3)
                    {
                        Instantiate(wall, new Vector2(roomCenter.x + (distanceToWestWall - 2.5f) / 2f, roomCenter.y + (distanceToNorthWall + 2.2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 0)));
                        Instantiate(wall, new Vector2(roomCenter.x - (distanceToWestWall - 2.5f) / 2f, roomCenter.y - (distanceToNorthWall + 2.2f) / 2f), Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                    break;
                default:
                    break;
            }

            //int enemyRng = Random.Range(0, 5); //Random.Range(0, 8);
            int enemyRng = 1;

            switch (enemyRng)
            {
                //zombie room
                case 0:
                    //generate X zombies
                    int amountOfZombies = Random.Range(1, roomDifficulty * 2);
                    for (int i = 0; i < amountOfZombies; i++)
                    {
                        Instantiate(zombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    break;
                //spider and zombie room
                case 1:
                    //generate X spiders
                    int amountOfSpiders = Random.Range(1, roomDifficulty);
                    for (int i = 0; i < amountOfSpiders; i++)
                    {
                        Instantiate(spider, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    //generate X zombies
                    int amountOfZombies2 = Random.Range(1, roomDifficulty);
                    for (int i = 0; i < amountOfZombies2; i++)
                    {
                        Instantiate(zombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    break;
                case 2:
                    //generate X wraiths
                    int amountOfWraiths = Random.Range(1, roomDifficulty + 1);
                    for (int i = 0; i < amountOfWraiths; i++)
                    {
                        Instantiate(wraith, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    break;
                case 3:
                    //generate X zombies and fire zombies
                    int amountOfFireZombies = Random.Range(1, roomDifficulty + 1);
                    for (int i = 0; i < amountOfFireZombies; i++)
                    {
                        Instantiate(fireZombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    //generate X zombies
                    int amountOfZombies3 = Random.Range(1, roomDifficulty + 2);
                    for (int i = 0; i < amountOfZombies3; i++)
                    {
                        Instantiate(zombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    break;
                case 4:
                    //generate X zombies, fire zombies and oil zombies
                    int amountOfFireZombies2 = Random.Range(1, roomDifficulty);
                    for (int i = 0; i < amountOfFireZombies2; i++)
                    {
                        Instantiate(fireZombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    //generate X zombies
                    int amountOfZombies4 = Random.Range(1, roomDifficulty);
                    for (int i = 0; i < amountOfZombies4; i++)
                    {
                        Instantiate(zombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    int amountOfOilZombies = Random.Range(1, roomDifficulty);
                    for (int i = 0; i < amountOfOilZombies; i++)
                    {
                        Instantiate(oilZombie, new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
                    }
                    break;
                default:
                    break;
            }
            
            //generate objects
            int amountOfObjects = Random.Range(1, 7);
            for (int k = 0; k < amountOfObjects; k++)
            {
                Instantiate(objects[Random.Range(0,objects.Length)], new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 1f, distanceToWestWall - 1f), roomCenter.y + Random.Range(-distanceToNorthWall + 1f, distanceToNorthWall - 1f)), Quaternion.identity);
            }
        }
        if (type == RoomType.BOSS)
        {
            GameObject bossObject = Instantiate(boss, roomCenter, Quaternion.identity);
        }
    }

    public enum RoomType
    {
        BASIC, BOSS
    }
}
