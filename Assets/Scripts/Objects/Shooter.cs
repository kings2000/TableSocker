using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject turnIndicator;
    public MeshRenderer meshRenderer;
    public bool isTurn;
    //public AnimationCurve turnAnimation;

    [HideInInspector] public bool stoped;

    Rigidbody rbody;
    //float animTime;
    Vector3 initialScale;

    void Start()
    {
        turnIndicator.SetActive(false);
        initialScale = turnIndicator.transform.localScale;
        rbody = GetComponent<Rigidbody>();
        Invoke(nameof(ClampRb), 3f);
    }

    void ClampRb()
    {
        rbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    public void ActivateCollider()
    {
        meshRenderer.GetComponent<MeshCollider>().enabled = true;
    }

    public void UpdateIndicator(float percent)
    {
        turnIndicator.transform.localScale = initialScale + Vector3.one * percent;
    }

    public void ShooterUpdate()
    {
        
        rbody.angularVelocity = Vector3.MoveTowards(rbody.angularVelocity, Vector3.zero, Time.deltaTime * 1.5f);
        stoped = rbody.velocity.magnitude <= 0.01 && rbody.angularVelocity.magnitude <= 0.01;
        if (!stoped)
        {
            MatchHandler.instance.ObjectInMotion();
        }
    }

    public void ActivateTurnUndicator(bool value)
    {
        turnIndicator.SetActive(value);
    }

    public void SetShooterTurn(bool value)
    {
        isTurn = value;
        turnIndicator.transform.localScale = initialScale;
        
    }

    public void SetMaterial(Material material)
    {
       meshRenderer.material = material;
    }

    public void MoveToward(Vector3 direction, float force)
    {
        rbody.AddForce(direction * force, ForceMode.Force);
        //MatchHandler.instance.DisableShooterTurn();
    }
}
