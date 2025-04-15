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

    public Quaternion enemyRotation;

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
            Debug.Log("New spawn round " + PlayerControls.Instance.currentRound);
            List<GameObject> possibleEnemies = new List<GameObject>();
            int roundIndex = PlayerControls.Instance.currentRound;
            RoundData round;
            if (rounds.Count - 1 > roundIndex)
            {
                round = rounds[roundIndex];
                possibleEnemies = round.possibleEnemies;
            }
            else
            {
                round = rounds[rounds.Count - 1];
                possibleEnemies = round.possibleEnemies;
            }
           
            float numberOfEnemies = UnityEngine.Random.Range(round.minEnemiesPerSpawn, round.maxEnemiesPerSpawn);
            while (numberOfEnemies > 0)
            {
                SpawnEnemy(possibleEnemies);
                numberOfEnemies--;
            }
            yield return new WaitForSeconds(round.timeBetweenSpawns);
        }
    }

    public void SpawnEnemy(List<GameObject> possibleEnemies)//añadir como parámetro de entrada posibles enemigos
    {
        int chosenEnemyID = UnityEngine.Random.Range(0, possibleEnemies.Count);
        EnemyBehaviour enemyBehaviour = possibleEnemies[chosenEnemyID].GetComponent<EnemyBehaviour>();
        Transform spawner;

        GameObject enemy = Instantiate(enemyBehaviour.transform.gameObject);
        int chosenSpawnerID = 0;
        float deviation = 0;
        SpawnerProperties spawnProps;

        if (enemyBehaviour.spawnerType == SpawnerType.HORIZONTAL)
        {
            chosenSpawnerID = UnityEngine.Random.Range(0, TopBottomSpawners.Count);
            spawner = TopBottomSpawners[chosenSpawnerID].transform;
            spawnProps = spawner.GetComponent<SpawnerProperties>();
            deviation = UnityEngine.Random.Range(-spawnProps.spawnerRange, spawnProps.spawnerRange);
            enemy.transform.position = spawner.position + Vector3.right * deviation;

        } else if (enemyBehaviour.spawnerType == SpawnerType.VERTICAL) {

            chosenSpawnerID = UnityEngine.Random.Range(0, SideSpawners.Count);
            spawner = SideSpawners[chosenSpawnerID].transform;
            spawnProps = spawner.GetComponent<SpawnerProperties>();
            deviation = UnityEngine.Random.Range(-spawnProps.spawnerRange, spawnProps.spawnerRange);
            enemy.transform.position = spawner.position + new Vector3(0, 0, 1) * deviation;

        }
        else
        {
            chosenSpawnerID = UnityEngine.Random.Range(0, CornerSpawners.Count);
            spawner = CornerSpawners[chosenSpawnerID].transform;
            spawnProps = spawner.GetComponent<SpawnerProperties>();
            enemy.transform.position = spawner.position;

        }
        enemy.transform.rotation = spawner.rotation;
        enemy.GetComponent<EnemyBehaviour>().enemyDirection = spawnProps.enemyDirection;
        enemy.transform.rotation = spawner.rotation;
        enemy.transform.parent = spawner;
        enemy.SetActive(true);
    }
}

