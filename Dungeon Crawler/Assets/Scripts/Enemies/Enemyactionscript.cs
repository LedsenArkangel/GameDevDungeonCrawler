using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemyactionscript : MonoBehaviour
{
    [Header("Main stats")]
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public float attacksPerSecond = 1f;
    public AttackType attackType = AttackType.MELEE;
    public GameObject attackProjectile;

    [Header("Visuals")]
    public GameObject attackEffect;

    [Header("Boss attributes")]
    public GameObject attackProjectile2;
    public GameObject attackProjectile3;
    public GameObject firePool;
    private bool phase2=false;


    private float attackCoolDown = 0f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if(GetComponent<AIDestinationSetter>() != null) GetComponent<AIDestinationSetter>().target = player.transform;
    }
    
    void Update()
    {
        if (attackCoolDown >= 0) attackCoolDown -= Time.deltaTime;
        if((transform.position - player.transform.position).magnitude < attackRange && attackCoolDown <= 0)
        {
            attackCoolDown = 1f / attacksPerSecond;
            Attack(player);
        }
    }

    void Attack(GameObject target)
    {
        if(target.GetComponent<Playerstats>() != null)
        {
            if(attackType.Equals(AttackType.MELEE))target.GetComponent<Playerstats>().TakeDamage(attackDamage);
            if (attackType.Equals(AttackType.PROJECTILE))
            {
                GameObject projectile = Instantiate(attackProjectile, transform.position, Quaternion.identity);
                projectile.GetComponent<ProjectileScript>().initializeProjectile(target.transform.position - transform.position, gameObject.GetComponent<Collider2D>());
            }
            if (attackType.Equals(AttackType.MISSILE))
            {
                GameObject projectile = Instantiate(attackProjectile, transform.position, Quaternion.identity);
                projectile.GetComponent<ProjectileScript>().initializeProjectile(target.transform.position - transform.position, gameObject.GetComponent<Collider2D>(), player.transform);
            }
            if (attackType.Equals(AttackType.NECROMANCERBOSS))
            {
                float rng = Random.Range(0f,1f);
                if(rng <= 0.4)
                {
                    GameObject projectileOne = Instantiate(attackProjectile, transform.position, Quaternion.identity);
                    projectileOne.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(target.transform.position - transform.position, 30), gameObject.GetComponent<Collider2D>(), player.transform);
                }
                else if(rng <= 0.7f)
                {
                    GameObject projectileOne = Instantiate(attackProjectile2, transform.position, Quaternion.identity);
                    projectileOne.GetComponent<ProjectileScript>().initializeProjectile(target.transform.position - transform.position, gameObject.GetComponent<Collider2D>());

                    GameObject projectileTwo = Instantiate(attackProjectile2, transform.position, Quaternion.identity);
                    projectileTwo.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(target.transform.position - transform.position, 25), gameObject.GetComponent<Collider2D>());

                    GameObject projectileThree = Instantiate(attackProjectile2, transform.position, Quaternion.identity);
                    projectileThree.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(target.transform.position - transform.position, -25), gameObject.GetComponent<Collider2D>());

                    GameObject projectileFour = Instantiate(attackProjectile2, transform.position, Quaternion.identity);
                    projectileFour.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(target.transform.position - transform.position, -50), gameObject.GetComponent<Collider2D>());

                    GameObject projectileFive = Instantiate(attackProjectile2, transform.position, Quaternion.identity);
                    projectileFive.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(target.transform.position - transform.position, 50), gameObject.GetComponent<Collider2D>());
                }
                else if(rng <= 0.85f)
                {
                    Vector2[] roomSpots = new Vector2[2];
                    if(phase2) roomSpots = new Vector2[3];
                    if (GetComponent<Necromancermovement>() != null) {
                        for (int i = 0; i < roomSpots.Length; i++)
                        {
                            roomSpots[i] = GetComponent<Necromancermovement>().getNewSpotInRoom();

                            GameObject projectileOne = Instantiate(attackProjectile3, roomSpots[i], Quaternion.identity);
                            projectileOne.GetComponent<ProjectileScript>().initializeProjectile(Vector2.up, gameObject.GetComponent<Collider2D>());

                            GameObject projectileTwo = Instantiate(attackProjectile3, roomSpots[i], Quaternion.identity);
                            projectileTwo.GetComponent<ProjectileScript>().initializeProjectile(Vector2.down, gameObject.GetComponent<Collider2D>());

                            GameObject projectileThree = Instantiate(attackProjectile3, roomSpots[i], Quaternion.identity);
                            projectileThree.GetComponent<ProjectileScript>().initializeProjectile(Vector2.left, gameObject.GetComponent<Collider2D>());

                            GameObject projectileFour = Instantiate(attackProjectile3, roomSpots[i], Quaternion.identity);
                            projectileFour.GetComponent<ProjectileScript>().initializeProjectile(Vector2.right, gameObject.GetComponent<Collider2D>());
                        }
                    }
                }
                else
                {
                    if (GetComponent<Necromancermovement>() != null) Instantiate(firePool, GetComponent<Necromancermovement>().getNewSpotInRoom(), Quaternion.identity);
                    if (phase2)
                    {
                        GameObject projectileOne = Instantiate(attackProjectile3, transform.position, Quaternion.identity);
                        projectileOne.GetComponent<ProjectileScript>().initializeProjectile(target.transform.position - transform.position, gameObject.GetComponent<Collider2D>());

                        GameObject projectileTwo = Instantiate(attackProjectile3, transform.position, Quaternion.identity);
                        projectileTwo.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(target.transform.position - transform.position, 32), gameObject.GetComponent<Collider2D>());

                        GameObject projectileThree = Instantiate(attackProjectile3, transform.position, Quaternion.identity);
                        projectileThree.GetComponent<ProjectileScript>().initializeProjectile(RotateVector(target.transform.position - transform.position, -32), gameObject.GetComponent<Collider2D>());
                    }
                    else
                    {
                        GameObject projectileOne = Instantiate(attackProjectile3, transform.position, Quaternion.identity);
                        projectileOne.GetComponent<ProjectileScript>().initializeProjectile(target.transform.position - transform.position, gameObject.GetComponent<Collider2D>());
                    }
                }

                //if below half hp, enter phase 2
                if (GetComponent<Enemystats>() != null) if (GetComponent<Enemystats>().getCurrentHp() <= GetComponent<Enemystats>().maxHp / 2 && !phase2) {
                    attacksPerSecond *= 1.5f;
                    phase2 = true;
                }

            }
        }
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

    public enum AttackType
    {
        MELEE, PROJECTILE, MISSILE, NECROMANCERBOSS
    }
}
