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

    [Header("Visuals")]
    public GameObject attackEffect;

    private float attackCoolDown = 0f;
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        GetComponent<AIDestinationSetter>().target = Player.transform;
    }
    
    void Update()
    {
        if (attackCoolDown >= 0) attackCoolDown -= Time.deltaTime;
        if((transform.position - Player.transform.position).magnitude < attackRange && attackCoolDown <= 0)
        {
            attackCoolDown = attacksPerSecond;
            Attack(Player);
        }
    }

    void Attack(GameObject target)
    {
        if(target.GetComponent<Playerstats>() != null)
        {
            target.GetComponent<Playerstats>().TakeDamage(attackDamage);
        }
    }
}
