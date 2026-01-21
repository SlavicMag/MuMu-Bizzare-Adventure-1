using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public enum ActivatorType {zonaDamage, activeAnim}
    public ActivatorType activeType;
    private PlayerKontroller player;

    private void Start()
    {
        player = FindObjectOfType<PlayerKontroller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activeType == ActivatorType.zonaDamage)
        {
            if(collision.CompareTag("Player"))
            {
                player.PlayerTakeDamage(50);
            }
        }
    }
}
