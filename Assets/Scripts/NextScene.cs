using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kino;

public class NextScene : MonoBehaviour
{
    public GameObject gameManager;
    public Animator animator;
    public GameObject canvas;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameManager.Instance.UI_blackCanvas;
        animator = canvas.transform.Find("Black").GetComponent<Animator>();
        isActive = false;
    }
    public void Use()
    {
        if(isActive == true)
            return;
        isActive = true;
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
    

    IEnumerator LoadScene(int index)
    {
        SetGlitch(0.2f);
        yield return new WaitForSeconds(1.5f);
        SetGlitch(0.5f);
        animator.SetBool("fadeIn", true);
        Fungus.Flowchart.BroadcastFungusMessage("cut");
        animator.SetBool("fadeOut", false);
        yield return new WaitForSeconds(1.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadedScene;

    }

    private void OnLoadedScene(AsyncOperation obj)
    {
        animator.SetBool("fadeIn", false);
        animator.SetBool("fadeOut", true);

        GameManager.Instance.ruinList.Clear();
        GameManager.Instance.RefreshStatusUI();
    }

    private void SetGlitch(float intensity) {
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<DigitalGlitch>().intensity = 0.2f * intensity;
        camera.GetComponent<AnalogGlitch>().scanLineJitter = 0.5f * intensity;
        camera.GetComponent<AnalogGlitch>().verticalJump = 0.5f * intensity;
        camera.GetComponent<AnalogGlitch>().horizontalShake = 0.5f * intensity;
        camera.GetComponent<AnalogGlitch>().colorDrift = 0.5f * intensity;
    }

}
