using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject itemPlacing;
    public bool isPlacingItem;
    private bool placingItem;
    private Camera mainCam;
    public int coins;
    public TMP_Text coinsText;
    public bool shopOpen;

    public GameObject playButton;
    public GameObject shopButton;
    private void Start()
    {
        mainCam = Camera.main;
        coinsText.text = coins.ToString();
    }

    public void PurchaseItem(GameObject item)
    {
        int cost = item.GetComponent<PlaceableItem>().cost;
        if (isPlacingItem || coins< cost)
        {
            return;
        }

        coins -= cost;
        coinsText.text = coins.ToString();
        shopPanel.SetActive(false);
        shopButton.SetActive(false);
        shopOpen = false;
        itemPlacing = Instantiate(item, mainCam.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
        placingItem = true;
        
    }

    public void ShopOpenClose()
    {
        if (shopOpen)
        {
            shopPanel.SetActive(false);
            playButton.SetActive(true);
            shopOpen = false;
        }
        else
        {
            shopPanel.SetActive(true);
            playButton.SetActive(false);
            shopOpen = true;
        }
    }
    

    private void Update()
    {
        if (placingItem)
        {
            itemPlacing.transform.position = new Vector3(mainCam.ScreenToWorldPoint(Input.mousePosition).x, mainCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
            itemPlacing.transform.Rotate(0, 0, -Input.GetAxis("Horizontal")*100*Time.deltaTime);
        }
        

        if (placingItem && Input.GetMouseButton(0))
        {
            placingItem = false;
            itemPlacing.GetComponent<BoxCollider2D>().enabled = true;
            itemPlacing.AddComponent<Rigidbody2D>();
            itemPlacing.GetComponent<Rigidbody2D>().mass = itemPlacing.GetComponent<PlaceableItem>().desiredMass;
            playButton.SetActive(true);
            shopButton.SetActive(true);
        }
    }
}
