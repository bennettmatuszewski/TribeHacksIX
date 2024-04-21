using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Urchin : MonoBehaviour
{
   private GameManager gameManager;
   private SpriteRenderer sr;
   private Rigidbody2D rb;
   private GameObject clawdius;
   private IEnumerator Start()
   {
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
      if (GameObject.FindGameObjectsWithTag("Crab").Length > 1)
      {
         clawdius = GameObject.FindGameObjectsWithTag("Crab")[Random.Range(0, 2)];  
      }
      else
      {
         clawdius = GameObject.FindGameObjectWithTag("Crab");
      }
      gameManager = FindObjectOfType<GameManager>();
      sr.DOFade(1, 0.5f);
      yield return new WaitForSeconds(1);
      Move();
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Crab"))
      {
         //game over
         FindObjectOfType<GameManager>().gameLost = true;
         other.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
         Destroy(other.gameObject, 0.5f);
      }
   }

   public void Move()
   {
      if (clawdius==null)
      {
         return;
      }
      Vector3 dir = clawdius.transform.position - transform.position;
      dir = dir.normalized;
      rb.gravityScale = 0.5f;
      dir = new Vector3(dir.x, dir.y + Random.Range(0.25f, 0.4f), dir.z);
      rb.AddForce(dir * Random.Range(500, 700));
      StartCoroutine(WaitDestroy());
   }

   IEnumerator WaitDestroy()
   {
      yield return new WaitForSeconds(3);
      sr.DOFade(0, 1);
      Destroy(gameObject,1);
   }
}
