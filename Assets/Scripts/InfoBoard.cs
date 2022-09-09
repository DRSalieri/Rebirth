using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class InfoBoard : MonoBehaviour
{
    public bool onlyOncePassiveBlock;
    public bool havePassiveBlock;
    public string passiveBlock;
    public bool havePositiveBlock;
    public string positiveBlock;
    public Flowchart flowChart;


    public void ExecutePositiveBlock()
    {
        if(havePositiveBlock)
            flowChart.ExecuteBlock(positiveBlock);
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        if (havePassiveBlock && other.gameObject.tag == "Player")
        {
            flowChart.ExecuteBlock(passiveBlock);
            if(onlyOncePassiveBlock == true)
                havePassiveBlock = false;
        }
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (havePassiveBlock)
                flowChart.StopBlock(passiveBlock);

            if (havePositiveBlock)
                flowChart.StopBlock(positiveBlock);
        }
    }

    
}
