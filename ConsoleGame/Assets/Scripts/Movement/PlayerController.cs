using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody { get; private set; }
    public CombatManager combat { get; private set; }
    public Character characterStats { get; private set; }

    public PlayerHealth health;

    public Transform cam;

    public LayerMask hitMask;

    public float gravityMultiplier = 3f;
    [Range(0, 50)]
    public int gravityCurve = 10;

    public float moveSpeed = 30f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 1000;
    public float wallRunMultiplier = 0.9f;
    public float wallJumpForce = 100f;

    [Range(0, 50)]
    public int jumpHeightBufferFrames = 5;

    [Range(0, 50)]
    public int jumpBufferFrames = 5;

    public bool grounded { get; private set; }
    float groundCheckLength;

    bool canJump = true;
    bool canDoubleJump = false;
    bool doubleJumpActive = false;
    bool jumping = false;

    [HideInInspector]
    public bool wallRunning = false;
    [HideInInspector]
    public bool wallRunningOnLeft = false;

    [HideInInspector]
    public Vector3 wallNormal;

    float currentGravity;

    int jumpHeightBufferCountdown;
    int jumpBufferCountdown;

    float turnSmoothVelocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        combat = GetComponent<CombatManager>();
        characterStats = GetComponent<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
        groundCheckLength = transform.localScale.y + 0.1f;
        currentGravity = gravityMultiplier;
        ResetJumpHeightBuffer();

        foreach(var c in Input.GetJoystickNames())
        {
            Debug.Log(c);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        combat.UpdateCombat(InputManager.Instance.input);
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        PlayerMove();

        Jump();

        ApplyGravity();
    }

    void CheckGrounded()
    {
        bool groundCheck = Physics.Raycast(transform.position, Vector3.down, groundCheckLength) ||
                           Physics.Raycast(transform.position + (Vector3.right * (transform.localScale.x * 0.5f)), Vector3.down, groundCheckLength) ||
                           Physics.Raycast(transform.position - (Vector3.right * (transform.localScale.x * 0.5f)), Vector3.down, groundCheckLength) ||
                           Physics.Raycast(transform.position + (Vector3.forward * (transform.localScale.z * 0.5f)), Vector3.down, groundCheckLength) ||
                           Physics.Raycast(transform.position - (Vector3.forward * (transform.localScale.z * 0.5f)), Vector3.down, groundCheckLength);

        if (groundCheck)
        {
            grounded = true;

            rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);

            currentGravity = 1;

            jumping = false;

            canDoubleJump = true;

            if(canJump) { return; }

            JumpBuffer();
        }
        else
        {
            grounded = false;

            if (!jumping) currentGravity = gravityMultiplier;
            else IncreaseGravity();

            if (!doubleJumpActive)
                JumpHeightBuffer();

            if(!canJump && !InputManager.Instance.input.Jump && canDoubleJump)
            {
                canJump = true;
                canDoubleJump = false;
                doubleJumpActive = true;

                ResetJumpBuffer();
            }
        }

        if(wallRunning)
        {
            currentGravity = 0;
            JumpBuffer();
        }
    }

    void IncreaseGravity()
    {
        // only start increasing gravity at top of jump
        if (rigidbody.velocity.y > 0) { return; }

        if (currentGravity >= gravityMultiplier) { return; }

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
        ResetJumpHeightBuffer();
    }

    void JumpBuffer()
    {
        if (jumpBufferCountdown > 0)
        {
            jumpBufferCountdown--;
        }
        else
        {
            ResetJumpBuffer();
        }
    }

    void ResetJumpHeightBuffer()
    {
        jumpHeightBufferCountdown = jumpHeightBufferFrames;
        canJump = true;
        currentGravity = 1;
    }

    void JumpHeightBuffer()
    {
        if (jumpHeightBufferCountdown > 0)
        {
            jumpHeightBufferCountdown--;
        }
        else
        {
            canJump = false;
        }
    }

    private void PlayerMove()
    {
        if(combat.attacking) { return; }

        Vector3 direction = new Vector3(InputManager.Instance.input.HorizontalInput, 0, InputManager.Instance.input.VerticalInput);

        //direction.Normalize();

        direction = Vector3.ClampMagnitude(direction, 1);

        float actualSpeed = characterStats.MovementSpeed.baseValue;

        if (wallRunning)
            actualSpeed *= wallRunMultiplier;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            rigidbody.AddForce(moveDir * actualSpeed * direction.magnitude);
        }
    }

    void Jump()
    {
        if(combat.attacking) { return; }

        if (canJump)
        {
            if (InputManager.Instance.input.Jump)
            {
                if (wallRunning)
                {
                    rigidbody.AddForce(wallNormal * wallJumpForce, ForceMode.Impulse);
                }

                if (doubleJumpActive)
                {
                    doubleJumpActive = false;
                    ResetJumpHeightBuffer();
                }

                rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                jumping = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit!");
            float baseDamage = other.gameObject.GetComponentInParent<EnemyAnimEvents>().meleeDamage;

            float rawDamage = baseDamage - characterStats.PhysicalDefence.Value;

            float damage = Mathf.Clamp(rawDamage, 1, baseDamage);

            health.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundCheckLength));
        Gizmos.DrawLine(transform.position + (Vector3.right * (transform.localScale.x * 0.5f)),   transform.position + (Vector3.down * groundCheckLength) + (Vector3.right * (transform.localScale.x * 0.5f))); 
        Gizmos.DrawLine(transform.position - (Vector3.right * (transform.localScale.x * 0.5f)),   transform.position + (Vector3.down * groundCheckLength) - (Vector3.right * (transform.localScale.x * 0.5f)));
        Gizmos.DrawLine(transform.position + (Vector3.forward * (transform.localScale.z * 0.5f)), transform.position + (Vector3.down * groundCheckLength) + (Vector3.forward * (transform.localScale.z * 0.5f)));
        Gizmos.DrawLine(transform.position - (Vector3.forward * (transform.localScale.z * 0.5f)), transform.position + (Vector3.down * groundCheckLength) - (Vector3.forward * (transform.localScale.z * 0.5f)));
    }
}

