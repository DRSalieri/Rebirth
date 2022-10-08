using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool first_time_1 = true;
    private bool first_time_2 = true;
    private bool first_time_3 = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.name == "SavePoint" && first_time_1) {
            Fungus.Flowchart.BroadcastFungusMessage("control trigger");
            first_time_1 = false;
        }
        if (gameObject.name == "Switch" && first_time_2) {
            Fungus.Flowchart.BroadcastFungusMessage("conversation_1");
            first_time_2 = false;
        }
        if (gameObject.name == "Button" && first_time_3) {
            Fungus.Flowchart.BroadcastFungusMessage("conversation_2");
            first_time_3 = false;
        }
    }
}
