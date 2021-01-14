using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    
    Rigidbody rbody;

    void Start()
    {
        
        rbody = GetComponent<Rigidbody>();
        Invoke(nameof(ClampRb), .3f);
    }

    void ClampRb()
    {
        rbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void MoveToward(Vector3 direction, float force)
    {
        rbody.AddForce(direction * force);
    }
}
