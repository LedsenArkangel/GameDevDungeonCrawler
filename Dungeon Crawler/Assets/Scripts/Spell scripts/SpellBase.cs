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

    public float maxAmmo = 1;
    public float currentAmmo = 1;
    public float ammoRegenPerSecond = 1f;

    [Header("Visual effects")]
    public GameObject spellUseEffectPrimary;
    public GameObject spellUseEffectSecondary;
    public GameObject spellUseEffectSTetriary;

    public void useSpell(GameObject player)
    {
        if (player.GetComponent<Playerstats>() == null) return;
        if (currentAmmo < 0.5f) return;

        if(player.GetComponent<Playerstats>().useMana(manaCost))
        {
            currentAmmo -= 1f;

            switch (spell)
            {
                case Spell.PROJECTILE:
                    GameObject projectileObject = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObject.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    break;
                case Spell.HEAL:
                    player.GetComponent<Playerstats>().heal(10);
                    if (spellUseEffectPrimary != null) Instantiate(spellUseEffectPrimary, player.transform.position, Quaternion.identity);
                    break;
                case Spell.SMITE:
                    if (GetComponent<Smite>() != null) GetComponent<Smite>().useSmite(player, Camera.main.ScreenToWorldPoint(Input.mousePosition), spellUseEffectPrimary, spellUseEffectSecondary);
                    break;
                case Spell.BARRAGE:
                    GameObject projectileObject1 = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObject1.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    GameObject projectileObject2 = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObject2.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), 45), player.GetComponent<Collider2D>());
                    GameObject projectileObject3 = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObject3.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), -45), player.GetComponent<Collider2D>());
                    GameObject projectileObject4 = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObject4.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), 22), player.GetComponent<Collider2D>());
                    GameObject projectileObject5 = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObject5.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), -22), player.GetComponent<Collider2D>());
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

    public Vector2 RotateVector(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public enum Spell
    {
        UNDEFINED, PROJECTILE, SMITE, HEAL, BARRAGE
    }

}
