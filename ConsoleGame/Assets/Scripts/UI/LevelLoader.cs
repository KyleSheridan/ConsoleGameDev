using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressSlider;
    public Text progressText;

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            int progressInt = Mathf.RoundToInt(progress * 100f);

            Debug.Log(progress * 100f + "% loaded");

            progressSlider.value = progress;
            progressText.text = progressInt + "%";
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
