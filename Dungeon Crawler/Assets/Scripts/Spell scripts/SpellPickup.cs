using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    public GameObject[] spellProjectiles;
    public GameObject spell;
    public GameObject castEffect;

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
            if (GameObject.FindGameObjectWithTag("Announcer") != null) GameObject.FindGameObjectWithTag("Announcer").GetComponent<AnnounceTextScript>().announce("Spell learned", 2.5f, Color.blue);
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
        string projectileTypeName = "";
        if(rng1 <= 55)
        {
            generatedSpellType = SpellBase.Spell.PROJECTILE;
            projectileTypeName = "";
        }
        else if (rng1 <= 60)
        {
            generatedSpellType = SpellBase.Spell.FOURDIRECTIONALNOVA;
            powerBudget -= 2f;
            projectileTypeName = "nova";
        }
        else if (rng1 <= 65)
        {
            generatedSpellType = SpellBase.Spell.FOURDIRECTIONALPOINT;
            powerBudget -= 6f;
            projectileTypeName = "cross";
        }
        else if (rng1 <= 80)
        {
            generatedSpellType = SpellBase.Spell.BARRAGE3;
            powerBudget -= 5f;
            projectileTypeName = "spray";
        }
        else if (rng1 <= 90)
        {
            generatedSpellType = SpellBase.Spell.BARRAGE3WIDE;
            powerBudget -= 4f;
            projectileTypeName = "spray";
        }
        else
        {
            generatedSpellType = SpellBase.Spell.BARRAGE5;
            powerBudget -= 7f;
            projectileTypeName = "barrage";
        }

        //depending on the projectile power, reduce balancing
        GameObject generatedProjectile = spellProjectiles[Random.Range(0,spellProjectiles.Length)];
        if (generatedProjectile.GetComponent<ProjectileScript>() != null) powerBudget -= generatedProjectile.GetComponent<ProjectileScript>().powerValue;
        string projectileClassName = generatedProjectile.GetComponent<ProjectileScript>().projectileName;

        //use projectile sprite
        Sprite generatedSpellIcon;
        generatedSpellIcon = generatedProjectile.GetComponentInChildren<SpriteRenderer>().sprite;

        //generate name accordingly
        string generatedSpellName;
        generatedSpellName = projectileClassName + " " + projectileTypeName;
        
        //randomize ammo
        float generatedMaxAmmo = 4;
        float generatedAmmoRegenPerSecond = 2f;
        float rng2 = Random.Range(0, 100);
        if (rng2 <= 25)
        {
            generatedMaxAmmo = Mathf.Max(powerBudget, 1);

            generatedAmmoRegenPerSecond = Mathf.Max(powerBudget,2f) / 6f;
        }
        else if (rng2 <= 50)
        {
            generatedMaxAmmo = 2;
            generatedAmmoRegenPerSecond = 0.8f;
            powerBudget += 2;
        }
        else if (rng2 <= 60)
        {
            generatedMaxAmmo = 4;
            generatedAmmoRegenPerSecond = 4f / powerBudget;
            powerBudget += 2;
        }
        else if (rng2 <= 80)
        {
            if (powerBudget > 3)
            {
                generatedMaxAmmo = Mathf.Max(powerBudget * 2 - 4, 1);
                generatedAmmoRegenPerSecond = 0.3f;
            }
            else
            {
                generatedMaxAmmo = 2;
                generatedAmmoRegenPerSecond = 0.45f;
            }
        }
        else
        {
            generatedMaxAmmo = Mathf.Min(Mathf.Max(powerBudget * 2,6f),10f);
            generatedAmmoRegenPerSecond = 0.5f;
            powerBudget -= 2f;
        }

        //mana cost depends on power budget
        float generatedManaCost=0f;
        generatedManaCost = Mathf.Max(0f,-powerBudget * 5f + 25);
        
        spell = new GameObject();
        //attach new spellscript to spell
        spell.AddComponent<SpellBase>();
        spell.GetComponent<SpellBase>().EditSpellBase(generatedSpellIcon, generatedSpellName, generatedManaCost, generatedSpellType, generatedProjectile, generatedMaxAmmo, generatedAmmoRegenPerSecond, castEffect); ;
    }
    
}
