using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObjectDelete : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyWait(3f));
    }

    IEnumerator DestroyWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
