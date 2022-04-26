using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    private const KeyCode FORWARD_KEY = KeyCode.W;
    private const KeyCode BACKWARD_KEY = KeyCode.S;
    private const KeyCode LEFT_KEY = KeyCode.A;
    private const KeyCode RIGHT_KEY = KeyCode.D;

    public TridentController waterPlane;
    public float buoyancy;
    public float movementForce;

    private Collider collider;
    private Rigidbody rigidbody;

    private void Awake() {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        applyBuoyantForce();
        applyMovementForce();
    }




    private void applyMovementForce() {
        if (Input.GetKey(FORWARD_KEY))  rigidbody.AddForce(Vector3.forward * movementForce);
        else if (Input.GetKey(BACKWARD_KEY)) rigidbody.AddForce(Vector3.back * movementForce);

        if (Input.GetKey(LEFT_KEY)) rigidbody.AddForce(Vector3.left * movementForce);
        else if (Input.GetKey(RIGHT_KEY)) rigidbody.AddForce(Vector3.right * movementForce);
    }
    
    private void applyBuoyantForce() {
        float height = collider.bounds.size.y;
        float waveHeight = waterPlane.getWaveHeight(transform.position);
        float waveOffsetTop = (transform.position.y + (height / 2f)) - waveHeight;
        float amountSubmerged = 1 - Mathf.Clamp01(waveOffsetTop / height);
        float localBuoyancy = buoyancy * amountSubmerged * -Physics.gravity.y * rigidbody.mass;
        rigidbody.AddForce(new Vector3(0, localBuoyancy, 0));
    }
}
