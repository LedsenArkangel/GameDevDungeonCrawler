using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 4f;
    public Vector2 moveDirection = new Vector2(0f,1f);
    public float damage = 10f;

    public bool rotating = false;
    public float rotateSpeed = 10f;
    
    public void initializeProjectile(Vector2 direction, Collider2D user)
    {
        this.moveDirection = direction;
        if(user != null)Physics2D.IgnoreCollision(GetComponent<Collider2D>(),user);
    }

    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + moveDirection.normalized.x * Time.deltaTime * speed, gameObject.transform.position.y + moveDirection.normalized.y * Time.deltaTime * speed);
        if (rotating) transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        //hit player
        if (collision.gameObject.GetComponent<Playerstats>() != null)
        {

        }
        //hit enemy
        else if (collision.gameObject.GetComponent<Enemystats>() != null)
        {
            collision.gameObject.GetComponent<Enemystats>().TakeDamage(damage);
            Destroy(gameObject);
        }
        //hit wall or other collider
        else
        {
            Destroy(gameObject);
        }
    }
}
