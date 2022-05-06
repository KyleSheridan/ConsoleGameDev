using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveScript : MonoBehaviour
{
    public Transform[] platformWayPoints;

    public Transform startPos;

    public float speed;

    Vector3 nextPos;

    public GameObject player;

    public void Awake()
    {
        transform.position = startPos.position;
    }

    public void Update()
    {
        for (int i =0; i< platformWayPoints.Length; i++)
        {
            if(transform.position == platformWayPoints[i].position)
            {
                if(i < platformWayPoints.Length - 1)
                {
                    nextPos = platformWayPoints[i+1].position;
                }

                else 
                {
                    nextPos = platformWayPoints[i-1].position;
                }
            }

            Debug.Log(nextPos);
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

            if (i >= platformWayPoints.Length - 1)
            {
                //
            }
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
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.parent = null;
        }
    }
}
