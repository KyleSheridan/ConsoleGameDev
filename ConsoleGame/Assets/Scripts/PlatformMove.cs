using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform[] platformWayPoints;

    public float speed = 5;

    Vector3 nextPos;

    public int currentIndex = 0;

    private void Awake()
    {
        transform.position = platformWayPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - platformWayPoints[currentIndex].position).magnitude < 0.1f)
        {
            currentIndex++;

            if (currentIndex >= platformWayPoints.Length)
            {
                currentIndex = 0;
            }

            Vector3 direction = platformWayPoints[currentIndex].position - transform.position;

            direction.Normalize();

            Debug.Log(direction);

            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, nextPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Transform player = other.transform.parent;
            player.transform.parent = transform;
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
