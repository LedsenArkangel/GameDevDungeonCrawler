using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    GameObject[] spellProjectiles;

    void Start()
    {
        //GENERATE SPELL
    }

    //collision with player => pickup spell

    
    public void generateRandomSpell()
    {
        //goal is to get powerBudget as close to zero as possible
        float powerBudget = 10f;

        //projectile, or barrage(reduces balance)?
        float rng1 = Random.Range(0,100);
        SpellBase.Spell generatedSpellType = SpellBase.Spell.PROJECTILE;
        if(rng1 <= 75)
        {
            generatedSpellType = SpellBase.Spell.PROJECTILE;
        }
        else if (rng1 <= 95)
        {
            generatedSpellType = SpellBase.Spell.BARRAGE3;
            powerBudget -= 3;
        }
        else if (rng1 > 0)
        {
            generatedSpellType = SpellBase.Spell.BARRAGE5;
            powerBudget -= 5;
        }

        //depending on the projectile power, reduce balancing
        GameObject generatedProjectile;

        //use projectile sprite
        Sprite generatedSpellIcon;

        //generate name accordingly
        string generatedSpellName;

        //depending on ammo, reduce or increase balance
        float generatedMaxAmmo = 1;
        float generatedAmmoRegenPerSecond = 1f;

        //depending on mana cost, reduce or increase balance
        float generatedManaCost;


        //spell = new gameobject
        GameObject spell = new GameObject();
        //attach new spellscript to spell
        //SpellBase spellbase = new SpellBase(generatedSpellIcon, generatedSpellName, generatedManaCost, generatedSpellType, generatedProjectile, generatedMaxAmmo, generatedAmmoRegenPerSecond);
    }
    
}
