using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyUI : MonoBehaviour
{
    public float bobSpeed;
    public float bobAmount;

    private float startY;
    private float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * bobSpeed * direction * Time.deltaTime);

        if(transform.position.y >= (bobAmount + startY) && direction > 0)
        {
            bobAmount *= -1;
            direction *= -1;
        }
        else if(transform.position.y <= bobAmount + startY && direction < 0)
        {
            bobAmount *= -1;
            direction *= -1;
        }
    }
}
