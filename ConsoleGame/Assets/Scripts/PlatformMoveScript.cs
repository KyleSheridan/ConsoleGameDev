using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{
    public Transform[] platformWayPoints;

    public void Update()
    {
        for (int i =0; i< platformWayPoints.Length; i++)
        {
            this.transform.Translate(platformWayPoints[i].position); 
            if (i >= platformWayPoints.Length)
            {
                i = 0;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
        }
        
        if (other.tag == "Enemy")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }

        if (other.tag == "Enemy")
        {
            other.transform.parent = null;
        }
    }
}
