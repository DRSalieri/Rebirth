using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneEndTrigger : MonoBehaviour
{
    public EnemyGenerator eg;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
            eg.Trigger_End();
    }
}
