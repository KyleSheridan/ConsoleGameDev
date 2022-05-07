using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathScreen;

    public GameObject playerUI;

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth.isAlive) { return; }

        Time.timeScale = 0f;
        deathScreen.SetActive(true);
        playerUI.SetActive(false);

        if (InputManager.Instance.input.Jump)
        {
            Application.LoadLevel(Application.loadedLevel);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
