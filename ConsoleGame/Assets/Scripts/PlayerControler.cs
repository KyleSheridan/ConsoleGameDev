using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody rigidbody { get; private set; }
    public InputData input { get; private set; }

    public float gravityMultiplier = 3f;
    [Range(0, 50)]
    public int gravityCurve = 10;

    public float moveSpeed = 30f;
    public float jumpHeight = 1000;

    [Range(0, 50)]
    public int jumpBufferFrames = 5;

    // all inputsources that can control the player
    IInput[] allInputs;

    bool grounded = true;
    float groundCheckLength;

    bool canJump = true;
    bool jumping = false;

    float currentGravity;

    int jumpBufferCountdown;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        allInputs = GetComponents<IInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        groundCheckLength = transform.localScale.y + 0.1f;
        currentGravity = gravityMultiplier;
        ResetJumpBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetInputs();

        PlayerMove();

        Jump();

        CheckGrounded();

        ApplyGravity();

        Debug.Log(input.HorizontalInput);
    }

    void CheckGrounded()
    {
        //Vector3 playerBase = transform.position + (Vector3.down * transform.localScale.y);

        if (Physics.Raycast(transform.position, Vector3.down, groundCheckLength))
        {
            grounded = true;

            currentGravity = 1;

            jumping = false;

            ResetJumpBuffer();
        }
        else
        {
            grounded = false;

            if (!jumping) currentGravity = gravityMultiplier;
            else IncreaseGravity();

            JumpBuffer();
        }
    }

    void IncreaseGravity()
    {
        // only start increasing gravity at top of jump
        if(rigidbody.velocity.y > 0) { return; }

        if(currentGravity >= gravityMultiplier) { return; }

        float gravityIncrement = gravityMultiplier / gravityCurve;

        currentGravity += gravityIncrement;
    }

    void ApplyGravity()
    {
        rigidbody.AddForce(Physics.gravity * currentGravity, ForceMode.Acceleration);
    }

    void ResetJumpBuffer()
    {
        jumpBufferCountdown = jumpBufferFrames;
        canJump = true;
    }

    void JumpBuffer()
    {
        if(jumpBufferCountdown > 0)
        {
            jumpBufferCountdown--;
        } 
        else
        {
            canJump = false;
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
        if(canJump)
        {
            if(input.Jump)
            {
                rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                jumping = true;
            }
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
