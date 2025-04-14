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

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * enemySpeed);
        enemyTimer += Time.deltaTime;
        if (enemyTimer > enemyTotalLife)
        {
            enemyTimer = 0;
            gameObject.SetActive(false);
        }
    }

}
