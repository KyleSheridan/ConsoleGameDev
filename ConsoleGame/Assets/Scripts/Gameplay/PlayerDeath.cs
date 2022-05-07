using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathScreen;

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth.isAlive) { return; }

        deathScreen.SetActive(true);

        if (InputManager.Instance.input.Jump)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
