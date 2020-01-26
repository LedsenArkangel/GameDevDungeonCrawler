using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Main stats")]
    public float movementForce = 1000f;
    public float maxMovementSpeed = 5f;


    [Header("Visual effects")]
    public GameObject runEffect;
    public float runEffectCoolDown = 0.2f;
    public float runEffectDistanceFromPlayer = 0.2f;
    public Animator animator;

    private Playerstats stats;
    private float runEffectCoolDownTimer = 0.2f;

    void Start()
    {
        stats = gameObject.GetComponent<Playerstats>();
    }
    
    void Update()
    {
        Vector2 playerVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        animator.SetFloat("SpeedY", 0f);
        animator.SetFloat("SpeedX", 0f);
        //movements
        if (Input.GetKey("w"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + movementSpeed * Time.deltaTime);
            //playerVelocity = new Vector2(playerVelocity.x, maxMovementSpeed);
            if (playerVelocity.y < maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, movementForce * Time.deltaTime));
            DoRunEffect(new Vector2(transform.position.x, transform.position.y - runEffectDistanceFromPlayer));
            animator.SetFloat("SpeedY", 1f);
        }
        if (Input.GetKey("a"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x - movementSpeed * Time.deltaTime, gameObject.transform.position.y);
            //playerVelocity = new Vector2(-maxMovementSpeed, playerVelocity.y);
            if (playerVelocity.x > -maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementForce * Time.deltaTime, 0f));
            DoRunEffect(new Vector2(transform.position.x + runEffectDistanceFromPlayer, transform.position.y));
            animator.SetFloat("SpeedX", -1f);
        }
        if (Input.GetKey("s"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - movementSpeed * Time.deltaTime);
            //playerVelocity = new Vector2(playerVelocity.x, -maxMovementSpeed);
            if (playerVelocity.y > -maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -movementForce * Time.deltaTime));
            DoRunEffect(new Vector2(transform.position.x, transform.position.y + runEffectDistanceFromPlayer));
            animator.SetFloat("SpeedY", -1f);
        }
        if (Input.GetKey("d"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x + movementSpeed * Time.deltaTime, gameObject.transform.position.y);
            //playerVelocity = new Vector2(maxMovementSpeed, playerVelocity.y);
            if (playerVelocity.x < maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(movementForce * Time.deltaTime, 0f));
            DoRunEffect(new Vector2(transform.position.x - runEffectDistanceFromPlayer, transform.position.y));
            animator.SetFloat("SpeedX", 1f);
        }

        if (runEffectCoolDownTimer >= 0f) runEffectCoolDownTimer -= Time.deltaTime;
    }


    void DoRunEffect(Vector2 position)
    {
        if (runEffectCoolDownTimer <= 0f) {
            runEffectCoolDownTimer = runEffectCoolDown;
            GameObject runDust = Instantiate(runEffect, position, Quaternion.identity);
            Destroy(runDust, 0.7f);
        }
    }
}
