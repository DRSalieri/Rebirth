using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class MySceneManager : MonoBehaviour
{
    // 每个场景的起始重生点
    public Transform initSpawnPos;
    public CinemachineVirtualCamera cine;


    void Start()
    {
        // 消除存档点
        GameManager.Instance.nowSavePoint = null;
        // 设置起始重生点
        GameManager.Instance.spawnPos = initSpawnPos;
        // 设置虚拟相机
        GameManager.Instance.cine = cine;
        // 创建玩家
        GameManager.Instance.Rebirth();
    }

    public void FadeInDark()
    {
        GameManager.Instance.FadeInDark();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
