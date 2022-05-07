using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : MonoBehaviour
{
    float healthPoints = 30f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //health += healthPoints
            Destroy(gameObject);
        }
    }
}
