using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Football/" + (nameof(ObjectFactory)))]
public class ObjectFactory : ScriptableObject
{

    public Ball ballPrefab;
    public Shooter shooterPrefab;

    public Ball GetBall()
    {
        Ball ins = Instantiate(ballPrefab);
        return ins;
    }
    

    public Shooter GetShooter()
    {
        return Instantiate(shooterPrefab);
    }
}