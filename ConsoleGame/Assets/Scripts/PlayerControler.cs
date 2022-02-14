using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody rigidbody { get; private set; }
    public InputData input { get; private set; }

    public float gravityMultiplier = 3f;

    public float moveSpeed = 30f;
    public float jumpHeight = 1000;

    // all inputsources that can control the player
    IInput[] allInputs;

    bool grounded = true;
    float groundCheckLength;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        allInputs = GetComponents<IInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        groundCheckLength = transform.localScale.y + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetInputs();

        ApplyGravity();

        PlayerMove();

        Jump();
    }

    void ApplyGravity()
    {
        //Vector3 playerBase = transform.position + (Vector3.down * transform.localScale.y);

        if (Physics.Raycast(transform.position, Vector3.down, groundCheckLength))
        {
            grounded = true;

            rigidbody.AddForce(Physics.gravity, ForceMode.Acceleration);
        }
        else
        {
            grounded = false;

            rigidbody.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    private void PlayerMove()
    {
        Vector3 moveDir = new Vector3(input.HorizontalInput, 0, input.VerticalInput);

        moveDir.Normalize();

        rigidbody.AddForce(moveDir * moveSpeed);
    }

    void Jump()
    {
        if(input.Jump && grounded)
        {
            rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    void GetInputs()
    {
        input = new InputData();

        for (int i = 0; i < allInputs.Length; i++)
        {
            input = allInputs[i].GenerateInput();
        }
    }

    private void OnDrawGizmos()
    {
        //Vector3 playerBase = transform.position + (Vector3.down * transform.localScale.y);

        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundCheckLength));
    }
}
