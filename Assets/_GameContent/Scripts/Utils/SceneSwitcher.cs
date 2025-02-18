using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private UnityEvent startEvent;
    [SerializeField] private Image loadingBar;
    [SerializeField] private float minAsyncLoadTime = 2f;
    [SerializeField] private GameObject loadingFX;
    [SerializeField] private float fxTime;
    private void Start()
    {
        startEvent?.Invoke();
    }
    public void LoadScene(int index)
    {
        if (SceneSwitchFX())
        {
            StartCoroutine(DelayedLoad(index));
        }
        else
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(index);
        }
    }
    public void ReloadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (SceneSwitchFX())
        {
            StartCoroutine(DelayedLoad(index));
        }
        else
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(index);
        }
    }
    public void NextSceneLoad()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneSwitchFX())
        {
            StartCoroutine(DelayedLoad(index));
        }
        else
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(index);
        }
    }
    private IEnumerator DelayedLoad(int index)
    {
        yield return new WaitForSeconds(fxTime);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(index);
    }
    public int SceneIdnex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }


    public void LoadAsync(int index)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneAsyncCoroutine(index));
    }

    private AsyncOperation asyncLoad;
    private IEnumerator LoadSceneAsyncCoroutine(int index)
    {

        asyncLoad = SceneManager.LoadSceneAsync(index);
        asyncLoad.allowSceneActivation = false;
        StartCoroutine(LoadingAnimation());
        yield return new WaitForSeconds(minAsyncLoadTime);
        asyncLoad.allowSceneActivation = true;

    }
    private IEnumerator LoadingAnimation()
    {
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingBar.fillAmount = progress;
            yield return null;
        }
    }
    public void LoadNextSceneAddictive(string nextSceneName)
    {
        //bugs ----------------
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentScene);
    }
    private bool SceneSwitchFX()
    {
        if (loadingFX != null)
        {
            GameObject loadingFXGO = Instantiate(loadingFX);
            DontDestroyOnLoad(loadingFXGO);
            return true;
        }
        return false;
    }
}
