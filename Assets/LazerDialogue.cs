using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerDialogue : MonoBehaviour
{
    private bool first_time_1 = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (first_time_1 && Input.GetButtonDown("Interact")) {
            Fungus.Flowchart.BroadcastFungusMessage("lazer trigger");
            first_time_1 = false;
        }
    }
}
