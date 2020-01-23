using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [Header("Main stats")]
    public float maxHp = 100f;

    [Header("Explosive attributes")]
    public bool explodes = false;
    public float explosionRadius = 2f;
    public float explosionDamage = 15f;
    public float explosionForce = 10f;
    public GameObject explosionEffect;

    public GameObject aftermathObject;

    private float currentHp = 100;

    private void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) breakObject();
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        currentHp -= damage;
        if (currentHp < 0) breakObject();
    }

    public void Explode()
    {
        explodes = false;
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
                collider.gameObject.GetComponent<ObjectScript>().TakeDamage(explosionDamage * 2f);
                if (collider.gameObject.GetComponent<Rigidbody2D>() != null) collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y).normalized * explosionForce);
            }
        }
    }

    public void breakObject()
    {
        if (explodes) Explode();
        if(aftermathObject != null) Instantiate(aftermathObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
