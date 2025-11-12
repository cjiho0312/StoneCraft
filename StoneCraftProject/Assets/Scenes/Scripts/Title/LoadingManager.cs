using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    static public LoadingManager Instance;

    [SerializeField] public GameObject loadingCanvas;
    [SerializeField] AudioSource Bgm;
    public float minLoadingTime = 1.5f;

    private void Start()
    {
        if (Instance == null) { Instance = this; }
        loadingCanvas.SetActive(false);
    }

    public void StartLoading()
    {
        loadingCanvas.SetActive(true);
        Bgm.Stop();
        StartCoroutine(LoadLoginScene());
    }

    private IEnumerator LoadLoginScene()
    {
        float startTime = Time.time;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TestScene");
        asyncLoad.allowSceneActivation = false;

        bool readyToActivate = false;

        while (!asyncLoad.isDone)
        {
            bool timeReady = Time.time - startTime >= minLoadingTime;
            bool loadReady = asyncLoad.progress >= 0.9f;

            if (!readyToActivate && timeReady && loadReady)
            {
                readyToActivate = true;
                yield return new WaitForSeconds(0.5f);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        loadingCanvas.SetActive(false);
    }
}
