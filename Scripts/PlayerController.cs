using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    //movement
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 500f;
    //[SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private bool isGamepad;
    
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveForce = 200f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 direction;

    public bool isAddForceMove;
    public bool isChangeVelocityMove;
    private Vector2 movement;
    public Vector2 aim;

    private Vector3 playerVelocity;

    private PlayerControls playerControls;
    private PlayerInput playerInput;
    
    //Reference
    public AttackController attackController;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Cursor.visible = false;
    }
    
    private void Update()
    {
        HandleInput();
        HandleRotation();
        Shoot();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        direction = playerControls.Controls.Movement.ReadValue<Vector2>();
        moveDirection = new Vector2(direction.x, direction.y).normalized;
        
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        if (isChangeVelocityMove)   
        {
            ChangeVelocityMove();
        }
        if (isAddForceMove)
        {
            AddForceMove();
        }
    }

    void HandleRotation()
    {
        if (isGamepad)
        {
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;

                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newrotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newrotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    }
    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void onDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    void AddForceMove()
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed && Mathf.Abs(rb.velocity.z) < maxSpeed) 
        {
            rb.AddForce(moveDirection.x * moveForce, 0, moveDirection.y * moveForce, ForceMode.Force);
        }
    }

    void ChangeVelocityMove()
    {
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.y * moveSpeed);    
    }
    
    void Shoot ()
    {
        if (gameObject.GetComponent<AttackController>().isChargeGun)
        {
            if ((playerControls.Controls.Shoot.WasPressedThisFrame()))
            {
                //PlayPullBowSound();
            }
            if ((playerControls.Controls.Shoot.IsPressed()))
            {
                attackController.PrepareAttack();
            }
            if ((playerControls.Controls.Shoot.WasReleasedThisFrame()))
            {
                attackController.Attack();
            }
        }

        if (gameObject.GetComponent<AttackController>().isImpulseGun)
        {
            if ((playerControls.Controls.Shoot.WasPressedThisFrame()))
            {
                attackController.Attack();
            }
        }
        
        if (gameObject.GetComponent<AttackController>().isMachineGun)
        {
            if ((playerControls.Controls.Shoot.IsPressed()))
            {
                attackController.Attack();
            }
        }

    }
}
