using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public float movementTreshHold = 0.01f;

    public bool isNecromancer= false;

    public Animator animator;


    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("SpeedY", 0f);
        animator.SetFloat("SpeedX", 0f);

        if (!isNecromancer)
        {
            //idle
            if (aiPath.desiredVelocity.x <= movementTreshHold && aiPath.desiredVelocity.x > -movementTreshHold && aiPath.desiredVelocity.y <= movementTreshHold && aiPath.desiredVelocity.y > -movementTreshHold)
            {
                animator.SetFloat("SpeedY", 0f);
                animator.SetFloat("SpeedX", 0f);
            }
            //moving right
            else if (aiPath.desiredVelocity.x > movementTreshHold && aiPath.desiredVelocity.x > Mathf.Abs(aiPath.desiredVelocity.y))
            {
                //use right animation
                animator.SetFloat("SpeedX", 1f);
                animator.SetFloat("SpeedY", 0f);
            }
            //moving left
            else if (aiPath.desiredVelocity.x < -movementTreshHold && -aiPath.desiredVelocity.x > Mathf.Abs(aiPath.desiredVelocity.y))
            {
                //use left animation
                animator.SetFloat("SpeedX", -1f);
                animator.SetFloat("SpeedY", 0f);
            }
            //moving down
            else if (aiPath.desiredVelocity.y < -movementTreshHold)
            {
                //use down animation
                animator.SetFloat("SpeedY", -1f);
                animator.SetFloat("SpeedX", 0f);
            }
            //moving up
            else if (aiPath.desiredVelocity.y > movementTreshHold)
            {
                //use up animation
                animator.SetFloat("SpeedY", 1f);
                animator.SetFloat("SpeedX", 0f);
            }
        }
        else
        {
            Vector2 movementVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            //idle
            if (movementVelocity.x <= movementTreshHold && movementVelocity.x > -movementTreshHold && movementVelocity.y <= movementTreshHold && movementVelocity.y > -movementTreshHold)
            {
                animator.SetFloat("SpeedY", 0f);
                animator.SetFloat("SpeedX", 0f);
            }
            //moving right
            else if (movementVelocity.x > movementTreshHold && movementVelocity.x > Mathf.Abs(movementVelocity.y))
            {
                //use right animation
                animator.SetFloat("SpeedX", 1f);
                animator.SetFloat("SpeedY", 0f);
            }
            //moving left
            else if (movementVelocity.x < -movementTreshHold && -movementVelocity.x > Mathf.Abs(movementVelocity.y))
            {
                //use left animation
                animator.SetFloat("SpeedX", -1f);
                animator.SetFloat("SpeedY", 0f);
            }
            //moving down
            else if (movementVelocity.y < -movementTreshHold)
            {
                //use down animation
                animator.SetFloat("SpeedY", -1f);
                animator.SetFloat("SpeedX", 0f);
            }
            //moving up
            else if (movementVelocity.y > movementTreshHold)
            {
                //use up animation
                animator.SetFloat("SpeedY", 1f);
                animator.SetFloat("SpeedX", 0f);
            }
        }
    }
}
