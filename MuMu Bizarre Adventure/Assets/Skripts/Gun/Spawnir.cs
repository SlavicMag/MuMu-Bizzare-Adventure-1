using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnir : MonoBehaviour
{
    public bool SpawnStart;
    public GameObject[] enemyPrefab;
    public float StartSpawnTime;
    public float spawnTime;
    public int spawnCount;
    public int cout;

    private void Start()
    {
        if (SpawnStart)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        cout++;
        spawnTime = StartSpawnTime;
        int rand = Random.Range(0, enemyPrefab.Length);

        GameObject enemy = Instantiate(enemyPrefab[rand], transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (cout < spawnCount)
        {
            if (spawnTime <= 0)
            {
                SpawnEnemy();
            }
            else
            {
                spawnTime -= Time.deltaTime;
            }
        }
        else if (cout >= spawnCount)
        {
            gameObject.SetActive(false);
        }
    }
}
