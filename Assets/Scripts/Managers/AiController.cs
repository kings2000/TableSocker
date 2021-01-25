using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class AiController : MonoBehaviour
{

    public Team aiTeam;

    public Transform testShooter;
    public Transform ball;
    public Transform ballRaduis;
    public LayerMask ballMask;

    public Transform goalPoint;
    public float shooterRad = 1;

    float ballRad;

    public void PlayTurn()
    {

    }

    public void Update()
    {
        ballRad = ball.GetChild(1).transform.lossyScale.x * .5f;
        
        
        //

        

        int childCount = goalPoint.childCount;
        Vector3[] points = new Vector3[childCount];
        for (int i = 0; i < childCount; i++)
        {
            Vector3 direc = (goalPoint.GetChild(i).position - ball.position).normalized;
            Debug.DrawLine(ball.position, goalPoint.GetChild(i).position);
            Vector3 pointsFromBall = ball.position - (direc * ballRad);
            Debug.DrawLine(ball.position, pointsFromBall, Color.red);
            points[i] = pointsFromBall;
        }

        if(childCount > 0)
        {
            
            Vector3 closePoint = points[0];
            float dis = 10;
            for (int i = 0; i < childCount; i++)
            {
                Vector3 point = points[i];
                Vector3 dir = point - testShooter.position;
                Ray ray = new Ray(testShooter.position, dir);
                if (Physics.Raycast(ray, out RaycastHit hit, 1000, ballMask))
                {
                    float newDis = Vector3.Distance(hit.point, point);
                    if(newDis < dis)
                    {
                        dis = newDis;
                        closePoint = point;
                    }
                    
                }

            }
            Debug.DrawLine(testShooter.position, closePoint);
            Vector3 _dir = (closePoint - testShooter.position).normalized;
            Vector3 per = Vector3.Cross(_dir, Vector3.up);
            Vector3 invPer = -per;
            Debug.DrawLine(testShooter.position, testShooter.position + (per * shooterRad), Color.red);
            Debug.DrawLine(testShooter.position, testShooter.position + (invPer * shooterRad), Color.red);

            Debug.DrawLine(testShooter.position + (per * shooterRad), closePoint, Color.red);
            Debug.DrawLine(testShooter.position + (invPer * shooterRad), closePoint, Color.red);
        }

    }

}