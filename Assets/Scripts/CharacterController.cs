using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    private const KeyCode FORWARD_KEY = KeyCode.W;
    private const KeyCode BACKWARD_KEY = KeyCode.S;
    private const KeyCode LEFT_KEY = KeyCode.A;
    private const KeyCode RIGHT_KEY = KeyCode.D;
    private const KeyCode UP_KEY = KeyCode.Space;
    private const KeyCode DOWN_KEY = KeyCode.LeftShift;

    public TridentController waterPlane;
    public float buoyancy;
    public float movementForce;
    public float movementForceY;
    public float mouseSensitivity { get; private set; } = 200;

    private Camera mainCamera;
    private Collider collider;
    private Rigidbody rigidbody;

    public float waterHeight;
    public float gravity = 9.8f;
    private float velocity = 0;
    private float rotationX = 0;

    //Animation Variables
    Animator animator;
    int isMovingHash;

    private void Awake() {
        mainCamera = Camera.main;
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;

        //Animation Variables
        animator = GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
    }

    private void Update() {
        updateDirection();
        updateMovement();
        //updateModelDirection();
    }

    private void FixedUpdate() {
        applyBuoyantForce();
    }

    private void updateModelDirection(){
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move.Normalize();
        //controller.Move(move * Time.deltaTime * 2);

        // turn the character in the direction its moving (if applicable)
        if (move != Vector3.zero) {
                //gameObject.transform.forward = move;
                Quaternion rotGoal = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotGoal, 750f * Time.deltaTime);

        }
    }
    private void updateDirection() {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 rotationY = new Vector3(0, mouseDelta.x, 0) * (Time.deltaTime * mouseSensitivity);
        
        // x-axis rotation (applies to camera only)
        float rotationDeltaX = -mouseDelta.y * Time.deltaTime * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX + rotationDeltaX, -90, 90);
        mainCamera.transform.localRotation = Quaternion.Euler(new Vector3(rotationX, 0, 0));
        
        // y-axis rotation (applies to player obj only)
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationY);
    }

    private void updateMovement() {
        //animation if statement to check if moving;
        // if (Input.GetKey(FORWARD_KEY) ||Input.GetKey(LEFT_KEY) || Input.GetKey(RIGHT_KEY)){
        //     animator.SetBool(isMovingHash, true);
        // } else{
        //     animator.SetBool(isMovingHash, false);

        // }

        if (Input.GetKey(FORWARD_KEY)) rigidbody.AddForce(transform.forward * (movementForce * Time.deltaTime));
        if (Input.GetKey(BACKWARD_KEY)) rigidbody.AddForce(-transform.forward * (movementForce * Time.deltaTime));
        if (Input.GetKey(LEFT_KEY)) rigidbody.AddForce(-transform.right * (movementForce * Time.deltaTime));
        if (Input.GetKey(RIGHT_KEY)) rigidbody.AddForce(transform.right * (movementForce * Time.deltaTime));

        // note: these up/down movement commands will probably eventually be replaced with a ballast tank fill/drain that affects buoyancy
        if (transform.position.y < waterHeight)
        {
            if (Input.GetKey(UP_KEY)) rigidbody.AddForce(transform.up * (movementForceY * Time.deltaTime));
            if (Input.GetKey(DOWN_KEY)) rigidbody.AddForce(-transform.up * (movementForceY * Time.deltaTime));
        }
        else
        {
            velocity -= gravity * Time.deltaTime;
            rigidbody.AddForce(new Vector3(0, velocity, 0));
        }

        
    }
    
    private void applyBuoyantForce() {
            float height = collider.bounds.size.y;
            float waveHeight = waterPlane.getWaveHeight(transform.position);
            float waveOffsetTop = (transform.position.y + (height / 2f)) - waveHeight;
            float amountSubmerged = 1 - Mathf.Clamp01(waveOffsetTop / height);
            float localBuoyancy = buoyancy * amountSubmerged * -Physics.gravity.y * rigidbody.mass;
            rigidbody.AddForce(new Vector3(0, localBuoyancy, 0), ForceMode.Acceleration);
    }
}
