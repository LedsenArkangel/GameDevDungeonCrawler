using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject zombie;
    public GameObject wall;

    private int roomDifficulty;

    public void GenerateObjectsAndEnemies(int difficultyLevel)
    {
        roomDifficulty = difficultyLevel;

        //TODO: generate objects
        int amountOfWalls = Random.Range(0,2);
        //check that wall positions do not stop player advancement
        //instantiate wall


        //TODO: generate enemies
        //instantiate zombie
    }
}
