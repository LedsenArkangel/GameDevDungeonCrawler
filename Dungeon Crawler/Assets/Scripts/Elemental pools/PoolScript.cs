﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{
    public float lifeTime = 20f;
    public float damagePerSecond = 0f;
    public PoolType type = PoolType.BLOOD;
    public DamageType damageType = DamageType.BASIC;

    [Header("Explosive attributes")]
    public bool explodes = false;
    public DamageType explosionCatalyst = DamageType.FIRE;
    public float explosionRadius = 2f;
    public float explosionDamage = 15f;
    public float explosionForce = 10f;
    public GameObject explosionEffect;
    public GameObject afterMath;

    // Update is called once per frame
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //projectile over pool
        if (collision.gameObject.GetComponent<ProjectileScript>() != null)
        {
            if (explodes)
            {
                if ((type == PoolType.OIL || type == PoolType.ACID) && collision.gameObject.GetComponent<ProjectileScript>().damageType.Equals(DamageType.FIRE))
                {
                    Explode();
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
        }

        Instantiate(afterMath, transform.position, Quaternion.identity);
    }


    public enum PoolType
    {
        BLOOD, OIL, FIRE, ACID
    }
}