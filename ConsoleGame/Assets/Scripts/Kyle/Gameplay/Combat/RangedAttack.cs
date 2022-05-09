using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float speed = 100f;
    public float lifetime = 1f;

    public float damage = 0f;

    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
