using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstNPC : MonoBehaviour
{
    // Start is called before the first frame update
    private bool first_time = true;
    void Awake() {
        gameObject.GetComponent<PlayerController>().canOperate = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            if (gameObject.GetComponent<PlayerController>().canOperate == true)
                gameObject.GetComponent<PlayerController>().canOperate = false;
            else
                gameObject.GetComponent<PlayerController>().canOperate = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (first_time) {
            Fungus.Flowchart.BroadcastFungusMessage("first NPC");
            first_time = false;
        }
    }
}
