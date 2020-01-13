using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeractions : MonoBehaviour
{
    [Header("Main stats")]
    public float movementForce = 1000f;
    public float maxMovementSpeed = 5f;
    public float attackManaCost = 10f;

    [Header("Projectiles")]
    public GameObject ninjaStar;
    public GameObject firePotion;

    private Playerstats stats;

    void Start()
    {
        stats = gameObject.GetComponent<Playerstats>();
    }
    
    void Update()
    {
        Vector2 playerVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        //movements
        if (Input.GetKey("w"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + movementSpeed * Time.deltaTime);
            if(playerVelocity.y < maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, movementForce * Time.deltaTime));
            //playerVelocity = new Vector2(playerVelocity.x, maxMovementSpeed);
        }
        if (Input.GetKey("a"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x - movementSpeed * Time.deltaTime, gameObject.transform.position.y);
            if (playerVelocity.x > -maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementForce * Time.deltaTime, 0f));
            //playerVelocity = new Vector2(-maxMovementSpeed, playerVelocity.y);
        }
        if (Input.GetKey("s"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - movementSpeed * Time.deltaTime);
            if (playerVelocity.y > -maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -movementForce * Time.deltaTime));
            //playerVelocity = new Vector2(playerVelocity.x, -maxMovementSpeed);
        }
        if (Input.GetKey("d"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x + movementSpeed * Time.deltaTime, gameObject.transform.position.y);
            if (playerVelocity.x < maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(movementForce * Time.deltaTime, 0f));
            //playerVelocity = new Vector2(maxMovementSpeed, playerVelocity.y);
        }


        //attack left click
        if (Input.GetMouseButtonDown(0))
        {
            if (stats.useMana(attackManaCost)) {
                GameObject shuriken = Instantiate(ninjaStar, gameObject.transform.position, Quaternion.identity);

                Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                positionMouse.z = transform.position.z;
                Vector3 towardsMouseFromPlayer = positionMouse - transform.position;

                shuriken.GetComponent<ProjectileScript>().initializeProjectile(new Vector2(towardsMouseFromPlayer.x, towardsMouseFromPlayer.y), gameObject.GetComponent<Collider2D>());
            }
        }

        //attack right click
        if (Input.GetMouseButtonDown(1))
        {
            if (stats.useMana(attackManaCost))
            {
                GameObject firePotionAttack = Instantiate(firePotion, gameObject.transform.position, Quaternion.identity);

                Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                positionMouse.z = transform.position.z;
                Vector3 towardsMouseFromPlayer = positionMouse - transform.position;

                firePotionAttack.GetComponent<ProjectileScript>().initializeProjectile(new Vector2(towardsMouseFromPlayer.x, towardsMouseFromPlayer.y), gameObject.GetComponent<Collider2D>());
            }
        }
    }
}
