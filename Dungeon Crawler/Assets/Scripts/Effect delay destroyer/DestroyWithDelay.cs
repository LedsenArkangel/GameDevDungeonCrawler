using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    public float deathTimer = 5f;
    
    void Start()
    {
        Destroy(gameObject, deathTimer);
    }
}
