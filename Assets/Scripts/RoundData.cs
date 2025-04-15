using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RoundData", order = 1)]

public class RoundData : ScriptableObject
{
    public List<GameObject> possibleEnemies;
    public float enemySpeedBoost;
    public float timeBetweenSpawns;
    public int minEnemiesPerSpawn;
    public int maxEnemiesPerSpawn;
}
