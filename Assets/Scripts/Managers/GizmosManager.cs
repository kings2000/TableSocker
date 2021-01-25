using UnityEngine;
using System.Collections;

public class GizmosManager
{

    public static void DrawGridBaseLayout(CellNode[,] grid, int gridsizex, int gridsizey, float gridcelldiameter, bool showEdges = true, float gridrotation = 0)
    {
        if (grid != null && grid.Length > 0)
        {
            for (int y = 0; y < gridsizey; y++)
            {
                for (int x = 0; x < gridsizex; x++)
                {
                    if (x == 0 || y == 0 || x == gridsizex - 1 || y == gridsizey - 1 || showEdges)
                    {
                        Gizmos.matrix = Matrix4x4.TRS(grid[x, y].worldPoint, Quaternion.Euler(Vector3.up * gridrotation), new Vector3(gridcelldiameter, .05f, gridcelldiameter));
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                    }

                }
            }
        }
    }

    public static void DrawSphere(Vector3 point, float radius)
    {
        Gizmos.DrawSphere(point, radius);
    }

    public static void DrawCircle(Vector3 center, float radius)
    {
        float inscrement = 360f / 20f;
        Vector3[] Points = new Vector3[20];
        for (int i = 0; i < Points.Length; i++)
        {
            float angle = Mathf.Deg2Rad * inscrement * i;
            float x = Mathf.Sin(angle);
            float z = Mathf.Cos(angle);

            Points[i] = new Vector3(center.x + x * radius, center.y, center.z + z * radius);
        }
        
        for (int i = 0; i < Points.Length; i++)
        {
           
            Gizmos.DrawLine(Points[i], Points[(i + 1) % Points.Length]);
        }
    }

}
