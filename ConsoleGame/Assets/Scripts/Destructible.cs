using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject crackedVersion;

    public GameObject[] spawnableObjects;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Melee")
        {
            Break();
        }

        if (other.gameObject.tag == "Ranged")
        {
            Destroy(gameObject);
            Break();
        }

        if (other.gameObject.tag == "Magic")
        {
            Destroy(gameObject);
            Break(); 
        }
    }
    private void Break()
    {
        Instantiate(crackedVersion, transform.position, transform.rotation);
        int randNum = Random.Range(0, spawnableObjects.Length);
        if (spawnableObjects[randNum] != null)
        {
            Instantiate(spawnableObjects[randNum], transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }


}
