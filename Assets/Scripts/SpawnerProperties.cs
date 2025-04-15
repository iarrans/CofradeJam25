using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerProperties : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 enemyDirection;
    public float spawnerRange;//cantidad de unidades que puede desplazarse a ambos lados ¿O marco los límites?
    public SpawnerType spawnerType;

}

public enum SpawnerType
{
    HORIZONTAL, VERTICAL, CORNER
}
