using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject player;
    private float hpBarVisibilityRange = 4f;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).magnitude < hpBarVisibilityRange) {
            lifebarForeground.gameObject.SetActive(true);
            lifebarBackground.gameObject.SetActive(true);
            lifebarForeground.sizeDelta = new Vector2(lifebarBackground.sizeDelta.x * (currentHp / maxHp), lifebarForeground.sizeDelta.y);
        }
        else
        {
            lifebarForeground.gameObject.SetActive(false);
            lifebarBackground.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) die();
    }

    public void TakeDamage(float damage, DamageType damageType)
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

    public float getCurrentHp()
    {
        return currentHp;
    }
}
