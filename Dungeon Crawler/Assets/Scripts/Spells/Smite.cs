using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite : MonoBehaviour
{
    public Vector2 position;
    public float delay = 1f;
    public float damage = 20f;
    public float smiteSize = 3f;
    public float smiteForce = 200f;

    public void useSmite(GameObject player, Vector3 targetPosition,  GameObject spellUseEffectPrimary, GameObject spellUseEffectSecondary)
    {
        position = new Vector2(targetPosition.x, targetPosition.y);
        if (spellUseEffectPrimary != null) Instantiate(spellUseEffectPrimary, position, Quaternion.identity);
        if (spellUseEffectSecondary != null) Instantiate(spellUseEffectSecondary, position, Quaternion.identity);

        Invoke("doSmiteEffect", delay);
    }

    void doSmiteEffect()
    {
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(position, smiteSize);
        foreach (Collider2D collider in collidersHit)
        {
            if (collider.gameObject.GetComponent<Enemystats>() != null)
            {
                collider.gameObject.GetComponent<Enemystats>().TakeDamage(damage);
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - position.x, collider.transform.position.y - position.y).normalized * smiteForce);
            }
            if (collider.gameObject.GetComponent<ObjectScript>() != null)
            {
                collider.gameObject.GetComponent<ObjectScript>().TakeDamage(damage/2f);
                if (collider.gameObject.GetComponent<Rigidbody2D>() != null) collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider.transform.position.x - position.x, collider.transform.position.y - position.y).normalized * smiteForce);
            }
        }
    }
}
