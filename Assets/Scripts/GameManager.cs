using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    private List<GameObject> platformList;      // 保存所有被创造的平台
    public List<GameObject> ruinList;          // 保存所有被创造的废墟
    [SerializeField] private int maxRuin;       // 最大废墟数
    public Transform spawnPos;                  // 重生点
    public ComputerStation nowSavePoint;        // 现在的存档点
    public CinemachineVirtualCamera cine;
    public GameObject UI_blackCanvas;                  // 场景过渡UI
    public Animator UI_blackAnim;
    public GameObject UI_Info;
    public UI_StatusImg UI_StatusImg;                   // 形态UI
    public TextMeshProUGUI UI_StatusText;                    // 文字UI

    public GameObject barragePanel;


    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject platformPrefab;

    [SerializeField] private Sprite savePointOff;
    [SerializeField] private Sprite savePointOn;


    // Start is called before the first frame update
    private void Init()
    {
        DontDestroyOnLoad(this.UI_blackCanvas);
        DontDestroyOnLoad(this.UI_Info);
        DontDestroyOnLoad(this);
        platformList = new List<GameObject>(4);
        ruinList = new List<GameObject>(12);
    }
    void Start()
    {
        Init();
        RefreshStatusUI();

    }

    void Update()
    {
        // trigger barrage test
        if(Input.GetKeyDown(KeyCode.B)) {
            List<string> x = new List<string>();
            x.Add("11111111111");
            x.Add("22222222222222");
            x.Add("3333333333333333");
            x.Add("44444444444");
            x.Add("5555555");
            x.Add("6666666");
            x.Add("77777");
            x.Add("88888");
            x.Add("999999999999999");
            x.Add("1010101010");
            barragePanel.GetComponent<Barrage>().TriggerBarrage(x);
        }
        
    }

    /***
    *
    *   公共接口
    *
    ***/



    // 在指定位置创建一个platform
    public void CreatePlatform(Vector3 pos)
    {
        GameObject _platform = Instantiate(platformPrefab, pos, Quaternion.identity);

        if (platformList.Count >= 3)
        {
            Destroy(platformList[0]);
            platformList.RemoveAt(0);
        }

        platformList.Add(_platform);
    }

    // 在指定位置创建一个Ruin
    /*
    public void CreateRuin(Vector3 pos)
    {
        GameObject _ruin = Instantiate(ruinPrefab, pos, Quaternion.identity);

        if (ruinList.Count >= maxRuin)
        {
            Destroy(ruinList[0]);
            ruinList.RemoveAt(0);
        }

        ruinList.Add(_ruin);
    }
    */
    // 删除指定的Ruin
    /*
    public void DeleteRuin(Ruin _r)
    {
        for (int i = 0; i < ruinList.Count; i++)
        {
            if (ruinList[i] == _r)
            {
                ruinList.RemoveAt(i);
                break;
            }
        }
        Destroy(_r.gameObject);
    }
    */

    public void OnRuinCreate(GameObject _r)
    {
        // 遍历ruinList，消除空引用
        for(int i = ruinList.Count - 1; i >= 0; i--)
        {
            if(ruinList[i] == null)
                ruinList.RemoveAt(i);
        }

        ruinList.Add(_r);

        if(ruinList.Count > maxRuin)
        {
            Destroy(ruinList[0]);
            ruinList.RemoveAt(0);
        }

        // 更新UI
        RefreshStatusUI();
    }
    public void RefreshStatusUI()
    {
        UI_StatusText.text = "Created Bodies  " + ruinList.Count.ToString() + "/" + maxRuin.ToString();
    }
    public void FadeInDark()
    {
        UI_blackAnim.SetBool("fadeIn", true);
        UI_blackAnim.SetBool("fadeOut", false);
    }

    public void FadeOutDark()
    {
        UI_blackAnim.SetBool("fadeIn", false);
        UI_blackAnim.SetBool("fadeOut", true);
    }
    // 重生
    public void Rebirth()
    {
        // Destroy(player);

        GameObject new_player = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity);
        if(nowSavePoint != null)
        {
            // 有存档点时，切换形态
            // new_player.GetComponent<PlayerController>().shape = nowSavePoint.shape;
        }
        cine.Follow = new_player.transform;
    }

    // 切换当前存档点
    public void SwitchSavePoint(ComputerStation cs)
    {
        if (nowSavePoint != null)
        {
            nowSavePoint.GetComponent<SpriteRenderer>().sprite = savePointOff;
        }
        cs.GetComponent<SpriteRenderer>().sprite = savePointOn;
        nowSavePoint = cs;
        spawnPos = cs.spawnPos;
    }

}



