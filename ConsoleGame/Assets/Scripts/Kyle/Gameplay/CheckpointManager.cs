using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager _instance;

    public static CheckpointManager Instance { get { return _instance; } }

    public Vector3 spawnPoint;

    public string currentLevel = "";

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            if(SceneManager.GetActiveScene().name == _instance.currentLevel)
            {
                Debug.Log("help");
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("help prt2");
                Destroy(_instance.gameObject);

                _instance = this;
                DontDestroyOnLoad(this.gameObject);
                currentLevel = SceneManager.GetActiveScene().name;
            }
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            currentLevel = SceneManager.GetActiveScene().name;
        }
    }
}
