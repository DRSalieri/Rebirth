using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStatus : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI tmp;
    public string statusValue;
    public string bodiesValue;
    void Start()
    {
        statusValue = "1";
        bodiesValue = "5";
    }

    void Update()
    {
        tmp.text = "status: " + statusValue + "\nbodies: " + bodiesValue;
    }
}
