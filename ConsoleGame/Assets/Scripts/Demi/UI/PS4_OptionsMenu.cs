using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_OptionsMenu : MonoBehaviour
{
    public GameObject resolutions;
    public GameObject fullScreen;

    // Update is called once per frame
    void Update()
    {
#if UNITY_PS4
        if(!resolutions.activeInHierarchy && !fullScreen.activeInHierarchy) { return; }

        resolutions.SetActive(false);
        fullScreen.SetActive(false);
#endif
    }
}
