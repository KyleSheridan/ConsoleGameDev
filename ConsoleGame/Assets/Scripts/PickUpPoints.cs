using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    float xpPoints = 30f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //XP += xpPoints
            Destroy(gameObject);
        }
    }
}
