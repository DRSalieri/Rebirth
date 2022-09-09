using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public GameObject enemyObject;
    public void AfterDied()
    {
        Destroy(enemyObject);
    }
}
