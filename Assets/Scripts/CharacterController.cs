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
    public float mouseSensitivity;

    private Camera mainCamera;
    private Collider collider;
    private Rigidbody rigidbody;

    private void Awake() {
        mainCamera = Camera.main;
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        updateDirection();
        updateMovement();
    }

    private void FixedUpdate() {
        applyBuoyantForce();
    }


    private void updateDirection() {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 rotationY = new Vector3(0, mouseDelta.x, 0) * (Time.deltaTime * mouseSensitivity);
        Vector3 rotationX = new Vector3(-mouseDelta.y, 0, 0) * (Time.deltaTime * mouseSensitivity);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationY);
        mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.eulerAngles + rotationX);
    }

    private void updateMovement() {
        if (Input.GetKey(FORWARD_KEY)) rigidbody.AddForce(transform.forward * (movementForce * Time.deltaTime));
        if (Input.GetKey(BACKWARD_KEY)) rigidbody.AddForce(-transform.forward * (movementForce * Time.deltaTime));
        if (Input.GetKey(LEFT_KEY)) rigidbody.AddForce(-transform.right * (movementForce * Time.deltaTime));
        if (Input.GetKey(RIGHT_KEY)) rigidbody.AddForce(transform.right * (movementForce * Time.deltaTime));
        
        if (Input.GetKey(UP_KEY)) rigidbody.AddForce(transform.up * (movementForceY * Time.deltaTime));
        if (Input.GetKey(DOWN_KEY)) rigidbody.AddForce(-transform.up * (movementForceY * Time.deltaTime));
    }
    
    private void applyBuoyantForce() {
        float height = collider.bounds.size.y;
        float waveHeight = waterPlane.getWaveHeight(transform.position);
        float waveOffsetTop = (transform.position.y + (height / 2f)) - waveHeight;
        float amountSubmerged = 1 - Mathf.Clamp01(waveOffsetTop / height);
        float localBuoyancy = buoyancy * amountSubmerged * -Physics.gravity.y * rigidbody.mass;
        print("A: " + amountSubmerged + ", B: " + localBuoyancy);
        rigidbody.AddForce(new Vector3(0, localBuoyancy, 0), ForceMode.Acceleration);
    }
}
