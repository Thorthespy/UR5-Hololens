using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    private bool _groundDetected = false;
    public Transform endpoint;
    void FixedUpdate()
    {
        RaycastHit hit;

        if (!_groundDetected && Physics.Raycast(transform.position, Vector3.down, out hit)) {
            float offset = Vector3.Distance(transform.position, endpoint.position);
            transform.position = new Vector3(transform.position.x, hit.point.y + offset, transform.position.z);
            _groundDetected = true;
        }
            
    }
}
