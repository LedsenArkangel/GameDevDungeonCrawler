using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{
    public float lifeTime = 20f;
    public float damagePerSecond = 0f;
    public PoolType type = PoolType.BLOOD;
    public DamageType damageType = DamageType.BASIC;

    [Header("Explosive attributes")]
    public float explosionRadius = 2f;
    public float explosionDamage = 15f;
    public float explosionForce = 10f;
    public GameObject explosionEffect;

    [Header("Interaction attributes")]
    public bool interactsWithElements = false;
    public GameObject afterMath;

    
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) Destroy(gameObject);
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        //player in pool
        if (collision.gameObject.GetComponent<Playerstats>() != null)
        {
            if(type == PoolType.BLOOD)
            {
                float manaRegenPerSec = 10f;
                collision.gameObject.GetComponent<Playerstats>().gainMana(manaRegenPerSec * Time.deltaTime);
            }
            if (type == PoolType.FIRE || type == PoolType.ACID) collision.gameObject.GetComponent<Playerstats>().TakeDamage(damagePerSecond * Time.deltaTime, damageType);
        }
        //enemy in pool
        else if (collision.gameObject.GetComponent<Enemystats>() != null)
        {
            if (type == PoolType.FIRE || type == PoolType.ACID) collision.gameObject.GetComponent<Enemystats>().TakeDamage(damagePerSecond * Time.deltaTime, damageType);
        }
        //object in pool
        else if (collision.gameObject.GetComponent<ObjectScript>() != null)
        {
            if (type == PoolType.FIRE || type == PoolType.ACID) collision.gameObject.GetComponent<ObjectScript>().TakeDamage(damagePerSecond * Time.deltaTime, damageType);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactsWithElements)
        {
            //projectile over pool
            if (collision.gameObject.GetComponent<ProjectileScript>() != null)
            {
                //oil and acid explode from fire
                if ((type == PoolType.OIL || type == PoolType.ACID) && collision.gameObject.GetComponent<ProjectileScript>().damageType.Equals(DamageType.FIRE))
                {
                        Explode();
                        collision.gameObject.GetComponent<ProjectileScript>().destroyProjectile();
                        Destroy(gameObject);
                }
                
                //fire extingueshed by frost
                if(type == PoolType.FIRE && collision.gameObject.GetComponent<ProjectileScript>().damageType.Equals(DamageType.FROST))
                {
                    afterMathSpawn();
                    collision.gameObject.GetComponent<ProjectileScript>().destroyProjectile();
                    Destroy(gameObject);
                }

                //water gets poisoned
                if (type == PoolType.WATER && collision.gameObject.GetComponent<ProjectileScript>().damageType.Equals(DamageType.POISON))
                {
                    afterMathSpawn();
                    collision.gameObject.GetComponent<ProjectileScript>().destroyProjectile();
                    Destroy(gameObject);
                }
            }
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
                collider.gameObject.GetComponent<Playerstats>().TakeDamage(explosionDamage, damageType);
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y).normalized * explosionForce);
            }
            if (collider.gameObject.GetComponent<Enemystats>() != null)
            {
                collider.gameObject.GetComponent<Enemystats>().TakeDamage(explosionDamage, damageType);
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y).normalized * explosionForce);
            }
            if (collider.gameObject.GetComponent<ObjectScript>() != null)
            {
                collider.gameObject.GetComponent<ObjectScript>().TakeDamage(explosionDamage, damageType);
                if (collider.gameObject.GetComponent<Rigidbody2D>() != null) collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - transform.position.x, collider.transform.position.y - transform.position.y).normalized * explosionForce);
            }
            if(collider.gameObject.GetComponent<PoolScript>() != null)
            {
                collider.gameObject.GetComponent<PoolScript>().ignite();
            }
        }
        afterMathSpawn();
    }

    public void ignite()
    {
        if (type == PoolType.OIL || type == PoolType.ACID)
        {
            type = PoolType.BASIC;
            Explode();
            Destroy(gameObject);
        }
    }

    public void afterMathSpawn()
    {
        Instantiate(afterMath, transform.position, Quaternion.identity);
    }

    public enum PoolType
    {
        BLOOD, OIL, FIRE, ACID, WATER, BASIC
    }
}
