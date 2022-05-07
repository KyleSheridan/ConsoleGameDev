using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform platform;

    public Transform[] platformWayPoints;

    public float speed = 5;

    Vector3 nextPos;

    int currentIndex = 0;

    private void Awake()
    {
        platform.position = platformWayPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((platform.position - platformWayPoints[currentIndex].position).magnitude < 0.1f)
        {
            currentIndex++;

            if (currentIndex >= platformWayPoints.Length)
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
        Gizmos.DrawLine(platform.position, nextPos);
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
            player.transform.parent = null;
        }
    }
}
