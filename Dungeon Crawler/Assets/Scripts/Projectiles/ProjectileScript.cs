using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Main attributes")]
    public float speed = 4f;
    public float damage = 10f;
    public bool bouncesOffWalls = false;
    public ProjectileType type = ProjectileType.IMPACT;

    [Header("Explosive attributes")]
    public float explosionRadius = 2f;
    public float explosionDamage = 15f;
    public float explosionForce = 10f;
    public GameObject explosionEffect;

    [Header("Visual")]
    public bool rotating = false;
    public float rotateSpeed = 10f;
    public GameObject destroyEffect;
    

    public void initializeProjectile(Vector2 moveDirection, Collider2D user)
    {
        moveDirection.Normalize();
        gameObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        if (user != null)Physics2D.IgnoreCollision(GetComponent<Collider2D>(),user);

    }

    void Update()
    {
        if (rotating && GetComponentInChildren<Transform>() != null) GetComponentInChildren<Transform>().Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit something");
        //hit player
        if (collision.gameObject.GetComponent<Playerstats>() != null)
        {
            if (type == ProjectileType.IMPACT)
            {
                collision.gameObject.GetComponent<Playerstats>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (type == ProjectileType.EXPLOSIVE)
            {
                collision.gameObject.GetComponent<Playerstats>().TakeDamage(damage);
                Explode();
                Destroy(gameObject);
            }
        }
        //hit enemy
        else if (collision.gameObject.GetComponent<Enemystats>() != null)
        {
            if(type == ProjectileType.IMPACT)
            {
                collision.gameObject.GetComponent<Enemystats>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (type == ProjectileType.EXPLOSIVE)
            {
                collision.gameObject.GetComponent<Enemystats>().TakeDamage(damage);
                Explode();
                Destroy(gameObject);
            }
        }
        //hit wall or other collider
        else
        {
            if(!bouncesOffWalls)Destroy(gameObject);
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        //get objects within radius and deal damage and apply force to them
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in collidersHit)
        {
            if (collider.gameObject.GetComponent<Playerstats>() != null)
            {
                collider.gameObject.GetComponent<Playerstats>().TakeDamage(explosionDamage);
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y).normalized * explosionForce);
            }
            if (collider.gameObject.GetComponent<Enemystats>() != null)
            {
                collider.gameObject.GetComponent<Enemystats>().TakeDamage(explosionDamage);
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y).normalized * explosionForce);
            }
        }
    }

    public enum ProjectileType
    {
        IMPACT, EXPLOSIVE
    }
}
