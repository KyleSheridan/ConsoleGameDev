using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody rigidbody { get; private set; }
    public InputData input { get; private set; }

    // all inputsources that can control the player
    IInput[] allInputs;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        allInputs = GetComponents<IInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetInputs()
    {
        input = new InputData();

        for (int i = 0; i < allInputs.Length; i++)
        {
            input = allInputs[i].GenerateInput();
        }
    }
}
