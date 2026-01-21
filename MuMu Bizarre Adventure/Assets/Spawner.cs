using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] variants;

    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float daecreaseTime;
    public float minTime;
 
    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, variants.Length);
            Instantiate(variants[rand], transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
            
            if(startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= daecreaseTime;
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
