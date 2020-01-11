using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 4f;
    public Vector2 direction = new Vector2(0f,1f);
    public bool ignorePlayer = false;
    public float damage = 10f;

    public bool rotating = false;
    public float rotateSpeed = 10f;

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        gameObject.transform.Translate(direction.normalized * Time.deltaTime * speed);
        if (rotating) transform.eulerAngles = Vector3.forward * rotateSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ignorePlayer)
        {

        }
    }
}
