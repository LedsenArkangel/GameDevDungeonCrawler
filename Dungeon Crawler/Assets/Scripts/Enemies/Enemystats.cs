using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemystats : MonoBehaviour
{
    [Header("Main stats")]
    public float maxHp = 100f;

    [Header("Life bar")]
    public RectTransform lifebarBackground;
    public RectTransform lifebarForeground;

    [Header("on death effects")]
    public GameObject deathVisualEffect;
    public GameObject bloodPool;

    private float currentHp = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        lifebarForeground.sizeDelta = new Vector2(lifebarBackground.sizeDelta.x * (currentHp / maxHp), lifebarForeground.sizeDelta.y);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) die();
    }
    
    public void die()
    {
        if (deathVisualEffect != null)Instantiate(deathVisualEffect, transform.position, Quaternion.identity);
        if (bloodPool != null) Instantiate(bloodPool, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
