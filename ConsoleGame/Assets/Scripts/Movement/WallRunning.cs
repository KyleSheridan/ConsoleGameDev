using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    //public Transform orientation;
    private PlayerController controller;
    private Rigidbody rigidbody;

    [Header("WallRunning")]
    public LayerMask wallMask;
    public LayerMask groundMask;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        CheckForWall();
        StateMachine(InputManager.Instance.input);
    }

    private void CheckForWall()
    {
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallHit, wallCheckDistance, wallMask);
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallHit, wallCheckDistance, wallMask);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, groundMask);
    }

    private void StateMachine(InputData input)
    {
        if((wallLeft || wallRight) && input.VerticalInput > 0 && AboveGround())
        {
            StartWallRun();
        }
        else
        {
            StopWallRun();
        }
    }

    private void StartWallRun()
    {
        controller.wallRunning = true;

        controller.wallRunningOnLeft = wallLeft;

        controller.wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        //Vector3 wallForward = Vector3.Cross(controller.wallNormal, transform.up);

        //if ((transform.forward - wallForward).magnitude > (transform.forward + wallForward).magnitude)
        //    controller.wallNormal = -controller.wallNormal;
    }

    private void StopWallRun()
    {
        controller.wallRunning = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallCheckDistance);
        Gizmos.DrawLine(transform.position, transform.position - Vector3.right * wallCheckDistance);
    }
}
