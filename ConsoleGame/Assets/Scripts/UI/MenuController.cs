using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{

    public GameObject pauseMenu;

    public GameObject optionsMenu;

    public GameObject checkManager;

    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton, creditsFirstButton, creditsClosedButton;

    public GameObject playerUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMenu != null)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Options"))
            {
                if (!optionsMenu.activeInHierarchy)
                {
                    PauseUnpause();
                }
            }
        }
    }

    public void PauseUnpause()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            playerUI.SetActive(false);
            Time.timeScale = 0f;
            //clear selected object 
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        else
        {
            pauseMenu.SetActive(false);
            playerUI.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void OpenOptions()
    {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions()
    {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }
    public void OpenCredits()
    {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
    }

    public void CloseCredits()
    {
        //clear selected object 
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(creditsClosedButton);
    }

    public void RestartFromCheckpoint()
    {
        Destroy(checkManager);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
