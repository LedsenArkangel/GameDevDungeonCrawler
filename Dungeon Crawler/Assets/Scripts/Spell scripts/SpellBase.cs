using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    public Sprite spellIcon;
    public string spellName;
    public float manaCost;
    public Spell spellType;
    public GameObject projectile;

    public float maxAmmo = 1;
    public float currentAmmo = 1;
    public float ammoRegenPerSecond = 1f;


    //TODO?
    [Header("Visual effects")]
    public GameObject spellUseEffectPrimary;
    public GameObject spellUseEffectSecondary;
    public GameObject spellUseEffectSTetriary;

    public void EditSpellBase(Sprite spellIcon, string spellName, float manaCost, Spell spellType, GameObject projectile, float maxAmmo, float ammoRegenPerSecond)
    {
        this.spellIcon = spellIcon;
        this.spellName = spellName;
        this.manaCost = manaCost;
        this.spellType = spellType;
        this.projectile = projectile;
        this.maxAmmo = maxAmmo;
        currentAmmo = maxAmmo;
        this.ammoRegenPerSecond = ammoRegenPerSecond;
    }

    public void useSpell(GameObject player)
    {
        if (player.GetComponent<Playerstats>() == null) return;
        if (currentAmmo < 0.5f) return;

        if(player.GetComponent<Playerstats>().useMana(manaCost))
        {
            currentAmmo -= 1f;

            switch (spellType)
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
                case Spell.BARRAGE5:
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
                case Spell.BARRAGE3WIDE:
                    GameObject projectileObjectFirst = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObjectFirst.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    GameObject projectileObjectSecond = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObjectSecond.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), 45), player.GetComponent<Collider2D>());
                    GameObject projectileObjectThird = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObjectThird.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), -45), player.GetComponent<Collider2D>());
                    break;
                case Spell.BARRAGE3:
                    GameObject projectileObjectOne = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObjectOne.GetComponent<ProjectileScript>().initializeProjectile(getDirectionFromMouseToPlayer(player), player.GetComponent<Collider2D>());
                    GameObject projectileObjectTwo = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObjectTwo.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), 22), player.GetComponent<Collider2D>());
                    GameObject projectileObjectThree = Instantiate(projectile, player.transform.position, Quaternion.identity);
                    projectileObjectThree.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(getDirectionFromMouseToPlayer(player), -22), player.GetComponent<Collider2D>());
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
        UNDEFINED, PROJECTILE, SMITE, HEAL, BARRAGE5, BARRAGE3WIDE, BARRAGE3
    }

}
