using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebirthTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool first_time;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(Input.GetButtonDown("Boom")) {
                GameManager.Instance.TriggerBarrage("first rebirth");
                first_time = false;
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (first_time || Input.GetButtonDown("Interact")) {
            Fungus.Flowchart.BroadcastFungusMessage("rebirth trigger");
            first_time = false;
        }
    }
}
