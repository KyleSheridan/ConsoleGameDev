using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth.isAlive) { return; }

        deathScreen.SetActive(true);

        if (InputManager.Instance.input.Jump)
        {
            Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
