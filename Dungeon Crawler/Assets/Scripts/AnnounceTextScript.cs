using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnounceTextScript : MonoBehaviour
{
    public float delay = 5f;
    public float fadespeed = 0.2f;

    private float delaytimer = 5f;

    // start fading text after delay
    void Update()
    {
        delaytimer -= Time.deltaTime;
        if (delaytimer <= 0)
        {
            if (GetComponent<Text>().color.a > 0)
            {
                GetComponent<Text>().color = new Color(1, 0f, 0f, GetComponent<Text>().color.a - Time.deltaTime*fadespeed);
            }
        }
    }

    public void announce(string text)
    {
        delaytimer = delay;
    }
}
