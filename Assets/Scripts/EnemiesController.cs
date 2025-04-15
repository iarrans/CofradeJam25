using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public List<GameObject> spawners;
    public GameObject enemyPrefab;

    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public float minDegreeDeviation;
    public float maxDegreeDeviation;
    public float maxUnitsDeviation;
    public float minEnemies;
    public float maxEnemies;

    public static EnemiesController Instance;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawner()
    {
        StartCoroutine(EnemySpawner());
    }

    public IEnumerator EnemySpawner()
    {
        while (PlayerControls.Instance.isPlaying)
        {
            float newWaitingTime = UnityEngine.Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
           
            float numberOfEnemies = UnityEngine.Random.Range(minEnemies, maxEnemies);
            while (numberOfEnemies > 0)
            {
                SpawnEnemy();
                numberOfEnemies--;
            }
            yield return new WaitForSeconds(newWaitingTime);
        }
    }

    public void SpawnEnemy()
    {
        int chosenSpawnerID = UnityEngine.Random.Range(0, spawners.Count);
        Transform spawner = spawners[chosenSpawnerID].transform;
        GameObject enemy = Instantiate(enemyPrefab, spawner);
        enemy.transform.position = spawner.position;
        enemy.transform.rotation = spawner.rotation;
        enemy.GetComponent<EnemyBehaviour>().enemyDirection = spawner.GetComponent<SpawnerProperties>().enemyDirection;
        enemy.SetActive(true);
    }
}

