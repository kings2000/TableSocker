using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float drag = 1;
    Rigidbody rbody;
    [HideInInspector]public bool stoped;
    

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        Invoke(nameof(ClampRb), 4f);
    }

    void ClampRb()
    {
        rbody.constraints = RigidbodyConstraints.FreezePositionY;
        //MatchHandler.instance.StartGame();
    }

    public void BallUpdate()
    {
        
        rbody.velocity = Vector3.MoveTowards(rbody.velocity, Vector3.zero, Time.fixedDeltaTime * drag );
        Vector3 an = rbody.angularVelocity;
        an.y = 0;
        rbody.angularVelocity = Vector3.MoveTowards(rbody.angularVelocity, an, Time.deltaTime * 6f);
        stoped = rbody.velocity.magnitude <= 0 && rbody.angularVelocity.magnitude <= 0.01;

        if (!stoped)
        {
            MatchHandler.instance.ObjectInMotion();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        MatchHandler.instance.OnGoal(other.tag);
    }

}
