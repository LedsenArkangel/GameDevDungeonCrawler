using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnounceTextScript : MonoBehaviour
{
    public float delay = 5f;
    public float fadespeed = 0.2f;

    private float delaytimer = 5f;

    void Start()
    {
        Invoke("tutorialAnnounce1", 5f);
        Invoke("tutorialAnnounce2", 10f);
        Invoke("tutorialAnnounce3", 15f);
        Invoke("tutorialAnnounce4", 20f);
    }


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

    public void announce(string text, float fadeDelay, Color color)
    {
        delaytimer = fadeDelay;
        GetComponent<Text>().text = text;
        GetComponent<Text>().color = color;
    }

    public void tutorialAnnounce1()
    {
        announce("use WASD to move", 5f, new Color(0.2755886f, 0.02790139f, 0.5377358f, 1f));
    }

    public void tutorialAnnounce2()
    {
        announce("mouse to aim and cast a spell", 5f, new Color(0.2755886f, 0.02790139f, 0.5377358f, 1f));
    }

    public void tutorialAnnounce3()
    {
        announce("use Q and E to switch spells", 5f, new Color(0.2755886f, 0.02790139f, 0.5377358f, 1f));
    }

    public void tutorialAnnounce4()
    {
        announce("Good luck, castermind", 5f, new Color(0.2755886f, 0.02790139f, 0.5377358f, 1f)); 
    }
}
