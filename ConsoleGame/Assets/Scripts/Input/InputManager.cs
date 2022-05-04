using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }

    public InputData input { get; private set; }

    // all inputsources that can control the player
    IInput[] allInputs;

    private void Awake()
    {
        if(_instance !=null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            
            allInputs = GetComponents<IInput>();
        }
    }

    private void LateUpdate()
    {
        GetInputs();
    }

    void GetInputs()
    {
        input = new InputData();

        for (int i = 0; i < allInputs.Length; i++)
        {
            input = allInputs[i].GenerateInput();
        }
    }

    private void OnDestroy()
    {
        if(this == _instance)
        {
            _instance = null;
        }
    }
}
