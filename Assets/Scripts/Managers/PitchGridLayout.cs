using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNode
{
    public int gridX;
    public int gridY;
    public bool isWalkable;
    public Vector3 worldPoint;
    public string ownerKey;
    
}

public class PitchGridLayout : MonoBehaviour
{

    public static PitchGridLayout instance;

    public bool showGiszmo;
    public int gridSizeX;
    public int gridSizeY;
    public float nodeDiameter = 0.25f;
    public CellNode[,] grid;

    void Start()
    {
        instance = this;
    }

    void GenerateGride()
    {
        float nodeRaduis = nodeDiameter / 2f;
        grid = new CellNode[gridSizeX, gridSizeY];
        Vector3 baseButtomLeft = transform.right * (-gridSizeX * nodeRaduis) + transform.forward * (-gridSizeY * nodeRaduis);
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                float pointX = baseButtomLeft.x + (x * nodeDiameter) + (nodeRaduis);
                float pointZ = baseButtomLeft.z + (y * nodeDiameter) + (nodeRaduis);
                Vector3 pointPos = new Vector3(pointX, 0, pointZ);
                grid[x, y] = new CellNode() { isWalkable = true, gridX = x, gridY = y, worldPoint = pointPos };

            }
        }

    }

    public Vector3 GetSnapPosition(Vector3 position, Vector2 tileSize)
    {
        Vector3 snapCart = Helper.Snap((position), nodeDiameter);
        float percentY = (tileSize.y % 2 == 0) ? 0 : nodeDiameter / 2f;
        float percentX = (tileSize.x % 2 == 0) ? 0 : nodeDiameter / 2f;
        snapCart -= new Vector3(-percentX, 0, percentY);
        return snapCart;
    }

    //public static Vector3 GetSnapPosition(Vector3 position, Vector2 tileSize)
    //{
    //    Vector3 snapCart = Helper.Snap((position), nodeDiameter);
    //    float percentY = (tileSize.y % 2 == 0) ? 0 : nodeDiameter / 2f;
    //    float percentX = (tileSize.x % 2 == 0) ? 0 : nodeDiameter / 2f;
    //    snapCart -= new Vector3(-percentX, 0, percentY);
    //    return snapCart;
    //}

    private void OnDrawGizmos()
    {
        if (showGiszmo)
        {
            GenerateGride();
            GizmosManager.DrawGridBaseLayout(grid, gridSizeX, gridSizeY, nodeDiameter, true);
        }
    }
}
