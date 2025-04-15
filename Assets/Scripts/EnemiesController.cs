using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class EnemiesController : MonoBehaviour
{
    public List<RoundData> rounds;

    public List<GameObject> spawners;
    public List<GameObject> TopBottomSpawners;
    public List<GameObject> SideSpawners;
    public List<GameObject> CornerSpawners;
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
        SpawnerProperties spawnProps = spawner.GetComponent<SpawnerProperties>();
        float deviation = UnityEngine.Random.Range(-spawnProps.spawnerRange, spawnProps.spawnerRange);

        GameObject enemy = Instantiate(enemyPrefab, spawner);
        switch (spawnProps.spawnerType)
        {
            case SpawnerType.HORIZONTAL:
                enemy.transform.position = spawner.position + Vector3.right * deviation;
                break;
            case SpawnerType.VERTICAL:
                enemy.transform.position = spawner.position + new Vector3(0,0,1) * deviation;
                break;
            case SpawnerType.CORNER:
                enemy.transform.position = spawner.position;
                break;
            default:
                // code block
                enemy.transform.position = spawner.position;
                break;
        }
        enemy.transform.rotation = spawner.rotation;
        enemy.GetComponent<EnemyBehaviour>().enemyDirection = spawnProps.enemyDirection;
        enemy.SetActive(true);
    }
}

