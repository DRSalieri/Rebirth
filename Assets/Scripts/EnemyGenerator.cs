using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public float geneTime;
    public Transform GeneratePos;
    public Transform left;
    public Transform right;
    public GameObject enemyPrefab;
    public int maxNum;
    public bool isActive;
    public bool isDied;
    private float remainTime;
    private List<EnemyMove> enemyList;


    private void Start()
    {
        isActive = false;
        isDied = false;
        remainTime = geneTime;
        enemyList = new List<EnemyMove>();
    }
    private void Update()
    {
        if (isActive)
        {
            remainTime -= Time.deltaTime;
            if (remainTime <= 0)
            {
                GenerateEnemy();
                remainTime = geneTime;
            }
        }
    }


    private void GenerateEnemy()
    {
        // 更新一次List，清除所有空引用
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
            }
        }

        if(enemyList.Count >= maxNum)
            return;

        EnemyMove em = Instantiate(enemyPrefab, GeneratePos.position, Quaternion.identity).GetComponent<EnemyMove>();
        enemyList.Add(em);
        em.leftPoint.position = left.position;
        em.rightPoint.position = right.position;
    }

    public void Trigger_Start()
    {
        if (isDied == true)
            return;
        if (isActive == false)
        {
            isActive = true;
            remainTime = 0;
        }
    }
    public void Trigger_End()
    {
        if (isDied == true)
            return;
        if (isActive == true)
        {
            isActive = false;
            isDied = true;
            // 清除List里所有敌人
            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                if (enemyList[i])
                {
                    enemyList[i].Died();
                }
            }

            Destroy(this.gameObject);
        }
    }

}
