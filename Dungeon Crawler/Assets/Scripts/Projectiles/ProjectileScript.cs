using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Main attributes")]
    public float speed = 4f;
    public float damage = 10f;
    public int bounceAmount = 0;
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
    public bool hasPointDirection = false;

    private Vector2 movementDirection;
    private Transform target;

    public void initializeProjectile(Vector2 moveDirection, Collider2D user)
    {
        moveDirection.Normalize();
        movementDirection = moveDirection;
        gameObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        if (user != null)Physics2D.IgnoreCollision(GetComponent<Collider2D>(),user);
    }

    public void initializeProjectile(Vector2 moveDirection, Collider2D user, Transform target)
    {
        moveDirection.Normalize();
        movementDirection = moveDirection;
        gameObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        if (user != null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), user);
        this.target = target;
    }
    
    void Update()
    {
        if (rotating && GetComponentInChildren<Transform>() != null) GetComponentInChildren<Transform>().Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        if (hasPointDirection && GetComponentInChildren<Transform>() != null)
        {

            movementDirection = GetComponent<Rigidbody2D>().velocity.normalized;
            if (movementDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                GetComponentInChildren<Transform>().rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        if(type == ProjectileType.MISSILE)
        {
            if(target != null)
            {
                Vector2 directionOfTarget = target.position - transform.position;
                directionOfTarget.Normalize();
                gameObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(directionOfTarget.x * speed, directionOfTarget.y * speed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //hit player
        if (collision.gameObject.GetComponent<Playerstats>() != null)
        {
            if (type == ProjectileType.IMPACT || type == ProjectileType.MISSILE)
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
            if(type == ProjectileType.IMPACT || type == ProjectileType.MISSILE)
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
        //hit object
        else if (collision.gameObject.GetComponent<ObjectScript>() != null)
        {
            if (type == ProjectileType.IMPACT || type == ProjectileType.MISSILE)
            {
                collision.gameObject.GetComponent<ObjectScript>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (type == ProjectileType.EXPLOSIVE)
            {
                collision.gameObject.GetComponent<ObjectScript>().TakeDamage(damage);
                Explode();
                Destroy(gameObject);
            }
        }
        //hits a wall or other collider
        else
        {
            if (bounceAmount <= 0)
            {
                if (type == ProjectileType.EXPLOSIVE) Explode();
                Destroy(gameObject);
            }
            bounceAmount--;
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
            if (collider.gameObject.GetComponent<ObjectScript>() != null)
            {
                collider.gameObject.GetComponent<ObjectScript>().TakeDamage(damage / 2f);
                if (collider.gameObject.GetComponent<Rigidbody2D>() != null) collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y).normalized * explosionForce);
            }
        }
    }

    public enum ProjectileType
    {
        IMPACT, EXPLOSIVE, MISSILE
    }
}
