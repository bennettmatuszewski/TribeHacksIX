using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItem : MonoBehaviour
{
    public int cost;
    public int health;
    public Sprite oneHitLeftSprite;
    public Sprite twoHitLeftSprite;
    private SpriteRenderer sr;
    public int desiredMass;
    private bool canTakeDamage;
    private void Start()
    {
        canTakeDamage = true;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Urchin") && canTakeDamage)
        {
            health--;
            canTakeDamage = false;
            StartCoroutine(CanTakeDamage());
            if (health<= 0)
            {
                Destroy(gameObject);
            }

            if (health == 1)
            {
                sr.sprite = oneHitLeftSprite;
            } 
            else if (health == 2)
            {
                sr.sprite = twoHitLeftSprite;
            }
        }
    }

    IEnumerator CanTakeDamage()
    {
        yield return new WaitForSeconds(2);
        canTakeDamage = true;
    }
}
