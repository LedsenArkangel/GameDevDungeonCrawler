using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playeractions : MonoBehaviour
{
    [Header("Main stats")]
    public float attackManaCost = 10f;
    public float potionAttackManaCost = 10f;

    [Header("Spells")]
    public List<GameObject> spells;

    [Header("Spell display")]
    public Text spellNameTextDisplay;
    public Image spellIconDisplay;
    public Text spellAmmoDisplay;


    [Header("Magic boots attributes")]
    public float boostForce = 800f;
    public float boostManaCost = 10f;
    public GameObject bootsBoostEffect;

    private Playerstats stats;
    private int equippedSpell = 0;

    void Start()
    {
        stats = gameObject.GetComponent<Playerstats>();
        UpdateSpellDisplay();
    }
    
    void Update()
    {
        UpdateAmmo();

        //swap weapons
        if (Input.GetKeyDown("e"))
        {
            if (equippedSpell < spells.Count -1) equippedSpell++;
            else equippedSpell = 0;
            UpdateSpellDisplay();
        }
        if (Input.GetKeyDown("q"))
        {
            if (equippedSpell > 0) equippedSpell--;
            else equippedSpell = spells.Count - 1;
            UpdateSpellDisplay();
        }

        //left click uses equipped spell
        if (Input.GetMouseButtonDown(0))
        {
            if (spells[equippedSpell].GetComponent<SpellBase>() != null) spells[equippedSpell].GetComponent<SpellBase>().useSpell(gameObject);
        }

        //right click uses magic boots
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionMouse.z = transform.position.z;
            Vector3 towardsMouseFromPlayer = positionMouse - gameObject.transform.position;
            GameObject boostEffect = Instantiate(bootsBoostEffect, transform.position, Quaternion.identity);
            Destroy(boostEffect, 0.5f);
            if(stats.useMana(boostManaCost))gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(towardsMouseFromPlayer.x, towardsMouseFromPlayer.y).normalized * boostForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //hit player
        if (collision.gameObject.GetComponent<DoorScript>() != null)
        {
            collision.gameObject.GetComponent<DoorScript>().TryOpenDoor();
        }
    }

    void UpdateAmmo()
    {
        foreach (GameObject spell in spells)
        {
            if (spell.GetComponent<SpellBase>() != null)
            {
                if (spell.GetComponent<SpellBase>().currentAmmo < spell.GetComponent<SpellBase>().maxAmmo)
                {
                    spell.GetComponent<SpellBase>().currentAmmo += spell.GetComponent<SpellBase>().ammoRegenPerSecond * Time.deltaTime;
                }
            }
        }

        if (spells[equippedSpell].GetComponent<SpellBase>() != null)
        {
            int currentAmmoDisplayValue = Mathf.RoundToInt(spells[equippedSpell].GetComponent<SpellBase>().currentAmmo);
            int maxAmmoDisplayValue = Mathf.RoundToInt(spells[equippedSpell].GetComponent<SpellBase>().maxAmmo);
            spellAmmoDisplay.text = "" + currentAmmoDisplayValue + " / " + maxAmmoDisplayValue;
        }
    }

    void UpdateSpellDisplay()
    {
        if (spells[equippedSpell].GetComponent<SpellBase>() != null)
        {
            spellNameTextDisplay.text = spells[equippedSpell].GetComponent<SpellBase>().spellName;
            spellIconDisplay.sprite = spells[equippedSpell].GetComponent<SpellBase>().spellIcon;
            int currentAmmoDisplayValue = Mathf.RoundToInt(spells[equippedSpell].GetComponent<SpellBase>().currentAmmo);
            int maxAmmoDisplayValue = Mathf.RoundToInt(spells[equippedSpell].GetComponent<SpellBase>().maxAmmo);
            spellAmmoDisplay.text = "" + currentAmmoDisplayValue + " / " + maxAmmoDisplayValue;
        }
    }

    public void LearnSpell(GameObject learnedSpell)
    {
        spells.Add(learnedSpell);
    }
}
