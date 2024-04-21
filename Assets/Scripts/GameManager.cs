using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject seaUrchin;
    public Transform shootPos;
    public Shop shop;
    public GameObject shopButton;
    public GameObject playButton;
    public bool gameIsPlaying;
    public int numberOfUrchins;
    public bool gameLost;
    public TMP_Text winLoseText;
    public LevelLoader ll;
    public TMP_Text urchinText;
    public Transform shootPos2;

    private void Start()
    {
        urchinText.text = numberOfUrchins.ToString();
    }

    public void StartGame()
    {
        if (shop.isPlacingItem || shop.shopOpen || gameIsPlaying)
        {
            return;            
        }

        foreach (var c in GameObject.FindGameObjectsWithTag("Crab"))
        {
            c.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        gameIsPlaying = true;
        shopButton.SetActive(false);
        playButton.SetActive(false);
        StartCoroutine(ShootUrchins());
    }

    IEnumerator ShootUrchins()
    {
        shootPos.GetComponent<ParticleSystem>().Stop();
        for (int i = 0; i < numberOfUrchins; i++)
        {
            yield return new WaitForSeconds(1);
            urchinText.text = (numberOfUrchins-i-1).ToString();
            if (!gameLost)
            {
                if (shootPos2 !=null)
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        Instantiate(seaUrchin, shootPos.position, transform.rotation);
                    }
                    else
                    {
                        Instantiate(seaUrchin, shootPos2.position, transform.rotation);   
                    }
                }
                else
                {
                    Instantiate(seaUrchin, shootPos.position, transform.rotation);   
                }
                yield return new WaitForSeconds(3);   
            }
        }

        yield return new WaitForSeconds(1);
        if (gameLost)
        {
            winLoseText.text = "R.I.P Clawdius";
            winLoseText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(1);
            ll.Restart();
        }
        else
        {
            yield return new WaitForSeconds(3);
            if (gameLost)
            {
                winLoseText.text = "R.I.P Clawdius";
                winLoseText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InOutQuad);
                yield return new WaitForSeconds(1);
                ll.Restart();
            }
            else
            {
                winLoseText.text = "You Win!";
                winLoseText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InOutQuad);
                yield return new WaitForSeconds(1);
                ll.LoadNextLevel();   
            }
        }
    }
}
