using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public float friction = 1;
    public LayerMask wallMask;
    [SerializeField, Range(0f, 1f)]
    float maxDis = 1f;

    
    [HideInInspector]public float maxSpeed;
    [HideInInspector] public Vector3 velocity;
    Vector3 displacement;
    float initYPosition;
    Rigidbody rigidbody;
    float raduis;


    void Start()
    {
        initYPosition = transform.localPosition.y;
        rigidbody = GetComponent<Rigidbody>();
        raduis = transform.localScale.x * .5f;
    }

    private void Update()
    {

        //float angle = 360f / rayCount;
        //for (int i = 0; i < rayCount; i++)
        //{
        //    float a = (angle * i) * Mathf.Deg2Rad;
        //    Vector3 dir = new Vector3(Mathf.Sin(a), 0, Mathf.Cos(a));
        //    //Vector3 to = transform.position + dir;

        //    if(Physics.SphereCast(transform.position,.1f, dir, out RaycastHit hit, raduis, wallMask))
        //    {
        //        Vector3 vel = velocity;
        //        velocity = Vector3.zero;
        //        Vector3 reflect = Vector3.Reflect(vel, hit.normal);
        //        velocity = reflect;
        //        print(hit.collider.name);
                
        //        print(hit.collider.name);
        //        break;
        //    }

            
        //}
    }

    void FixedUpdate()
    {
        displacement = velocity * Time.deltaTime * maxSpeed;
        displacement.y = 0;
        transform.localPosition += displacement;
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, Time.deltaTime * friction);
        rigidbody.angularVelocity = Vector3.MoveTowards(rigidbody.angularVelocity, Vector3.zero, Time.deltaTime * (2f - velocity.magnitude));
        if (velocity.magnitude <= 0)
        {
            rigidbody.angularVelocity = Vector3.MoveTowards(rigidbody.angularVelocity, Vector3.zero, Time.deltaTime * (40));
        }
    }

    public void MoveToward(Vector3 direction, float speed)
    {
        GetComponent<SphereCollider>().enabled = false;
        maxSpeed = speed;
        GetComponent<SphereCollider>().enabled = true;
        velocity = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        return;
        if (collision.collider.tag == "Wall")
        {
            Vector3 vel = velocity;
            velocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            Vector3 reflect = Vector3.Reflect(vel, collision.contacts[0].normal);
            velocity = reflect;

        }
        else if(collision.collider.tag == "Shooter")
        {
            Reflector reflector = collision.collider.GetComponent<Reflector>();
            maxSpeed = reflector.velocity.magnitude * reflector.maxSpeed;
            velocity = collision.contacts[0].normal;
        }

    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //float angle = 360f /rayCount;
        //for (int i = 0; i < rayCount; i++)
        //{
        //    float a = (angle * i) * Mathf.Deg2Rad;
        //    Vector3 dir = new Vector3( Mathf.Sin(a),0, Mathf.Cos(a));
        //    Vector3 to = transform.position + dir * raduis * 1.2f;
        //    Gizmos.DrawLine(transform.position + dir * raduis * .8f, to);
        //    Gizmos.DrawWireSphere(to, .1f);
        //}
    }
}
