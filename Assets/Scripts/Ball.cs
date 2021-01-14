using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    
    Rigidbody rbody;

    void Start()
    {
        
        rbody = GetComponent<Rigidbody>();
        Invoke(nameof(ClampRb), .3f);
    }

    void ClampRb()
    {
        rbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

    private void FixedUpdate()
    {
        rbody.velocity = Vector3.MoveTowards(rbody.velocity, Vector3.zero, Time.deltaTime * 2f );
        Vector3 an = rbody.angularVelocity;
        an.y = 0;
        rbody.angularVelocity = Vector3.MoveTowards(rbody.angularVelocity, an, Time.deltaTime * 1.5f);
    }


    public void OnHit(Vector3 velocity, float _speed)
    {
        //float speed = (velocity * _speed).magnitude;
        //reflector.MoveToward(velocity.normalized, speed);

    }

    

    
}
