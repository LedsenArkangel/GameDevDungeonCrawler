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
    public GameObject spellUseEffect;

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
                    break;
                default:
                    Debug.Log("spell not defined");
                    break;
            }

            if(spellUseEffect != null) Instantiate(spellUseEffect, player.transform.position, Quaternion.identity);
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
        UNDEFINED, NINJASTAR, FIREPOTION, HEAL
    }

}
