using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }

    public InputData input { get; private set; }

    public
    // all inputsources that can control the player
    IInput[] allInputs;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

#if UNITY_PS4
            allInputs = GetComponents<ConsoleInput>();
#else
            allInputs = GetComponents<PlayerInput>();
#endif

        }
    }

    private void Update()
    {
#if !UNITY_PS4
        CinemachineCore.GetInputAxis = GetAxisCustom;
#endif
    }

    private void LateUpdate()
    {
        GetInputs();
    }

    public float GetAxisCustom(string axisName)
    {
        float camX = Input.GetAxisRaw("CameraX");
        float camY = Input.GetAxisRaw("CameraY");

        if (axisName == "C_CameraX")
        {
            Debug.Log(camX);
            return camX;
        }
        else if (axisName == "C_CameraY")
        {
            return camY;
        }
        return 0;
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
        if (this == _instance)
        {
            _instance = null;
        }
    }
}
