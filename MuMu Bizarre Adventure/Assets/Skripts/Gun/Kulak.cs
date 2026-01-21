using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kulak : MonoBehaviour
{
    public GameObject IsEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(IsEffect, transform.position, Quaternion.identity);
        }
    }
}
