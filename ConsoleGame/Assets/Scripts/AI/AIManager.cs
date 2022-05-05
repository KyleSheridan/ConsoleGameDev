using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public float aiHealth = 50f;
    public bool dead = false;

    // Update is called once per frame
    void Update()
    {
        Die();
    }
    private void OnTriggerEnter(Collider other)
    {
        //
    }

    public void Die()
    {
        if(aiHealth <= 0)
        {
            dead = true;
            //StartCoroutine(DestroyWait(4f));

            
        }
    }

    IEnumerator DestroyWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Destroy(gameObject);
    }

}
