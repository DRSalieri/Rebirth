using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public GameObject gameManager;
    public Animator animator;
    public GameObject canvas;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
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
        animator.SetBool("fadeIn", true);
        animator.SetBool("fadeOut", false);

        yield return new WaitForSeconds(1);
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

}
