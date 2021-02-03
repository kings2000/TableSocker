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
    public GoalPoints[] goalPoints;
    //public Transform goalPoint;
    public float shooterRad = 1;

    float ballRad;
    Shooter shooter;

    private void Start()
    {
        shooter = testShooter.GetComponent<Shooter>();
    }

    public void PlayTurn()
    {
       
    }

    public void Update()
    {
        //ballRad = ball.GetChild(1).transform.lossyScale.x * .5f;


        ////int childCount = goalPoint.childCount;
        ////Vector3[] points = new Vector3[childCount];
        //for (int i = 0; i < goalPoints.Length; i++)
        //{
        //    Vector3 direc = (goalPoints[i].goalPoint.position - ball.position).normalized;
        //    Vector3 pointsFromBall = ball.position - (direc * ballRad);
        //    //points[i] = pointsFromBall;
        //    goalPoints[i].pointsFromBall = pointsFromBall;

        //    Debug.DrawLine(ball.position, goalPoints[i].goalPoint.position);
        //    Debug.DrawLine(ball.position, pointsFromBall, Color.red);

        //}

        //if(goalPoints.Length > 0)
        //{

        //    GoalPoints closeGoalPoint = goalPoints[0];
        //    float dis = 10;
        //    for (int i = 0; i < goalPoints.Length; i++)
        //    {
        //        Vector3 point = goalPoints[i].pointsFromBall;
        //        Vector3 dir = point - testShooter.position;
        //        Ray ray = new Ray(testShooter.position, dir);
        //        if (Physics.Raycast(ray, out RaycastHit hit, 1000, ballMask))
        //        {
        //            float newDis = Vector3.Distance(hit.point, goalPoints[i].pointsFromBall);
        //            if(newDis < dis)
        //            {
        //                dis = newDis;
        //                closeGoalPoint = goalPoints[i];
        //            }

        //        }

        //    }

        //    Vector3 closePoint = closeGoalPoint.pointsFromBall;
        //    Vector3 _dir = (closePoint - testShooter.position).normalized;
        //    Vector3 per = Vector3.Cross(_dir, Vector3.up);
        //    Vector3 invPer = -per;
        //    //Debug.DrawLine(testShooter.position, testShooter.position + (per * shooterRad), Color.red);
        //    //Debug.DrawLine(testShooter.position, testShooter.position + (invPer * shooterRad), Color.red);

        //    Vector3 rightEdge = testShooter.position + (invPer * (shooterRad ));
        //    Vector3 leftEdge = testShooter.position + (per * (shooterRad ));

        //    Debug.DrawLine(testShooter.position + (per * shooterRad), closePoint, Color.red);
        //    Debug.DrawLine(rightEdge, closePoint, Color.red);

        //    Vector3 rightEdgeDir = (rightEdge - closePoint).normalized;
        //    Vector3 leftEdgeDir = (leftEdge - closePoint).normalized;

        //    Vector3 rightOffsetPerpendicular = closePoint + (Vector3.Cross(rightEdgeDir, -Vector3.up) * (shooterRad - .15f));
        //    Vector3 leftOffsetPerpendicular = closePoint + (Vector3.Cross(leftEdgeDir, Vector3.up) * (shooterRad - .15f));

        //    Vector3 rightTargetOffsetDir = (rightOffsetPerpendicular - testShooter.position).normalized;
        //    Vector3 leftTargetOffsetDir = (leftOffsetPerpendicular - testShooter.position).normalized;

        //    Debug.DrawLine(closePoint,  rightOffsetPerpendicular, Color.red);
        //    Debug.DrawLine(closePoint, leftOffsetPerpendicular, Color.red);

        //    switch (closeGoalPoint.goalPointId)
        //    {
        //        case Enums.GoalPointID.Right:
        //            Debug.DrawLine(testShooter.position, leftOffsetPerpendicular);
        //            break;
        //        case Enums.GoalPointID.Center:
        //            Debug.DrawLine(testShooter.position, closePoint);
        //            break;
        //        case Enums.GoalPointID.Left:
        //            Debug.DrawLine(testShooter.position, rightOffsetPerpendicular);
        //            break;
        //    }


        //}
        Vector3 shootDir = ComputShootDirection(testShooter);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooter.MoveToward(shootDir, 500);
        }
    }

    public Vector3 ComputShootDirection(Transform shooter)
    {
        ballRad = ball.GetChild(1).transform.lossyScale.x * .5f;

        Vector3 shootDirction = Vector3.zero;
        //int childCount = goalPoint.childCount;
        //Vector3[] points = new Vector3[childCount];
        for (int i = 0; i < goalPoints.Length; i++)
        {
            Vector3 direc = (goalPoints[i].goalPoint.position - ball.position).normalized;
            Vector3 pointsFromBall = ball.position - (direc * ballRad);
            //points[i] = pointsFromBall;
            goalPoints[i].pointsFromBall = pointsFromBall;

            Debug.DrawLine(ball.position, goalPoints[i].goalPoint.position);
            Debug.DrawLine(ball.position, pointsFromBall, Color.red);

        }

        if (goalPoints.Length > 0)
        {
            
            GoalPoints closeGoalPoint = goalPoints[0];
            float dis = 10;
            for (int i = 0; i < goalPoints.Length; i++)
            {
                Vector3 point = goalPoints[i].pointsFromBall;
                Vector3 dir = point - shooter.position;
                Ray ray = new Ray(shooter.position, dir);
                goalPoints[i].madeContactOnRayHit = false;
                if (Physics.Raycast(ray, out RaycastHit hit, 1000, ballMask))
                {
                    float newDis = Vector3.Distance(hit.point, goalPoints[i].pointsFromBall);
                    goalPoints[i].madeContactOnRayHit = true;
                    if (newDis < dis)
                    {
                        dis = newDis;
                        closeGoalPoint = goalPoints[i];
                    }

                }

            }

            Vector3 closePoint = closeGoalPoint.pointsFromBall;
            Vector3 _dir = (closePoint - shooter.position).normalized;
            Vector3 per = Vector3.Cross(_dir, Vector3.up);
            Vector3 invPer = -per;
            //Debug.DrawLine(testShooter.position, testShooter.position + (per * shooterRad), Color.red);
            //Debug.DrawLine(testShooter.position, testShooter.position + (invPer * shooterRad), Color.red);

            Vector3 rightEdge = shooter.position + (invPer * (shooterRad));
            Vector3 leftEdge = shooter.position + (per * (shooterRad));

            Debug.DrawLine(shooter.position + (per * shooterRad), closePoint, Color.red);
            Debug.DrawLine(rightEdge, closePoint, Color.red);

            Vector3 rightEdgeDir = (rightEdge - closePoint).normalized;
            Vector3 leftEdgeDir = (leftEdge - closePoint).normalized;

            Vector3 rightOffsetPerpendicular = closePoint + (Vector3.Cross(rightEdgeDir, -Vector3.up) * (shooterRad - .15f));
            Vector3 leftOffsetPerpendicular = closePoint + (Vector3.Cross(leftEdgeDir, Vector3.up) * (shooterRad - .15f));

            Vector3 rightTargetOffsetDir = (rightOffsetPerpendicular - shooter.position).normalized;
            Vector3 leftTargetOffsetDir = (leftOffsetPerpendicular - shooter.position).normalized;

            Debug.DrawLine(closePoint, rightOffsetPerpendicular, Color.red);
            Debug.DrawLine(closePoint, leftOffsetPerpendicular, Color.red);

            shootDirction = _dir;

            switch (closeGoalPoint.goalPointId)
            {
                case Enums.GoalPointID.Right:
                    shootDirction = leftTargetOffsetDir;
                    Debug.DrawLine(shooter.position, leftOffsetPerpendicular);
                    break;
                case Enums.GoalPointID.Center:
                    Debug.DrawLine(shooter.position, closePoint);
                    break;
                case Enums.GoalPointID.Left:
                    shootDirction = rightTargetOffsetDir;
                    Debug.DrawLine(shooter.position, rightOffsetPerpendicular);
                    break;
            }


        }
        return shootDirction;
    }

}

[System.Serializable]
public class GoalPoints
{
    public Transform goalPoint;
    public Enums.GoalPointID goalPointId;
    [HideInInspector] public Vector3 pointsFromBall;
    [HideInInspector] public bool madeContactOnRayHit;
}