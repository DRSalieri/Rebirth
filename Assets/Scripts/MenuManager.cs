using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform spawnPos;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject new_player;

    private bool _isRun = false;


    void Start()
    {
        new_player = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity);
    }

    void Update()
    {  
        if (_isRun)
            new_player.transform.position += new Vector3((float)0.03, 0, 0);
    }

    public void Run() {
        Transform sprite = new_player.gameObject.transform.GetChild(0);
        Animator anim = sprite.GetComponent<Animator>();
        _isRun = true;
        anim.SetBool("IsRun", true);
    }

}



