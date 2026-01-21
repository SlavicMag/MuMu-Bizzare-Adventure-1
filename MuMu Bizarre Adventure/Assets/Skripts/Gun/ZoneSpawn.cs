using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawn : MonoBehaviour
{
    public GameObject knopka;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            knopka.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            knopka.SetActive(false);
        }
    }
}
