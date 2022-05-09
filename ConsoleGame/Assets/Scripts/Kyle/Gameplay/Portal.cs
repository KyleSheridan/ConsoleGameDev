using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string levelName;
    public GameObject loadingScreen;
    public Slider progressSlider;
    public Text progressText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 1f;
            StartCoroutine(LoadAsynchronously(levelName));
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

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
}
