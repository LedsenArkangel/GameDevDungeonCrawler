using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeractions : MonoBehaviour
{
    [Header("Main stats")]
    public float movementSpeed = 5f;
    public float attackManaCost = 10f;

    [Header("Projectiles")]
    public GameObject ninjaStar;

    private Playerstats stats;

    void Start()
    {
        stats = gameObject.GetComponent<Playerstats>();
    }
    
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        //movements
        if (Input.GetKey("w"))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - movementSpeed * Time.deltaTime, gameObject.transform.position.y);
        }
        if (Input.GetKey("s"))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + movementSpeed * Time.deltaTime, gameObject.transform.position.y);
        }

        //attack
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
    }
}
