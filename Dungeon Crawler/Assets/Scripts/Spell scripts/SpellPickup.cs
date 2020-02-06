using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    public GameObject[] spellProjectiles;
    public GameObject spell;

    void Start()
    {
        generateRandomSpell();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //hit player
        if (collision.gameObject.GetComponent<Playeractions>() != null)
        {
            collision.gameObject.GetComponent<Playeractions>().LearnSpell(spell);
            Destroy(gameObject);
        }
    }


        public void generateRandomSpell()
    {
        //goal is to get powerBudget as close to zero as possible
        float powerBudget = 9f;

        //projectile, or barrage(reduces balance)?
        float rng1 = Random.Range(0,100);
        SpellBase.Spell generatedSpellType = SpellBase.Spell.PROJECTILE;
        if(rng1 <= 75)
        {
            generatedSpellType = SpellBase.Spell.PROJECTILE;
        }
        else if (rng1 <= 85)
        {
            generatedSpellType = SpellBase.Spell.BARRAGE3;
            powerBudget -= 5f;
        }
        else if (rng1 <= 95)
        {
            generatedSpellType = SpellBase.Spell.BARRAGE3WIDE;
            powerBudget -= 4f;
        }
        else if (rng1 > 0)
        {
            generatedSpellType = SpellBase.Spell.BARRAGE5;
            powerBudget -= 7f;
        }

        //depending on the projectile power, reduce balancing
        GameObject generatedProjectile = spellProjectiles[Random.Range(0,spellProjectiles.Length)];
        if (generatedProjectile.GetComponent<ProjectileScript>() != null) powerBudget -= generatedProjectile.GetComponent<ProjectileScript>().powerValue;

        //use projectile sprite
        Sprite generatedSpellIcon;
        generatedSpellIcon = generatedProjectile.GetComponentInChildren<SpriteRenderer>().sprite;

        //generate name accordingly
        string generatedSpellName;
        generatedSpellName = "custom spell";

        //depending on ammo, reduce or increase balance
        float generatedMaxAmmo = 4;
        float generatedAmmoRegenPerSecond = 2f;

        float rng2 = Random.Range(0, 100);
        if (rng2 <= 25)
        {
            if(powerBudget>1)generatedMaxAmmo = powerBudget;
            else generatedMaxAmmo = 1;

            generatedAmmoRegenPerSecond = 1f / powerBudget;
        }
        else if (rng2 <= 50)
        {
            generatedMaxAmmo = 2;
            generatedAmmoRegenPerSecond = 0.5f;
            powerBudget += 2;
        }
        else if (rng2 <= 75)
        {
            if (powerBudget > 2)
            {
                generatedMaxAmmo = powerBudget * 2 - 3;
                generatedAmmoRegenPerSecond = 0.25f;
            }
            else
            {
                generatedMaxAmmo = 1;
                generatedAmmoRegenPerSecond = 0.3f;
            }
        }
        else if (rng2 > 0)
        {
            generatedMaxAmmo = 4;
            generatedAmmoRegenPerSecond = 0.35f;
            powerBudget -= 1f;
        }

        //mana cost depends on power budget
        float generatedManaCost=0f;
        generatedManaCost = Mathf.Max(0f,-powerBudget * 5f + 25);

        //spell = new gameobject
        spell = new GameObject();
        //attach new spellscript to spell
        spell.AddComponent<SpellBase>();
        spell.GetComponent<SpellBase>().EditSpellBase(generatedSpellIcon, generatedSpellName, generatedManaCost, generatedSpellType, generatedProjectile, generatedMaxAmmo, generatedAmmoRegenPerSecond); ;
    }
    
}
