using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyTimer;
    public float enemyTotalLife;
    public float enemySpeed;
    public Vector3 enemyDirection;
    public SpawnerType spawnerType;

    private void Update()
    {                           
                            //enemyDirection
        transform.Translate(enemyDirection * Time.deltaTime * enemySpeed);
        enemyTimer += Time.deltaTime;
        if (enemyTimer > enemyTotalLife)
        {
            enemyTimer = 0;
            gameObject.SetActive(false);
        }
    }

}
