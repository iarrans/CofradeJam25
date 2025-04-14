using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public List<GameObject> spawners;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;

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
            yield return new WaitForSeconds(newWaitingTime);
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Debug.Log("SpawnsEnemy");
    }
}
