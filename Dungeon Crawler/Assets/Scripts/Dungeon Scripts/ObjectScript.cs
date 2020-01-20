using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    [Header("Main stats")]
    public float maxHp = 100f;

    private float currentHp = 100;

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) Destroy(gameObject);
    }
}
