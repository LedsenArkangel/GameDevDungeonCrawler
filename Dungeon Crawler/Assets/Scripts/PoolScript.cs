using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{

    public float lifeTime = 20f;
    public float damagePerSecond = 0f;
    public PoolType type = PoolType.BLOOD;

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
            if (type == PoolType.FIRE || type == PoolType.ACID) collision.gameObject.GetComponent<Playerstats>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
        //enemy in pool
        else if (collision.gameObject.GetComponent<Enemystats>() != null)
        {
            if (type == PoolType.FIRE || type == PoolType.ACID) collision.gameObject.GetComponent<Enemystats>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
        //projectile over pool
        else if (collision.gameObject.GetComponent<ProjectileScript>() != null)
        {

        }
    }


    public enum PoolType
    {
        BLOOD, OIL, FIRE, ACID
    }
}
