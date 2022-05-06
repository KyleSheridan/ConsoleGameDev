using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{
    public Transform platform;

    public Transform[] platformWayPoints;

    public float speed = 5;

    public int currentIndex = 0;

    public void Awake()
    {
        platform.position = platformWayPoints[0].position;
    }

    void Update()
    {
        if((platform.position - platformWayPoints[currentIndex].position).magnitude < 0.1f)
        {
            currentIndex++;

            if(currentIndex >= platformWayPoints.Length)
            {
                currentIndex = 0;
            }

        }
        
        Vector3 direction = platformWayPoints[currentIndex].position - platform.position;

        direction.Normalize();

        platform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(platform.position, platformWayPoints[currentIndex].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Transform player = other.transform.parent;
            player.transform.parent = platform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Transform player = other.transform.parent;
            player.transform.parent = transform;
        }
    }
}
