using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerstats : MonoBehaviour
{
    [Header("Main stats")]
    public float maxHp = 100f;
    public float maxMana = 100f;
    public float manaRegenPerSecond = 10f;

    [Header("Life and mana bars")]
    public RectTransform lifebarBackground;
    public RectTransform lifebarForeground;
    public RectTransform manabarBackground;
    public RectTransform manabarForeground;

    private float currentHp = 100;
    private float currentMana = 100f;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        lifebarForeground.sizeDelta = new Vector2(lifebarBackground.sizeDelta.x * (currentHp/maxHp), lifebarForeground.sizeDelta.y);
        manabarForeground.sizeDelta = new Vector2(manabarBackground.sizeDelta.x * (currentMana / maxMana), manabarForeground.sizeDelta.y);

        if (currentMana < maxMana) currentMana += manaRegenPerSecond * Time.deltaTime;
    }

    //reduces mana, returns false if not possible
    public bool useMana(float cost)
    {
        if (cost <= currentMana)
        {
            currentMana -= cost;
            return true;
        }
        return false;
    }


    void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp=0;//die;
    }

}
