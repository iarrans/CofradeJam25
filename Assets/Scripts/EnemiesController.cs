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
        GameObject enemy = Instantiate(enemyPrefab, spawners[chosenSpawnerID].transform);
        enemy.transform.position = spawners[chosenSpawnerID].transform.position;
        enemy.transform.rotation = spawners[chosenSpawnerID].transform.rotation;
        enemy.SetActive(true);
    }
}

