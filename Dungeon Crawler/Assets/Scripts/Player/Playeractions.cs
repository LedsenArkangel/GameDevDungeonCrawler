using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playeractions : MonoBehaviour
{
    [Header("Main stats")]
    public float movementForce = 1000f;
    public float maxMovementSpeed = 5f;
    public float attackManaCost = 10f;
    public float potionAttackManaCost = 10f;

    [Header("Spells")]
    public GameObject[] spells;
    public GameObject hook;

    [Header("Spell display")]
    public Text spellNameTextDisplay;
    public Image spellIconDisplay;


    private Playerstats stats;
    private int equippedSpell = 0;

    void Start()
    {
        stats = gameObject.GetComponent<Playerstats>();
        UpdateSpellDisplay();
    }
    
    void Update()
    {
        Vector2 playerVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        //movements
        if (Input.GetKey("w"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + movementSpeed * Time.deltaTime);
            if(playerVelocity.y < maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, movementForce * Time.deltaTime));
            //playerVelocity = new Vector2(playerVelocity.x, maxMovementSpeed);
        }
        if (Input.GetKey("a"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x - movementSpeed * Time.deltaTime, gameObject.transform.position.y);
            if (playerVelocity.x > -maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(-movementForce * Time.deltaTime, 0f));
            //playerVelocity = new Vector2(-maxMovementSpeed, playerVelocity.y);
        }
        if (Input.GetKey("s"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - movementSpeed * Time.deltaTime);
            if (playerVelocity.y > -maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -movementForce * Time.deltaTime));
            //playerVelocity = new Vector2(playerVelocity.x, -maxMovementSpeed);
        }
        if (Input.GetKey("d"))
        {
            //gameObject.transform.position = new Vector2(gameObject.transform.position.x + movementSpeed * Time.deltaTime, gameObject.transform.position.y);
            if (playerVelocity.x < maxMovementSpeed) GetComponent<Rigidbody2D>().AddForce(new Vector2(movementForce * Time.deltaTime, 0f));
            //playerVelocity = new Vector2(maxMovementSpeed, playerVelocity.y);
        }

        //swap weapons
        if (Input.GetKeyDown("e"))
        {
            if (equippedSpell < spells.Length -1) equippedSpell++;
            else equippedSpell = 0;
            UpdateSpellDisplay();
        }
        if (Input.GetKeyDown("q"))
        {
            if (equippedSpell > 0) equippedSpell--;
            else equippedSpell = spells.Length - 1;
            UpdateSpellDisplay();
        }

        //left click uses equipped spell
        if (Input.GetMouseButtonDown(0))
        {
            if (spells[equippedSpell].GetComponent<SpellBase>() != null) spells[equippedSpell].GetComponent<SpellBase>().useSpell(gameObject);
        }


        if (Input.GetMouseButtonDown(1))
        {
            /*
            Vector3 positionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            positionMouse.z = transform.position.z;
            Vector3 towardsMouseFromPlayer = positionMouse - gameObject.transform.position;

            GameObject hookObject = Instantiate(hook, transform.position, Quaternion.identity);
            hook.GetComponent<HookScript>().initializeHook(new Vector2(towardsMouseFromPlayer.x, towardsMouseFromPlayer.y), gameObject);
            */
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //hit player
        if (collision.gameObject.GetComponent<DoorScript>() != null)
        {
            collision.gameObject.GetComponent<DoorScript>().TryOpenDoor();
        }
    }

    void UpdateSpellDisplay()
    {
        if (spells[equippedSpell].GetComponent<SpellBase>() != null)
        {
            spellNameTextDisplay.text = spells[equippedSpell].GetComponent<SpellBase>().spellName;
            spellIconDisplay.sprite = spells[equippedSpell].GetComponent<SpellBase>().spellIcon;
        }
    }
}
