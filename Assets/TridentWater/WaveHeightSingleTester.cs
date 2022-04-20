using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHeightSingleTester : MonoBehaviour {
    public TridentController targetController;
    public float mult;

    private void Update() {
        transform.position = new Vector3(transform.position.x, targetController.transform.position.y, transform.position.z);
        transform.position += new Vector3(0, targetController.getWaveHeight(transform.position) * mult, 0);
    }
}
