using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    [Header("Main attributes")]
    public float speed = 4f;
    public float hookForce = 1000f;

    private bool attached = false;
    private GameObject player;
    private GameObject target;

    public void initializeHook(Vector2 moveDirection, GameObject player)
    {
        this.player = player;
        moveDirection.Normalize();
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }

    void Update()
    {
        //pull player and target towards hook once attached
        if (attached)
        {
            Vector2 directionFromPlayer = transform.position - player.transform.position;
            player.GetComponent<Rigidbody2D>().AddForce(directionFromPlayer.normalized * hookForce * Time.deltaTime);
            if(target.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 directionFromTarget = transform.position - player.transform.position;
                target.GetComponent<Rigidbody2D>().AddForce(directionFromTarget.normalized * hookForce * Time.deltaTime);
            }
        }

        //keep the end of hook attached to target
        if (attached)
        {
            if (target.GetComponent<Rigidbody2D>() != null)
            {
                gameObject.transform.parent = target.transform;
            }
        }

        //draw hook linerender from it to player
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attached)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), false);
            attached = true;
            target = collision.gameObject;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }
}
