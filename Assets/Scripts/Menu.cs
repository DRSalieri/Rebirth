using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button start;
    public Button exit;

    void Start()
    {
        start.onClick.AddListener(StartOnClick);
        exit.onClick.AddListener(ExitOnClick);
    }

    void StartOnClick()
    {
        SceneManager.LoadScene(1);
    }
    void ExitOnClick()
    {
        Application.Quit();
    }
}
