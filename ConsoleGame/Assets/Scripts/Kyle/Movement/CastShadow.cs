using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadow : MonoBehaviour
{
    public GameObject shadow;

    public LayerMask hitMask;

    public float shadowRenderLength = 100f;
    
    RaycastHit info = new RaycastHit();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PositionOnFloor();
    }

    void PositionOnFloor()
    {
        

        if(Physics.Raycast(transform.position, Vector3.down, out info, shadowRenderLength, hitMask))
        {
            if(!shadow.activeSelf)
            {
                shadow.SetActive(true);
            }

            shadow.transform.position = new Vector3(transform.position.x, info.point.y + 0.001f, transform.position.z);
        }
        else
        {
            shadow.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {

    }
}
