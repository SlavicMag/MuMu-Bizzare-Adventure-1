using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    private bool inventoryOn;

    public object items { get; internal set; }

    private void Start()
    {
        inventoryOn = true;
    }
    public void Chest()
    {
        if (inventoryOn == false)
        {
            inventoryOn = true;
            inventory.SetActive(true);
        }

        else if (inventoryOn == true) 
        {
            inventoryOn = false;
            inventory.SetActive(false);
        }
}    }


