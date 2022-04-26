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
        float waveOffsetTop = (transform.position.y + (height / 2f)) - waveHeight; // top of object's height above water
        float amountSubmerged = 1 - Mathf.Clamp01(waveOffsetTop / height);
        print( "> " + amountSubmerged);
        float localBuoyancy = buoyancy * amountSubmerged * -Physics.gravity.y * rigidbody.mass;
        rigidbody.AddForce(new Vector3(0, localBuoyancy, 0));
        // Vector3 extents = collider.bounds.extents;
        // for (int x = -1; x <= 1; x += 2) {
        //     for (int z = -1; z <= 1; z += 2) {
        //         Vector3 currBoundPoint = collider.bounds.center + new Vector3(extents.x * x, collider.bounds.center.y, extents.z * z);
        //         float waveHeight = waterPlane.getWaveHeight(currBoundPoint) + waterPlane.transform.position.y;
        //         float amountSubmerged = Mathf.Clamp01((1 - collider.bounds.center.y + extents.y - waveHeight) / extents.y * 2f);
        //         float localBuoyancy = buoyancy * amountSubmerged * -Physics.gravity.y * rigidbody.mass * 0.25f;
        //         print(" > " + amountSubmerged);
        //         rigidbody.AddForceAtPosition(new Vector3(0, localBuoyancy, 0), currBoundPoint);
        //     }
        // }
    }
}
