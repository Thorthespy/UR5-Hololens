using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Class works with the Spatial Mapping Prefab from the Windows Mixed Reality Toolkit. If you apply this on a GameObject, it will move
//the Transform of the Gameobject. A Raycast will be send downwards to the ground and if it hits a collider, the position of the transfrom
//will move to this point. To place to object on the ground and not in the ground, you have to place an empty gameobject as a children of the
//gameobject and assign it in this script.
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
