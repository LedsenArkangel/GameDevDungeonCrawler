using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public float movementTreshHold = 0.01f;
    
    public Sprite idle;



    // Update is called once per frame
    void Update()
    {
        //idle
        if(aiPath.desiredVelocity.x <= movementTreshHold && aiPath.desiredVelocity.x > -movementTreshHold && aiPath.desiredVelocity.y <= movementTreshHold && aiPath.desiredVelocity.y > -movementTreshHold)
        {
            //TODO: use idle animation
        }
        //moving right
        else if(aiPath.desiredVelocity.x > movementTreshHold)
        {
            //TODO: use right animation
        }
        //moving left
        else if(aiPath.desiredVelocity.x < -movementTreshHold)
        {
            //TODO: use left animation
        }
        //moving down
        else if (aiPath.desiredVelocity.y < -movementTreshHold)
        {
            //TODO: use down animation
        }
        //moving up
        else if (aiPath.desiredVelocity.y > movementTreshHold)
        {
            //TODO: use up animation
        }
    }
}
