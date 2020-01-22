using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    public Sprite spellIcon;
    public string spellName;
    public float manaCost;
    public Spell spell;
    public GameObject projectile;

    [Header("Visual effects")]
    public GameObject spellUseEffectPrimary;
    public GameObject spellUseEffectSecondary;
    public GameObject spellUseEffectSTetriary;

    public void useSpell(GameObject player)
    {
        if (player.GetComponent<Playerstats>() == null) return;

        if(player.GetComponent<Playerstats>().useMana(manaCost))
        {

            switch (spell)
            {
                case Spell.NINJASTAR:
                    GameObject ninjastar = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    ninjastar.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    break;
                case Spell.FIREPOTION:
                    GameObject firepotion = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    firepotion.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    break;
                case Spell.HEAL:
                    player.GetComponent<Playerstats>().heal(10);
                    if (spellUseEffectPrimary != null) Instantiate(spellUseEffectPrimary, player.transform.position, Quaternion.identity);
                    break;
                case Spell.SMITE:
                    if (GetComponent<Smite>() != null) GetComponent<Smite>().useSmite(player, Camera.main.ScreenToWorldPoint(Input.mousePosition), spellUseEffectPrimary, spellUseEffectSecondary);
                    break;
                case Spell.POISONBOLT:
                    GameObject poisonBolt = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    poisonBolt.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    break;
                case Spell.FROSTBOLT:
                    GameObject frostBolt = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    frostBolt.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    break;
                default:
                    Debug.Log("spell not defined");
                    break;
            }
            
        }
    }

    Vector2 getDirectionFromMouseToPlayer(GameObject player)
    {
        Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionMouse.z = transform.position.z;
        Vector3 towardsMouseFromPlayer = positionMouse - player.transform.position;
        return new Vector2(towardsMouseFromPlayer.x, towardsMouseFromPlayer.y);
    }

    public enum Spell
    {
        UNDEFINED, NINJASTAR, FIREPOTION, HEAL, SMITE, POISONBOLT, FROSTBOLT
    }

}
