using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject crackedVersion;


    private void OnMouseDown()
    {
        Instantiate(crackedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
