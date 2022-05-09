using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_MainMenu : MonoBehaviour
{
    public GameObject exit;

    // Update is called once per frame
    void Update()
    {
#if UNITY_PS4
        if (!exit.activeInHierarchy) { return; }

        exit.SetActive(false);
#endif
    }
}
