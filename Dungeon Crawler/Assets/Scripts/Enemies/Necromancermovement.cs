using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancermovement : MonoBehaviour
{
    [Header("Main stats")]
    public float movementSpotSearchRate = 10f;
    public float teleportRate = 20f;
    public float movementSpeed = 5f;

    [Header("Visuals")]
    public GameObject teleportEffect;


    private Vector2 moveTarget;
    private Vector2 roomCenter;
    private GameObject player;
    private float moveSearchCoolDown = 10f;
    private float teleportCoolDown = 10f;

    float distanceToNorthWall = 7.5f;
    float distanceToWestWall = 7.5f;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        roomCenter = transform.position;
        moveTowardsNewSpot();
    }

    void Update()
    {
        moveSearchCoolDown -= Time.deltaTime;
        if (moveSearchCoolDown <= 0)
        {
            moveSearchCoolDown = movementSpotSearchRate;
            moveTowardsNewSpot();
        }

        teleportCoolDown -= Time.deltaTime;
        if (teleportCoolDown <= 0)
        {
            teleportCoolDown = teleportRate;
            teleport();
        }

        Vector2 moveDirection = moveTarget - new Vector2(transform.position.x, transform.position.y);
        if (moveDirection.magnitude >= 0.5f)
        {
            moveDirection.Normalize();
            if (GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().velocity = moveDirection * movementSpeed;
        }
        else if (GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);

    }

    void teleport()
    {
        if (teleportEffect != null) Instantiate(teleportEffect, transform.position, Quaternion.identity);
        transform.position = new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 2f, distanceToWestWall - 2f), roomCenter.y + Random.Range(-distanceToNorthWall + 2f, distanceToNorthWall - 2f));
    }

    void moveTowardsNewSpot()
    {
        moveTarget = new Vector2(roomCenter.x + Random.Range(-distanceToWestWall + 2f, distanceToWestWall - 2f), roomCenter.y + Random.Range(-distanceToNorthWall + 2f, distanceToNorthWall - 2f));
    }
}
